using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DaleranGames
{
    public static class TransformExtensions 
    {
        public static void SetRectTransformAnchorsAndPivot(this RectTransform rectTrans, Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot)
        {
            rectTrans.anchorMin = anchorMin;
            rectTrans.anchorMax = anchorMax;
            rectTrans.pivot = pivot;
        }

        public static void ResetTransform(this Transform transform)
        {
            transform.position = Vector3.one;
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        public static void SetAllChildrenActive(this Transform transform, bool value)
        {
            int childCount = transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(value);
            }
        }

        public static int GetActiveChildCount(this Transform transform)
        {
            int count = 0;
            foreach (Transform child in transform)
            {
                if (child.gameObject.activeSelf)
                {
                    count++;
                }
            }
            return count;
        }

        public static void SetX(this Transform transform, float x)
        {
            Vector3 newPosition = new Vector3(x, transform.position.y, transform.position.z);
            transform.position = newPosition;
        }

        public static void SetY(this Transform transform, float y)
        {
            Vector3 newPosition = new Vector3(transform.position.x, y, transform.position.z);
            transform.position = newPosition;
        }

        public static void SetZ(this Transform transform, float z)
        {
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, z);
            transform.position = newPosition;
        }

        public static Transform ClearChildren(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            return transform;
        }

        public static RectTransform ClearChildren(this RectTransform rectTransform)
        {
            foreach (RectTransform child in rectTransform)
            {
                GameObject.Destroy(child.gameObject);
            }
            return rectTransform;
        }

        public enum Axis { X, Y, Z }

        public static void Set2DRotation(this Transform transform, float angle, Space space = Space.World, Axis axis = Axis.Z)
        {
            var rotation = new Quaternion();
            float halfAngle = angle % (2 * Mathf.PI) * .5F;
            rotation[(int)axis] = Mathf.Sin(halfAngle);
            rotation.w = Mathf.Cos(halfAngle);

            switch (space)
            {
                case Space.Self:
                    transform.localRotation = rotation;
                    break;
                default:
                    transform.rotation = rotation;
                    break;
            }
        }

        public static float Get2DRotation(this Transform transform, Space space = Space.World, Axis axis = Axis.Z)
        {
            Quaternion rotation;
            switch (space)
            {
                case Space.Self:
                    rotation = transform.localRotation;
                    break;
                default:
                    rotation = transform.rotation;
                    break;
            }
            return Mathf.Asin(rotation[(int)axis] * Mathf.Sign(rotation.w)) * 2;
        }
    }
}