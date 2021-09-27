using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PrefabTile))]
public class CustomTileEditor : Editor
{

    [MenuItem("Assets/Create/2D/Tiles/PrefabTile (From Selection)", false, 20)]
    private static void CreateFromTextures()
    {
        var objects = Selection.GetFiltered<GameObject>(SelectionMode.Assets);

        foreach (var obj in objects)
        {
        }
    }

    [MenuItem("Assets/Create/2D/Tiles/PrefabTile (From Selection)", true, 20)]
    private static bool CreateFromTeturesValidate()
    {
        return Selection.GetFiltered<GameObject>(SelectionMode.Assets).Length > 0;
    }
    
}