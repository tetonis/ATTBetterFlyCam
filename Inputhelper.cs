using UnityEngine;
using UnityEngine.InputSystem;

public static class InputHelper
{
    //Credit to shadow for saving me 3 minutes
    public static bool GetKey(Key key)
    {
        return Keyboard.current != null && Keyboard.current[key].isPressed;
    }

    public static bool GetKeyDown(Key key)
    {
        return Keyboard.current != null && Keyboard.current[key].wasPressedThisFrame;
    }

    public static bool GetKeyUp(Key key)
    {
        return Keyboard.current != null && Keyboard.current[key].wasReleasedThisFrame;
    }

    public static bool GetMouseLockState()
    {
        return Cursor.lockState == CursorLockMode.Locked;
    }

    public static void SetMouseLockState(bool lockState)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (lockState)
        {
            //You ain't goin nowhere
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}