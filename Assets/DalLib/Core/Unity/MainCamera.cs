using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames
{
    public class MainCamera : MonoBehaviour
    {
        [SerializeField]
        static Camera instance;
        public static Camera Instance { get { return instance; } }

        // Use this for initialization
        void Awake()
        {
            instance = Camera.main;
        }

    }

}
