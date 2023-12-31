﻿using MelonLoader;
using System.Text;
using UnityEngine;

namespace BlasII.CheatConsole
{
    public class Main : MelonMod
    {
        public static CheatConsole CheatConsole { get; private set; }

        public override void OnLateInitializeMelon()
        {
            CheatConsole = new CheatConsole();
        }
    }

    public static class Extensions
    {
        // Recursive method that returns the entire hierarchy of an object
        public static string DisplayHierarchy(this Transform transform, int maxLevel, bool includeComponents)
        {
            return transform.DisplayHierarchy_INTERNAL(new StringBuilder(), 0, maxLevel, includeComponents).ToString();
        }

        public static StringBuilder DisplayHierarchy_INTERNAL(this Transform transform, StringBuilder currentHierarchy, int currentLevel, int maxLevel, bool includeComponents)
        {
            // Indent
            for (int i = 0; i < currentLevel; i++)
                currentHierarchy.Append('\t');

            // Add this object
            currentHierarchy.Append(transform.name);

            // Add components
            if (includeComponents)
            {
                currentHierarchy.Append(" - ");
                foreach (Component c in transform.GetComponents<Component>())
                    currentHierarchy.Append(c.ToString() + ", ");
            }
            currentHierarchy.AppendLine();

            // Add children
            if (currentLevel < maxLevel)
            {
                for (int i = 0; i < transform.childCount; i++)
                    currentHierarchy = transform.GetChild(i).DisplayHierarchy_INTERNAL(currentHierarchy, currentLevel + 1, maxLevel, includeComponents);
            }

            // Return output
            return currentHierarchy;
        }
    }
}