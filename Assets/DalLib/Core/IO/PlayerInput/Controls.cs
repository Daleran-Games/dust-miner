using System;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.IO
{
    public class Controls : Singleton<Controls>
    {
        protected Controls() { }

        public enum MouseButton
        {
            LMB = 0,
            RMB = 1,
            MMB = 2
        }

        public MouseEvent LMBClick;
        public MouseEvent RMBClick;
        public MouseEvent MMBClick;
        public ControlAxis Scroll;

        public ControlAxis Strafe;
        public ControlAxis Throttle;
        public ControlAxis Rotate;

        public ControlButton Fire;
        public ControlButton Ability;
        public ControlButton Brake;

        public ControlButton Submit;
        public ControlButton Cancel;
        public ControlButton Menu;

        // Use this for initialization
        void Awake()
        {
            LMBClick = new MouseEvent(MouseButton.LMB);
            RMBClick = new MouseEvent(MouseButton.RMB);
            MMBClick = new MouseEvent(MouseButton.MMB);
            Scroll = new ControlAxis("Mouse ScrollWheel");

            Strafe = new ControlAxis("Strafe");
            Throttle = new ControlAxis("Throttle");
            Rotate = new ControlAxis("Rotate");

            Fire = new ControlButton("Fire");
            Ability = new ControlButton("Ability");
            Brake = new ControlButton("Brake");

            Submit = new ControlButton("Submit");
            Cancel = new ControlButton("Cancel");
            Menu = new ControlButton("Menu");
        }

        // Update is called once per frame
        void Update()
        {
            LMBClick.CheckForClicks();
            RMBClick.CheckForClicks();
            MMBClick.CheckForClicks();

            Fire.CheckForPresses();
            Ability.CheckForPresses();
            Brake.CheckForPresses();

            Submit.CheckForPresses();
            Cancel.CheckForPresses();
            Menu.CheckForPresses();
        }

        public class MouseEvent
        {
            int button;
            public event Action MouseButtonPressed;
            public event Action MouseButtonUp;
            public event Action MouseButtonDown;

            public MouseEvent(MouseButton button)
            {
                this.button = (int)button;
            }

            public MouseButton GetButtonType()
            {
                return (MouseButton)button;
            }

            public bool IsPressed()
            {
                return Input.GetMouseButton(button);
            }

            public bool IsMouseButtonDown()
            {
                return Input.GetMouseButtonDown(button);
            }

            public bool IsMouseButtonUp()
            {
                return Input.GetMouseButtonUp(button);
            }

            public void CheckForClicks()
            {
                if (IsPressed() && MouseButtonPressed != null)
                    MouseButtonPressed();

                if (IsMouseButtonDown() && MouseButtonDown != null)
                    MouseButtonDown();

                if (IsMouseButtonUp() && MouseButtonUp != null)
                    MouseButtonUp();
            }
        }

        public class ControlToggle
        {
            bool keyState = false;
            string name;
            public event Action<bool> KeyToggled;

            public ControlToggle(string axisName, bool startingState)
            {
                name = axisName;
                keyState = startingState;
            }

            public string GetAxisName()
            {
                return name;
            }

            public bool GetToggleState()
            {
                return keyState;
            }

            public void CheckToggleState()
            {
                if (Input.GetButtonUp(name))
                {
                    keyState = !keyState;

                    if (KeyToggled != null)
                        KeyToggled(keyState);
                }
            }

        }

        public class ControlButton
        {
            string name;
            public event Action ControlButtonPressed;
            public event Action ControlButtonDown;
            public event Action ControlButtonUp;

            public ControlButton(string axisName)
            {
                name = axisName;
            }

            public string GetAxisName()
            {
                return name;
            }

            public bool IsPressed()
            {
                return Input.GetButton(name);
            }

            public bool IsDown()
            {
                return Input.GetButtonDown(name);
            }

            public bool IsUp()
            {
                return Input.GetButtonUp(name);
            }

            public void CheckForPresses()
            {
                if (IsDown() && ControlButtonDown != null)
                    ControlButtonDown();

                if (IsUp() && ControlButtonDown != null)
                    ControlButtonUp();

                if (IsPressed() && ControlButtonPressed != null)
                    ControlButtonPressed();
            }
        }

        public class ControlAxis
        {
            string axis;

            public ControlAxis(string axisName)
            {
                axis = axisName;
            }

            public string GetAxisName()
            {
                return axis;
            }

            public float GetAxisValue()
            {
                return Input.GetAxis(axis);
            }

            public float GetAxisRaw()
            {
                return Input.GetAxisRaw(axis);
            }

            public bool IsAxisInUse()
            {
                if (Input.GetAxis(axis) != 0)
                    return true;
                else
                    return false;
            }

            public bool IsPositiveAndInUse()
            {
                if (Input.GetAxis(axis) > 0 && IsAxisInUse())
                    return true;
                else
                    return false;
            }
        }
    }
}
