using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.Effects
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteColorAnimator : MonoBehaviour
    {
        [SerializeField]
        bool startOnAwake = true;
        [SerializeField]
        float startTime = 0f;

        [SerializeField]
        protected bool isPlaying;
        public bool IsPlaying
        {
            get { return isPlaying; }
            set { isPlaying = value; }
        }

        [SerializeField]
        protected float animationTime = 1f;
        public float AnimationTime
        {
            get { return animationTime; }
            set
            {
                currentTime = currentTime * (value / animationTime);
                animationTime = value;
            }
        }

        float currentTime = 0f;
        public float CurrentTime
        {
            get { return currentTime; }
            set { currentTime = Mathf.Clamp(value, 0f, animationTime); }
        }

        [SerializeField]
        protected WrapMode mode;
        public WrapMode Mode
        {
            get { return mode; }
            set { mode = value; }
        } 

        [SerializeField]
        protected Gradient color;
        public Gradient Color
        {
            get { return color; }
            set { color = value; }
        } 


        float pingPong = 1;
        SpriteRenderer sprite;

        // Use this for initialization
        void Start()
        {
            sprite = gameObject.GetRequiredComponent<SpriteRenderer>();

            if (startOnAwake)
            {
                isPlaying = true;
                currentTime = startTime;
            }
        }

        public void ToggleAnimation (bool state)
        {
            if (state)
            {
                currentTime = 0f;
                isPlaying = true;
            }else
            {
                currentTime = 0f;
                isPlaying = false;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (isPlaying)
                UpdateAnimation();
        }

        void UpdateAnimation()
        {
            EvaluateAnimation();
            sprite.color = color.Evaluate(currentTime / animationTime);
        }
        
        void EvaluateAnimation()
        {
            if (mode == WrapMode.Loop)
            {
                currentTime += Time.deltaTime;

                if (currentTime >= animationTime)
                {
                    currentTime = 0f;
                }
            }
            else if (mode == WrapMode.Once)
            {
                currentTime += Time.deltaTime;

                if (currentTime >= animationTime)
                {
                    currentTime = 0f;
                    isPlaying = false;
                }
            }
            else if (mode == WrapMode.ClampForever || mode == WrapMode.Clamp)
            {
                currentTime += Time.deltaTime;

                if (currentTime >= animationTime)
                {
                    currentTime = animationTime;
                    isPlaying = false;
                }
            } else
            {
                currentTime += Time.deltaTime * pingPong;

                if (currentTime >= animationTime)
                {
                    currentTime = animationTime;
                    pingPong = -1f;
                }
                else if (currentTime <= 0f)
                {
                    currentTime = 0f;
                    pingPong = 1f;
                }
            }

        }


    }
}

