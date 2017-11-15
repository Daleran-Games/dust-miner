using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.Effects
{
    [RequireComponent(typeof(Camera))]
    public class PixelPerfectMoveZoomCamera : PixelPerfectZoomCamera
    {
        [Header("Move Settings")]
        [SerializeField]
        protected float panSpeed = 15f;
        [SerializeField]
        protected float panBorderThickness = 20f;
        [SerializeField]
        protected bool clampToBoundingArea = false;
        [SerializeField]
        protected Rect boundingArea = new Rect(0f, 0f, 10f, 10f);


        protected override void LateUpdate()
        {
            Vector2 moveDir = new Vector2();

            if (Input.GetAxis("Horizontal") > 0 || Input.mousePosition.x > Screen.width - panBorderThickness)
                moveDir.x = panSpeed;
            if (Input.GetAxis("Horizontal") < 0 || Input.mousePosition.x < panBorderThickness)
                moveDir.x = -panSpeed;
            if (Input.GetAxis("Vertical") > 0 || Input.mousePosition.y > Screen.height - panBorderThickness)
                moveDir.y = panSpeed;
            if (Input.GetAxis("Vertical") < 0 || Input.mousePosition.y < panBorderThickness)
                moveDir.y = -panSpeed;

            Vector3 newPosition = transform.position + (Vector3)moveDir.normalized * panSpeed * Time.deltaTime;

            if (clampToBoundingArea)
            {
                newPosition.x = Mathf.Clamp(newPosition.x, boundingArea.xMin, boundingArea.xMax);
                newPosition.y = Mathf.Clamp(newPosition.y, boundingArea.yMin, boundingArea.yMax);
            }

            transform.position = newPosition;
            base.LateUpdate();
        }

    }
}
