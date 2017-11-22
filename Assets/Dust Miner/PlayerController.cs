using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;

namespace DaleranGames.DustMiner
{
    public class PlayerController : MonoBehaviour
    {
        public Vector2 Thrust = new Vector2(5f, 10f);
        public float RotationSpeed = 90f;
        public float RotationDeadZone = 0.8f;
        public float RotationFactor = 1f;

        public Vector2 desiredTranslation;
        public Vector2 desiredDirection;

        public Vector2 thrustDirection;

        Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            desiredDirection = Controls.MouseVector.normalized;
            desiredTranslation = new Vector2(Controls.Strafe.Value, Controls.Throttle.Value).normalized;
        }

        private void FixedUpdate()
        {
            Rotate();
            Translate();
        }

        void Rotate()
        {
            if (Vector2.Angle(transform.up, desiredDirection) > RotationDeadZone)
            {
                if (Vector2.SignedAngle(transform.up, desiredDirection) > 0f)
                {
                    rb.angularVelocity = RotationSpeed;
                }
                else
                    rb.angularVelocity = -RotationSpeed;
            }
            else
                rb.angularVelocity = 0f;
        }

        void Translate()
        {
            Vector2 localVelocity = (Vector2)transform.InverseTransformDirection(rb.velocity).normalized;


            //rb.AddRelativeForce(new Vector2(thrustDirection.x*Thrust.x,thrustDirection.y*Thrust.y));
        }
    }
}

