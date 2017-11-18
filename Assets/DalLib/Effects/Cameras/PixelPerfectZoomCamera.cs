using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.Effects
{
    [RequireComponent(typeof(Camera))]
    public class PixelPerfectZoomCamera : MonoBehaviour
    {
        [Header("Zoom Settings")]
        [SerializeField]
        protected int pixelsPerUnit = 32;

        [SerializeField]
        protected List<float> scales;
        protected int indexAtScaleOne;
        public int IndexAtScaleOne { get { return indexAtScaleOne; } }

        protected Camera cam;
        protected float[] orthoSizes;
        protected int sizeIndex = 0;
        public virtual int SizeIndex
        {
            get { return sizeIndex; }
            protected set
            {
                sizeIndex = value;

                if (CameraZoomChange != null)
                    CameraZoomChange(sizeIndex);
            }
        }
        public event System.Action<int> CameraZoomChange;


        // Use this for initialization
        protected virtual void Start()
        {
            cam = gameObject.GetRequiredComponent<Camera>();
            orthoSizes = BuildSizeArray();
            cam.orthographicSize = orthoSizes[sizeIndex];
        }

        // Update is called once per frame
        protected virtual void LateUpdate()
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
                ZoomCameraIn();
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
                ZoomCameraOut();
        }

        protected virtual void ZoomCameraIn()
        {
            if (SizeIndex < orthoSizes.Length - 1)
            {
                SizeIndex++;
                cam.orthographicSize = orthoSizes[sizeIndex];
            }
        }

        protected virtual void ZoomCameraOut()
        {
            if (SizeIndex > 0)
            {
                SizeIndex--;
                cam.orthographicSize = orthoSizes[sizeIndex];
            }
        }

        protected virtual float CalculateOrthographicSize(float scale)
        {
            return (((float)Screen.height) / ((float)scale * (float)pixelsPerUnit)) * 0.5f;
        }

        protected virtual float[] BuildSizeArray()
        {
            List<float> customSizes = new List<float>();

            for (int i = 0; i < scales.Count; i++)
            {
                customSizes.Add(CalculateOrthographicSize(scales[i]));
            }
            customSizes.Sort();
            customSizes.Reverse();
            float scaleOne = CalculateOrthographicSize(1f);

            for (int i = 0; i < customSizes.Count; i++)
            {
                if (customSizes[i] == scaleOne)
                {
                    indexAtScaleOne = i;
                }
            }

            return customSizes.ToArray();
        }
    }
}