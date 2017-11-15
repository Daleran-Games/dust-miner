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
        protected int maxScale = 10;

        [SerializeField]
        protected bool AllowCustomScales = false;
        [SerializeField]
        protected List<float> customScales;
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
            float[] sizes = new float[maxScale];
            for (int i = 0; i < sizes.Length; i++)
            {
                sizes[i] = CalculateOrthographicSize(i + 1);
            }

            if (AllowCustomScales)
            {
                List<float> customSizes = new List<float>(sizes);

                for (int i = 0; i < customScales.Count; i++)
                {
                    customSizes.Add(CalculateOrthographicSize(customScales[i]));
                }
                customSizes.Sort();
                customSizes.Reverse();
                float scaleOne = CalculateOrthographicSize(1f);

                for (int i=0; i< customSizes.Count; i++)
                {
                    if (customSizes[i] == scaleOne)
                    {
                        indexAtScaleOne = i;
                    }
                }

                return customSizes.ToArray();
            }
            else
            {
                indexAtScaleOne = 0;
                return sizes;
            }
        }
    }
}