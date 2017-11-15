using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.IO
{
    public class Cursor3D : MonoBehaviour
    {
        [SerializeField]
        Vector3 cursorPlanePoint = Vector3.zero;
        [SerializeField]
        Vector3 cursorPlaneNormal = Vector3.back;

        Plane cursorPlane;

        private void Start()
        {
            cursorPlane = new Plane(cursorPlaneNormal, cursorPlanePoint);
        }

        void Update()
        {
            Ray ray = MainCamera.Instance.ScreenPointToRay(Input.mousePosition);

            float distance = 0;

            if (cursorPlane.Raycast(ray, out distance))
                transform.position = ray.GetPoint(distance);

        }
    }
}