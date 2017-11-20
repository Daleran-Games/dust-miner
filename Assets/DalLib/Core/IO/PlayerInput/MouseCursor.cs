using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

namespace DaleranGames.IO
{
    public class MouseCursor : Singleton<MouseCursor>
    {
        protected MouseCursor() { }

        public enum MouseRenderer
        {
            None = 0,
            Sprite = 1,
            Hardware =2
        }

        Image sprite;
        RectTransform trans;
        Canvas mouseCanvas;

        [Header("Dispay Settings")]
        [SerializeField]
        MouseRenderer renderMode = MouseRenderer.Sprite;
        [SerializeField]
        Color32 defaultColor = ColorExtensions.white;
        [SerializeField]
        Color32 highlightColor = ColorExtensions.gray;

        [Header("Information")]
        [ReadOnly]
        [SerializeField]
        Vector2 screenPosition;  //For DEBUG only!!
        public Vector2 ScreenPosition { get { return screenPosition; } }
        public Rect ScreenCanvasRect { get { return mouseCanvas.pixelRect; } }
        
        [ReadOnly]
        [SerializeField]
        Vector3 worldPosition;
        public Vector3 WorldPosition { get { return worldPosition; } }

        [Header("Orthographic Camera Settings")]
        [SerializeField]
        float zPlane = 0f;

        [Header("Prespective Camera Settings")]
        [SerializeField]
        Vector3 cursorPlanePoint = Vector3.zero;
        [SerializeField]
        Vector3 cursorPlaneNormal = Vector3.back;
        Plane cursorPlane; // plane on which the cursor move on in 3D space.
        Vector3 lastWorldPosition = Vector3.zero; // last successful raycasted world position for persepctive cameras

        bool isOrthographic = true;

        private void Awake()
        {
            sprite = gameObject.GetRequiredComponent<Image>();
            trans = gameObject.GetRequiredComponent<RectTransform>();
            mouseCanvas = GetComponentInParent<Canvas>();

            ChangeMouseRenderingMode(renderMode);

            if (Camera.main.orthographic)
            {
                isOrthographic = true;
            } else
            {
                isOrthographic = false;
                cursorPlane = new Plane(cursorPlaneNormal, cursorPlanePoint);
            }
        }
        // Update is called once per frame
        void Update()
        {
            MoveCursor(Input.mousePosition);
        }

        void MoveCursor(Vector3 pos)
        {
            transform.position = ClampToWindow(pos);
            screenPosition = transform.position;
            worldPosition = CalculateWorldPosition(pos);
        }

        void CheckIfOverUI()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                sprite.color = highlightColor;
            }
            else
                sprite.color = defaultColor;
        }

        Vector3 ClampToWindow(Vector3 newPos)
        {
            float x = newPos.x;
            float y = newPos.y;

            if (newPos.x < 0f)
                x = 0f;
            else if (newPos.x > mouseCanvas.pixelRect.width)
                x = mouseCanvas.pixelRect.width;

            if (newPos.y < 0f)
                y = 0f;
            else if (newPos.y > mouseCanvas.pixelRect.height)
                y = mouseCanvas.pixelRect.height;

            return new Vector3(x, y, newPos.z);
        }

        Vector3 CalculateWorldPosition (Vector3 mousePosition)
        {
            if (isOrthographic)
            {
                Vector3 pos = MainCamera.Instance.ScreenToWorldPoint(mousePosition);
                return new Vector3(pos.x, pos.y, zPlane);
            } else
            {
                Ray ray = MainCamera.Instance.ScreenPointToRay(mousePosition);
                float distance = 0;

                if (cursorPlane.Raycast(ray, out distance))
                    lastWorldPosition = ray.GetPoint(distance);

                return lastWorldPosition;

            }
        }

        public void ChangeMouseRenderingMode (MouseRenderer state)
        {
            switch (state)
            {
                case MouseRenderer.None:
                    Cursor.visible = false;
                    sprite.enabled = false;
                    break;
                case MouseRenderer.Sprite:
                    Cursor.visible = false;
                    sprite.enabled = true;
                    sprite.color = defaultColor;
                    break;
                case MouseRenderer.Hardware:
                    Cursor.visible = true;
                    sprite.enabled = false;
                    break;
            }
        }


    }
}