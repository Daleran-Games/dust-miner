using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.IO
{
    public class Cursor2D : MonoBehaviour
    {
        [SerializeField]
        Transform target;
        [SerializeField]
        bool showCursor = true;
        [SerializeField]
        Color32 defaultColor = ColorExtensions.white;
        [SerializeField]
        [Range(0f,1f)]
        float distanceScaler = 1f;
        [SerializeField]
        float zLevel = -9;

        SpriteRenderer cursorRenderer;
        
        private void Awake()
        {
            cursorRenderer = gameObject.GetComponent<SpriteRenderer>();
        }


        // Use this for initialization
        void Start()
        {
            if (cursorRenderer != null)
            {
                if (showCursor)
                    ToggleCursorSprite(true);
                else
                    ToggleCursorSprite(false);
                    
            }  
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 mousePos = MainCamera.Instance.ScreenToWorldPoint(Input.mousePosition);

            if (distanceScaler < 1)
            {
                Vector3 newPos = Vector3.MoveTowards(target.transform.position, mousePos, Vector3.Distance(target.transform.position, mousePos) * distanceScaler);
                transform.position = new Vector3(newPos.x, newPos.y, zLevel);
            }
            else
                transform.position = new Vector3(mousePos.x, mousePos.y, zLevel);
        }

        public void ToggleCursorSprite (bool active)
        {
            if (active && cursorRenderer != null)
            {
                cursorRenderer.color = defaultColor;
                cursorRenderer.enabled = true;
                showCursor = true;
            }
            else if (active == false && cursorRenderer != null)
            {
                cursorRenderer.enabled = false;
                showCursor = false;
            }
        }
    }
}
