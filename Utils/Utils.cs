using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static TextMesh CreateWorldText(string text, 
        Transform parent = null, 
        Vector3 localPosition = default, 
        int fontsize = 40) 
    { 
        var color = Color.white;
        return CreateWorldText(text,parent,localPosition,
            fontsize);
    }

    public static TextMesh CreateWorldText(Transform parent,
        string text, Vector3 localPosition,int fontsize) 
    {
        GameObject go = new("World_Text",typeof(TextMesh));
        Transform transform = go.transform;
        transform.SetParent(parent,false);
        transform.localPosition = localPosition;
        TextMesh textMesh = go.GetComponent<TextMesh>();
        textMesh.text = text;
        return textMesh;
    }

    public static void CreateMarker(Vector3 position) 
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        go.transform.localPosition = position;
        go.name = $"{position.x} - {position.z}";
    }
}
