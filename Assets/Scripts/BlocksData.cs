using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;
using UnityEditor;
//using UnityEngine;


//[CreateAssetMenu(menuName = "Mining blocks/Block")]
public class BlocksData : ScriptableObject
{
    public string DisplayName;
    public int blockID;
    public Sprite Icon;
    public GameObject Prefab;
}

#if UNITY_EDITOR
[CustomEditor(typeof(BlocksData))]
public class BuildingDataEditor : Editor
{
    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        var data = (BlocksData)target;

        if (data == null || data.Icon == null) return null;
        var texture = new Texture2D(width, height);
        EditorUtility.CopySerialized(data.Icon.texture, texture);
        return texture;
    }
}
#endif