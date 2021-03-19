using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BoardCreator))]
public class BoardCreatorInspector : Editor
{
    Vector2Int endPos = new Vector2Int(-1, -1);
    public BoardCreator current
    {
        get
        {
            return (BoardCreator)target;
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if(GUILayout.Button("Clear")){
            current.Clear();
            endPos.x = -1;
            endPos.y = -1;
        }
        if (GUILayout.Button("Grow"))
		    current.Grow();
        if (GUILayout.Button("Shrink"))
            current.Shrink();
        if (GUILayout.Button("Grow Area"))
            current.GrowArea();
        if (GUILayout.Button("Shrink Area"))
            current.ShrinkArea();
        GUILayout.Space(10);
        endPos = EditorGUILayout.Vector2IntField("EndPos", endPos);
        if (GUILayout.Button("Grow Flat Area"))
            if(endPos.x >= 0 && endPos.y >= 0)
                current.GrowFlatArea(endPos.x, endPos.y);
            else
                current.GrowFlatArea();
        if (GUILayout.Button("Shrink Flat Area"))
            if(endPos.x >= 0 && endPos.y >= 0)
                current.ShrinkFlatArea(endPos.x, endPos.y);
            else
                current.ShrinkFlatArea();
        GUILayout.Space(10);
        if (GUILayout.Button("Save"))
            current.Save();
        if (GUILayout.Button("Load"))
            current.Load();

        if (GUI.changed)
            current.UpdateMarker();
    }
}
