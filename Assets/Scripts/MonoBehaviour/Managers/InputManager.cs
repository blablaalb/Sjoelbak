using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : Singleton<InputManager>
{
    public event Action<Vector2> Touched;
    public event Action<Vector2> Moved;
    public event Action<Vector2> Left;

    internal void Start()
    {
        TouchInput.Instance.Touched += OnTouched;
        TouchInput.Instance.Moved += OnMoved;
        TouchInput.Instance.Left += OnLeft;

        KeyboardInput.Instance.Touched += OnTouched;
        KeyboardInput.Instance.Moved += OnMoved;
        KeyboardInput.Instance.Left += OnLeft;
    }

    override protected void OnDestroy()
    {
        if (TouchInput.Instance != null)
        {
            TouchInput.Instance.Touched -= OnTouched;
            TouchInput.Instance.Moved -= OnMoved;
            TouchInput.Instance.Left -= OnLeft;
        }
        if (KeyboardInput.Instance != null)
        {
            KeyboardInput.Instance.Touched -= OnTouched;
            KeyboardInput.Instance.Moved -= OnMoved;
            KeyboardInput.Instance.Left -= OnLeft;
        }
        base.OnDestroy();
    }

    private void OnTouched(Vector2 position)
    {
        Touched?.Invoke(position);
    }

    private void OnMoved(Vector2 position)
    {
        Moved?.Invoke(position);
    }

    private void OnLeft(Vector2 position)
    {
        Left?.Invoke(position);
    }
}
