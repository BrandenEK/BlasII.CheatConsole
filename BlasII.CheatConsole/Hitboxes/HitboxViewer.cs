using Il2CppTGK.Game.Components.Attack;
using Il2CppTGK.Game.Components.Persistence;
using Il2CppTGK.Game.Components;
using System.Collections.Generic;
using UnityEngine;

namespace BlasII.CheatConsole.Hitboxes
{
    public class HitboxViewer
    {
        private const string GEOMETRY_NAME = "GEO_Block";

        private readonly Dictionary<int, LineRenderer> _activeHitboxes = new();

        private bool _showHitboxes = false;

        private float _currentDelay = 0f;
        private bool _inGame = false;

        public HitboxConfig HitboxConfig { get; private set; }
        public Sprite HitboxImage { get; private set; }

        private bool ShowHitboxes
        {
            set
            {
                _showHitboxes = value;
                if (value)
                    AddHitboxes();
                else
                    RemoveHitboxes();
            }
        }

        private Material _material;
        public Material RendererMaterial
        {
            get
            {
                if (_material == null)
                {
                    var obj = new GameObject("Temp");
                    _material = obj.AddComponent<SpriteRenderer>().material;
                    Object.Destroy(obj);
                }
                return _material;
            }
        }

        public void CreateHitboxImage()
        {
            var tex = new Texture2D(1, 1, TextureFormat.RGB24, false);
            tex.SetPixel(0, 0, Color.white);
            tex.Apply();
            HitboxImage = Sprite.Create(tex, new Rect(0, 0, 1, 1), Vector2.zero, 1, 0, SpriteMeshType.FullRect);
        }

        public void Initialize()
        {
            HitboxConfig = new HitboxConfig(true, false, 1f);
        }

        public void SceneLoaded()
        {
            _inGame = true;
            CreateHitboxImage();

            if (_showHitboxes)
                AddHitboxes();
        }

        public void SceneUnloaded()
        {
            RemoveHitboxes();

            _inGame = false;
        }

        public void Update()
        {
            if (_showHitboxes && _inGame)
            {
                _currentDelay += Time.deltaTime;
                if (_currentDelay >= HitboxConfig.updateDelay)
                    AddHitboxes();
            }

            if (Input.GetKeyDown(KeyCode.Keypad8))
                ShowHitboxes = !_showHitboxes;
        }

        private void AddHitboxes()
        {
            float newColliders = 0;

            // Foreach collider in the scene, either add it or update it
            var foundColliders = new List<int>();
            foreach (Collider2D collider in Object.FindObjectsOfType<Collider2D>(true))
            {
                //if (!HitboxConfig.showGeometry && collider.name.StartsWith(GEOMETRY_NAME))
                //    continue;

                // Make sure the collider is a valid type
                string colliderType = collider.GetIl2CppType().Name;
                if (colliderType != "BoxCollider2D" && colliderType != "CircleCollider2D" && colliderType != "PolygonCollider2D")
                {
                    continue;
                }

                if (_activeHitboxes.TryGetValue(collider.gameObject.GetInstanceID(), out LineRenderer line))
                {
                    // If the collider is already stored, just update the colors
                    SetColor(line, collider);
                }
                else
                {
                    // Move this out into helper function
                    var obj = new GameObject("Hitbox");
                    obj.transform.parent = collider.transform;
                    obj.transform.localPosition = Vector3.zero;

                    line = obj.AddComponent<LineRenderer>();
                    line.material = RendererMaterial;
                    line.useWorldSpace = false;

                    float width = 0.04f;
                    line.SetWidth(width, width);

                    switch (colliderType)
                    {
                        case "BoxCollider2D":
                            SetupBox(line, collider.Cast<BoxCollider2D>());
                            break;
                        case "CircleCollider2D":
                            SetupCircle(line, collider.Cast<CircleCollider2D>());
                            break;
                        case "PolygonCollider2D":
                            SetupPolygon(line, collider.Cast<PolygonCollider2D>());
                            break;
                    }
                    SetColor(line, collider);

                    _activeHitboxes.Add(collider.gameObject.GetInstanceID(), line);
                    newColliders++;
                }

                foundColliders.Add(collider.gameObject.GetInstanceID());
            }

            // Foreach collider in the list that wasn't found, remove it
            var destroyedColliders = new List<int>();
            foreach (int colliderId in _activeHitboxes.Keys)
            {
                if (!foundColliders.Contains(colliderId))
                    destroyedColliders.Add(colliderId);
            }
            foreach (int colliderId in destroyedColliders)
            {
                _activeHitboxes.Remove(colliderId);
            }

            // Log amounts and reset timer
            if (destroyedColliders.Count > 0)
                Main.CheatConsole.Log($"Removing {destroyedColliders.Count} old colliders");
            if (newColliders > 0)
                Main.CheatConsole.Log($"Adding {newColliders} new colliders");
            ResetTimer();
        }

