﻿//
// Copyright (c) Brian Hernandez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.
//

using UnityEngine;

/// <summary>
/// Class specifically to deal with input.
/// </summary>
public class ShipInput : MonoBehaviour
{
    [Range(0.1f, 100f)]
    public float sensitivity = 100f;
    [Tooltip("When true, the mouse and mousewheel are used for ship input and A/D can be used for strafing like in many arcade space sims.\n\nOtherwise, WASD/Arrows/Joystick + R/T are used for flying, representing a more traditional style space sim.")]
    public bool useMouseInput = true;
    [Tooltip("When using Keyboard/Joystick input, should roll be added to horizontal stick movement. This is a common trick in traditional space sims to help ships roll into turns and gives a more plane-like feeling of flight.")]
    public bool addRoll = true;

    [Space]

    [HideInInspector]
    public float pitch;
    [HideInInspector]
    public float yaw;
    [HideInInspector]
    public float roll;
    [HideInInspector]
    public float strafe;
    [HideInInspector]    public float throttle;
    [Range(0.5f, 2)]
    public float sprint_throttle;

    // How quickly the throttle reacts to input.
    private const float THROTTLE_SPEED = 0.5f;

    // Keep a reference to the ship this is attached to just in case.
    private Ship ship;
    private float increase = 0f;

    private void Awake()
    {
        ship = GetComponent<Ship>();
    }

    private void Update()
    {
        if (useMouseInput)
        {
            strafe = Input.GetAxis("Horizontal");
            SetStickCommandsUsingMouse();
            UpdateMouseWheelThrottle();
            UpdateKeyboardThrottle(KeyCode.W, KeyCode.S);
        }
        else
        {            
            pitch = Input.GetAxis("Vertical");
            pitch *= sensitivity / 100f;
            //yaw = Input.GetAxis("Horizontal");
            //yaw *= sensitivity / 100f;

            strafe = 0.0f;
            UpdateKeyboardThrottle(KeyCode.R, KeyCode.F);
        }
        if (addRoll)
        {
            roll = -Input.GetAxis("Horizontal") * 0.5f;
            roll *= sensitivity / 100f;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            EnableSprint(true);
        }else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            EnableSprint(false);
        }
    }

    public void EnableSprint(bool enable)
    {
        if(enable)
        {
            increase = sprint_throttle;
        }else
        {
            increase = 0f;
        }
    }

    /// <summary>
    /// Freelancer style mouse controls. This uses the mouse to simulate a virtual joystick.
    /// When the mouse is in the center of the screen, this is the same as a centered stick.
    /// </summary>
    private void SetStickCommandsUsingMouse()
    {
        Vector3 mousePos = Input.mousePosition;

        // Figure out most position relative to center of screen.
        // (0, 0) is center, (-1, -1) is bottom left, (1, 1) is top right.      
        pitch = (mousePos.y - (Screen.height * 0.5f)) / (Screen.height* 0.5f);
        pitch *= sensitivity / 100f;
        yaw = (mousePos.x - (Screen.width * 0.5f)) / (Screen.width * 0.5f);
        yaw *= sensitivity / 100f;

        // Make sure the values don't exceed limits.
        pitch = -Mathf.Clamp(pitch, -1.0f, 1.0f);
        yaw = Mathf.Clamp(yaw, -1.0f, 1.0f);
    }

    /// <summary>
    /// Uses R and F to raise and lower the throttle.
    /// </summary>
    private void UpdateKeyboardThrottle(KeyCode increaseKey, KeyCode decreaseKey)
    {
        float target = throttle;

        if (Input.GetKey(increaseKey))
            target = 1.0f;
        else if (Input.GetKey(decreaseKey))
            target = 0.0f;
    
        throttle = Mathf.MoveTowards(throttle, target, Time.deltaTime * THROTTLE_SPEED);
    }

    /// <summary>
    /// Uses the mouse wheel to control the throttle.
    /// </summary>
    private void UpdateMouseWheelThrottle()
    {
        throttle += Input.GetAxis("Mouse ScrollWheel");
        throttle = Mathf.Clamp(throttle, 0.0f, 1.0f);
    }
}