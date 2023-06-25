using System;
using UnityEngine;

public class CursorSetter : MonoBehaviour
{
    [SerializeField] private Texture2D texture;
    private Vector2 hotspot;
    
    private void Awake()
    {
        hotspot = new Vector2(texture.width / 2, texture.height / 2);
        Cursor.SetCursor(texture, hotspot, CursorMode.Auto);
    }
}
