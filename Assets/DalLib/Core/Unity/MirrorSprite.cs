using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames
{
    public class MirrorSprite : MonoBehaviour
    {
        [SerializeField]
        protected bool trackParent;
        public bool TrackParent
        {
            get { return trackParent; }
            set { trackParent = value; CheckMirrorConditions(); }
        } 

        [SerializeField]
        protected Vector2 mirrorAxis;
        public Vector2 MirrorAxis
        {
            get { return mirrorAxis; }
            set { mirrorAxis = value; CheckMirrorConditions(); }
        } 

        [SerializeField]
        protected bool mirrorX;
        public bool MirrorX
        {
            get { return mirrorX; }
            set { mirrorX = value; CheckMirrorConditions(); }
        } 

        [SerializeField]
        protected bool mirrorXPositive;
        public bool MirrorXPositive
        {
            get { return mirrorXPositive; }
            set { mirrorXPositive = value; CheckMirrorConditions(); }
        } 

        [SerializeField]
        protected bool mirrorY;
        public bool MirrorY
        {
            get { return mirrorY; }
            set { mirrorY = value; CheckMirrorConditions(); }
        } 

        [SerializeField]
        protected bool mirrorYPositive;
        public bool MirrorYPositive
        {
            get { return mirrorYPositive; }
            set { mirrorYPositive = value; CheckMirrorConditions(); }
        } 

        Transform trackedTransform;
        SpriteRenderer sprite;

        // Use this for initialization
        void Start()
        {
            CheckMirrorConditions();
        }

        [ContextMenu("Check Mirror Conditions")]
        public void CheckMirrorConditions()
        {
            sprite = gameObject.GetRequiredComponent<SpriteRenderer>();
            trackedTransform = (trackParent) ? transform.parent : transform;

            if (mirrorX)
            {
                if (!mirrorXPositive && trackedTransform.localPosition.x < mirrorAxis.x)
                    sprite.flipX = true;
                else if (mirrorXPositive && trackedTransform.localPosition.x > mirrorAxis.x)
                    sprite.flipX = true;
            }

            if (mirrorY)
            {
                if (!mirrorYPositive && trackedTransform.localPosition.y < mirrorAxis.y)
                    sprite.flipY = true;
                else if (mirrorYPositive && trackedTransform.localPosition.y > mirrorAxis.y)
                    sprite.flipY = true;
            }
        }
    }
}

