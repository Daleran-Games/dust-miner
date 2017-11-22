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

        [Header("MouseControls")]
        [SerializeField]
        MouseEvent lmbClick;
        public static MouseEvent LMBClick { get { return Instance.lmbClick; } }
        [SerializeField]
        MouseEvent rmbClick;
        public static MouseEvent RMBClick { get { return Instance.rmbClick; } }
        [SerializeField]
        MouseEvent mmbClick;
        public static MouseEvent MMBClick { get { return Instance.mmbClick; } }
        [SerializeField]
        ControlAxis scroll;
        public static ControlAxis Scroll { get { return Instance.scroll; } }
        [SerializeField]
        Vector2 mouseVector;
        public static Vector2 MouseVector { get { return Instance.mouseVector; } }
        Vector2 screenCenter;

        [Header("Movement Controls")]
        [SerializeField]
        ControlAxis strafe;
        public static ControlAxis Strafe { get { return Instance.strafe; } }
        [SerializeField]
        ControlAxis throttle;
        public static ControlAxis Throttle { get { return Instance.throttle; } }
        [SerializeField]
        ControlAxis rotate;
        public static ControlAxis Rotate { get { return Instance.rotate; } }

        [Header("Actions Controls")]
        [SerializeField]
        ControlButton fire;
        public static ControlButton Fire { get { return Instance.fire; } }
        [SerializeField]
        ControlButton ability;
        public static ControlButton Ability { get { return Instance.ability; } }
        [SerializeField]
        ControlToggle rcs;
        public static ControlToggle RCS { get { return Instance.rcs; } }

        [Header("Menu Controls")]
        [SerializeField]
        ControlButton submit;
        public static ControlButton Submit { get { return Instance.submit; } }
        [SerializeField]
        ControlButton cancel;
        public static ControlButton Cancel { get { return Instance.cancel; } }
        [SerializeField]
        ControlButton menu;
        public static ControlButton Menu { get { return Instance.menu; } }

        // Use this for initialization
        void Awake()
        {
            lmbClick = new MouseEvent(MouseButton.LMB);
            rmbClick = new MouseEvent(MouseButton.RMB);
            mmbClick = new MouseEvent(MouseButton.MMB);
            scroll = new ControlAxis("Mouse ScrollWheel");

            screenCenter = new Vector2(Screen.width/2,Screen.height/2);

            strafe = new ControlAxis("Strafe");
            throttle = new ControlAxis("Throttle");
            rotate = new ControlAxis("Rotate");

            fire = new ControlButton("Fire");
            ability = new ControlButton("Ability");
            rcs = new ControlToggle("RCS", true);

            submit = new ControlButton("Submit");
            cancel = new ControlButton("Cancel");
            menu = new ControlButton("Menu");
        }

        // Update is called once per frame
        void Update()
        {
            lmbClick.CheckForClicks();
            rmbClick.CheckForClicks();
            mmbClick.CheckForClicks();

            mouseVector = (Vector2)Input.mousePosition - screenCenter;

            fire.CheckForPresses();
            ability.CheckForPresses();
            rcs.CheckToggleState();

            submit.CheckForPresses();
            cancel.CheckForPresses();
            menu.CheckForPresses();
        }

        [System.Serializable]
        public class MouseEvent
        {
            [SerializeField]
            [ReadOnly]
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
        [Serializable]
        public class ControlToggle
        {
            [SerializeField]
            bool toggleState = false;
            public bool ToggleState { get { return toggleState; } }

            [SerializeField]
            string axisName;
            public string AxisName { get { return axisName; } }

            public event Action<bool> KeyToggled;

            public ControlToggle(string axisName, bool startingState)
            {
                this.axisName = axisName;
                toggleState = startingState;
            }

            public void CheckToggleState()
            {
                if (Input.GetButtonUp(axisName))
                {
                    toggleState = !toggleState;

                    if (KeyToggled != null)
                        KeyToggled(toggleState);
                }
            }

        }
        [Serializable]
        public class ControlButton
        {
            [SerializeField]
            string axisName;
            public string AxisName { get { return axisName; } }

            public event Action ControlButtonPressed;
            public event Action ControlButtonDown;
            public event Action ControlButtonUp;

            public ControlButton(string axisName)
            {
                this.axisName = axisName;
            }

            public bool IsPressed()
            {
                return Input.GetButton(axisName);
            }

            public bool IsDown()
            {
                return Input.GetButtonDown(axisName);
            }

            public bool IsUp()
            {
                return Input.GetButtonUp(axisName);
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
        [Serializable]
        public class ControlAxis
        {
            [SerializeField]
            string axisName;
            public string AxisName { get { return axisName; } }

            public float Value { get { return Input.GetAxis(axisName); } }
            public float RawValue { get { return Input.GetAxisRaw(axisName); } }
            public bool IsInUse
            {
                get
                {
                    if (Input.GetAxis(axisName) != 0)
                        return true;
                    else
                        return false;
                }
            }

            public bool IsPositiveAndInUse
            {
                get
                {
                    if (Input.GetAxis(axisName) > 0 && IsInUse)
                        return true;
                    else
                        return false;
                }
            }

            public ControlAxis(string axisName)
            {
                this.axisName = axisName;
            }


        }
    }
}
