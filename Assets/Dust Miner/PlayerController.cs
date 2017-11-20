using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;

namespace DaleranGames.DustMiner
{
    public class PlayerController : MonoBehaviour
    {
        public float Thrust = 20f;
        public float RotationFactor = 1f;

        public Vector2 desiredTranslation;
        public Vector2 desiredDirection;

        VectorPID steeringPID = new VectorPID(0.5f, 0.01f, 1.5f);

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
            
        }
    }
}

