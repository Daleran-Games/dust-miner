using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DaleranGames.Effects
{
    [RequireComponent(typeof(Camera))]
    public class PixelPerfectFollowZoomCamera : PixelPerfectZoomCamera
    {
        [Header("Tracking Settings")]
        [SerializeField]
        protected Transform target;
        public Transform Target
        {
            get { return target; }
            set { target = value; }
        }

        [SerializeField]
        protected Vector3 offset = new Vector3(0f, 0f, -10f);

        // Update is called once per frame
        protected override void LateUpdate()
        {
            TrackTarget();
            base.LateUpdate();
        }

        protected virtual void TrackTarget()
        {
            if (target != null)
                transform.position = target.transform.position + offset;
        }
    }
}
