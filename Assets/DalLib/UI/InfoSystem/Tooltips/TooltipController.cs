using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DaleranGames.IO;

namespace DaleranGames.UI
{
    public class TooltipController : Singleton<TooltipController>
    {
        protected TooltipController( ) { }

        [SerializeField]
        RectTransform canvasRect;
        Canvas canvas;

        [SerializeField]
        RectTransform tooltipRect;
        VerticalLayoutGroup verticalLayout;

        [SerializeField]
        TextMeshProUGUI tooltipText;

        [SerializeField]
        Vector2 offset = new Vector2(32f, -32f);

        [SerializeField]
        bool isActive = false;
        public bool IsActive { get { return isActive; } }

        [SerializeField]
        bool hexCursorOverride = false;
        public bool HexCursorOverride { get { return hexCursorOverride; } set { hexCursorOverride = value; } }

        // Use this for initialization
        void Start()
        {
            tooltipRect.gameObject.SetActive(false);
            isActive = false;
            hexCursorOverride = false;
            canvas = canvasRect.GetComponent<Canvas>();
            verticalLayout = tooltipRect.GetComponent<VerticalLayoutGroup>();
        }

        // Update is called once per frame
        void Update()
        {
            if (IsActive)
            {
                CheckTooltipAnchors();
            }
        }

        void CheckTooltipAnchors()
        {
            Vector3 mousePos = MouseCursor.Instance.ScreenPosition;
            float screenHalfX = Screen.width - tooltipRect.rect.width;
            float screenHalfY = tooltipRect.rect.height * 1.5f;

            if (mousePos.x >= screenHalfX) // Right Side
            {
                if (mousePos.y >= screenHalfY) // Upper Right
                {

                    SetRectAnchorsAndPicot(Vector2.one);
                    canvasRect.anchoredPosition = Vector2.zero;
                    tooltipRect.anchoredPosition = new Vector2(-offset.x, offset.y);
                    verticalLayout.childAlignment = TextAnchor.UpperRight;
                } else // Lower Right
                {
                    SetRectAnchorsAndPicot(Vector2.right);
                    canvasRect.anchoredPosition = Vector2.zero;
                    tooltipRect.anchoredPosition = new Vector2(-offset.x, -offset.y);
                    verticalLayout.childAlignment = TextAnchor.UpperRight;
                }
            } else // Left Side
            {
                if (mousePos.y >= screenHalfY) // Upper Left
                {
                    SetRectAnchorsAndPicot(Vector2.up);
                    canvasRect.anchoredPosition = Vector2.zero;
                    tooltipRect.anchoredPosition = offset;
                    verticalLayout.childAlignment = TextAnchor.UpperLeft;
                }
                else // Lower Left
                {
                    SetRectAnchorsAndPicot( Vector2.zero);
                    canvasRect.anchoredPosition = Vector2.zero;
                    tooltipRect.anchoredPosition = new Vector2(offset.x, -offset.y);
                    verticalLayout.childAlignment = TextAnchor.UpperLeft;
                }
            }

        }

        void SetRectAnchorsAndPicot (Vector2 vec)
        {
            canvasRect.anchorMin = vec;
            canvasRect.anchorMax = vec;
            canvasRect.pivot = vec;
            tooltipRect.anchorMin = vec;
            tooltipRect.anchorMax = vec;
            tooltipRect.pivot = vec;
            
        }

        public void ShowTooltip(string text)
        {
                isActive = true;
                HexCursorOverride = true;
                tooltipRect.gameObject.SetActive(true);
                tooltipText.text = text;
        }

        public void HideTooltip()
        {
                isActive = false;
                HexCursorOverride = false;
                tooltipRect.gameObject.SetActive(false);

        }
    }
}

