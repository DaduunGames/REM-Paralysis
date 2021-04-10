using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameController : MonoBehaviour
{
    public Texture2D cursor;
    private void Start()
    {
        Cursor.SetCursor(cursor,Vector2.zero,CursorMode.Auto);
    }
}
