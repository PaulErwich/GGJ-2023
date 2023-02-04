using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

public class VirtualMouse : MonoBehaviour
{
    private Mouse virtual_mouse;
    [SerializeField] private RectTransform cursor_transform;
    private float cursor_speed = 1000;

    private void OnEnable()
    {
        if (virtual_mouse == null)
        {
            virtual_mouse = (Mouse) InputSystem.AddDevice("VirtualMouse");
        }
        else if (!virtual_mouse.added)
        {
            InputSystem.AddDevice(virtual_mouse);
        }

        if (cursor_transform != null)
        {
            Vector2 position = cursor_transform.anchoredPosition;
            InputState.Change(virtual_mouse.position, position);
        }

        InputSystem.onAfterUpdate += updateVirtualMouse;
    }

    private void OnDisable()
    {
        InputSystem.onAfterUpdate -= updateVirtualMouse;
    }

    private void updateVirtualMouse()
    {
        if (virtual_mouse == null || Gamepad.current == null)
        {
            return;
        }

        //delta
        Vector2 stick_value = Gamepad.current.rightStick.ReadValue();
        stick_value *= cursor_speed * Time.deltaTime;
    }
}
