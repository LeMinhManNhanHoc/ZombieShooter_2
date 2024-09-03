using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoSingleton<InputManager>
{
    [SerializeField] private VirtualJoystickController movementJoystick;
    [SerializeField] private VirtualJoystickController lookJoystick;

    public Vector2 GetMovementDirection()
    {
        return movementJoystick.JoystickValue;
    }

    public Vector2 GetLookDirection()
    {
        return lookJoystick.JoystickValue;
    }
}
