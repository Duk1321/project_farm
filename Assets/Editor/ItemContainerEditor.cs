using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemContainter))]
public class ItemContainerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ItemContainter containter = target as ItemContainter;
        if(GUILayout.Button("Clear container"))
        {
            for (int i = 0; i < containter.slots.Count; i++)
            {
                containter.slots[i].Clear();
            }
        }
        DrawDefaultInspector();
    }
}
