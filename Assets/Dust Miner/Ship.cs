using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.SquadronLeader
{
    public class Ship : MonoBehaviour
    {
        [Header("Information")]
        [SerializeField]
        string _pilotName = "Pilot";
        public string PilotName { get { return _pilotName; } }
        [SerializeField]
        string _className = "Default Ship Class";
        public string ClassName { get { return _className; } }
        [SerializeField]
        ShipType _type = ShipType.Fighter;
        public ShipType Type { get { return _type; } }


        [Header("Movement")]
        [SerializeField]
        float _maxSpeed = 15f;
        public float MaxSpeed { get { return _maxSpeed; } }
        [SerializeField]
        float _maxSteeringForce = 12f;
        public float MaxSteeringForce { get { return _maxSteeringForce; } }


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

    public enum ShipType
    {
        Fighter,
        Capital
    }
}