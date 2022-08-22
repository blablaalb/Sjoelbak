using System.Collections.Generic;
using UnityEngine;
using System;

public class KeyboardInput : Singleton<KeyboardInput>
{
    public event Action<Vector2> Touched;
    public event Action<Vector2> Moved;
    public event Action<Vector2> Left;

    internal void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Touched?.Invoke(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            Moved?.Invoke(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Left?.Invoke(Input.mousePosition);
        }
    }
}