        private void SetupBox(LineRenderer renderer, BoxCollider2D collider)
        {
            if (collider.size.x >= 15 || collider.size.y >= 15 || collider.size.x <= 0.1f || collider.size.y <= 0.1f)
                return;

            Vector2 halfSize = collider.size / 2f;
            Vector2 topLeft = new(-halfSize.x, halfSize.y);
            Vector2 topRight = halfSize;
            Vector2 bottomRight = new(halfSize.x, -halfSize.y);
            Vector2 bottomLeft = -halfSize;
            Vector2[] points = new Vector2[]
            {
                topLeft, topRight, bottomRight, bottomLeft, topLeft
            };

            renderer.positionCount = 5;
            for (int i = 0; i < points.Length; i++)
            {
                renderer.SetPosition(i, collider.offset + points[i]);
            }
        }

        private void SetupCircle(LineRenderer renderer, CircleCollider2D collider)
        {
            if (collider.radius >= 5 || collider.radius <= 0.1f)
                return;

            int steps = 100;
            float radius = collider.radius;

            renderer.positionCount = steps;
            for (int currentStep = 0; currentStep < steps; currentStep++)
            {
                float circumferenceProgress = (float)currentStep / (steps - 1);

                float currentRadian = circumferenceProgress * 2 * Mathf.PI;

                float xScaled = Mathf.Cos(currentRadian);
                float yScaled = Mathf.Sin(currentRadian);

                var currentPosition = new Vector2(radius * xScaled, radius * yScaled);
                renderer.SetPosition(currentStep, collider.offset + currentPosition);
            }
        }

        private void SetupPolygon(LineRenderer renderer, PolygonCollider2D collider)
        {
            if (collider.pathCount > 0)
            {
                var points = new List<Vector2>(collider.GetPath(0));
                if (points.Count > 0)
                    points.Add(points[0]);

                renderer.positionCount = points.Count;
                for (int i = 0; i < points.Count; i++)
                {
                    renderer.SetPosition(i, collider.offset + points[i]);
                }
            }
        }

        public void SetColor(LineRenderer renderer, Collider2D collider)
        {
            Color color;
            int order;
            if (!Main.CheatConsole.HitboxViewer.HitboxConfig.useColor)
            {
                color = Color.green;
                order = 0;
            }
            else if (!collider.isActiveAndEnabled)
            {
                color = Color.gray;
                order = 20;
            }
            else if (collider.name.StartsWith("GEO_"))
            {
                color = Color.green;
                order = 30;
            }
            else if (collider.transform.HasComponentInParent<PlayerPersistentComponent>())
            {
                color = Color.cyan;
                order = 100;
            }
            else if (collider.transform.HasComponentInParent<AliveEntity>())
            {
                color = Color.red;
                order = 80;
            }
            else if (collider.transform.GetComponent<AttackHit>() != null)
            {
                color = Color.magenta;
                order = 50;
            }
            else if (collider.isTrigger)
            {
                color = Color.blue;
                order = 60;
            }
            else
            {
                color = Color.yellow;
                order = 40;
            }

            renderer.SetColors(color, color);
            renderer.sortingOrder = order;
        }


        private void RemoveHitboxes()
        {
            foreach (LineRenderer hitbox in _activeHitboxes.Values)
            {
                if (hitbox != null && hitbox.gameObject != null)
                    Object.Destroy(hitbox.gameObject);
            }

            _activeHitboxes.Clear();
        }

        private void ResetTimer()
        {
            _currentDelay = 0;
        }
    }
}
