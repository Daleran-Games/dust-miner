using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;

namespace DaleranGames.SquadronLeader
{
    public class TargetThrottleSteering : MonoBehaviour
    {
        
        [Header("Steering Settings")]
        [SerializeField]
        [Range(0f,1f)]
        float stopCursorDistance = 0.1f;
        float stopPixels;
        [SerializeField]
        [Range(0f, 1f)]
        float maxSpeedCursorDistance = 0.8f;
        float maxPixels;

        [Header("Steering Information")]
        [ReadOnly]
        [SerializeField]
        float _throttle;
        public float Throttle { get { return _throttle; } }
        [ReadOnly]
        [SerializeField]
        Vector2 _desiredVelocity;
        public Vector2 DesiredVelocity { get { return _desiredVelocity; } }
        [ReadOnly]
        [SerializeField]
        Vector2 _steering;
        public Vector2 SteeringForce { get { return _steering; } }

        Rect screenSpace;
        Rigidbody2D rb;
        Ship ship;

        private void Awake()
        {
            if (stopCursorDistance >= maxSpeedCursorDistance)
                Debug.Log("Error: Stop Cursor Distance cannot be greater than the max speed cursor distance");

            rb = gameObject.GetRequiredComponent<Rigidbody2D>();
            ship = gameObject.GetRequiredComponent<Ship>();
        }

        // Use this for initialization
        void Start()
        {
            SetUpScreenLimits();
        }

        // Update is called once per frame
        void Update()
        {
            _desiredVelocity = CalculateDesiredVelocity();
        }

        private void FixedUpdate()
        {
            _steering = CalculateSteeringForce();
            MoveShipTowardsSteering();
            RotateShipTovelocity();
        }

        void SetUpScreenLimits()
        {
            screenSpace = MouseCursor.Instance.ScreenCanvasRect;
            stopPixels = (screenSpace.height * 0.5f) * stopCursorDistance;
            maxPixels = (screenSpace.height * 0.5f) * maxSpeedCursorDistance;
        }

        Vector2 CalculateDesiredVelocity()
        {
            Vector2 targetDirection = MouseCursor.Instance.ScreenPosition - (Vector2)MainCamera.Instance.WorldToScreenPoint(transform.position);
            float targetDistance = targetDirection.magnitude;

            if (targetDistance <= stopPixels)
                _throttle = 0f;
            else if (targetDistance > maxPixels)
                _throttle = 1f;
            else
                _throttle = (targetDistance - stopPixels) / (maxPixels - stopPixels);

            float desiredSpeed = ship.MaxSpeed * _throttle;

            return targetDirection.normalized * desiredSpeed;

        }

        Vector2 CalculateSteeringForce()
        {
            Vector2 steer = _desiredVelocity - rb.velocity;
            return Vector2.ClampMagnitude(steer, ship.MaxSteeringForce);
        }

        void MoveShipTowardsSteering()
        {
            rb.AddForce(_steering);
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, ship.MaxSpeed);
        }

        void RotateShipTovelocity()
        {
            if (rb.velocity.magnitude > 0.1f)
            {
                float angle = Vector2.SignedAngle(transform.up, rb.velocity);
                rb.MoveRotation(rb.rotation + angle);
            }
        }

    }
}
