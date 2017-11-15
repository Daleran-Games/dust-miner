using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.IO
{
    public class MouseCursorFollower : MonoBehaviour
    {

        [SerializeField]
        protected Vector3 offset;
        public Vector3 Offset
        {
            get { return offset; }
            set { offset = value; }
        } 

        MouseCursor cursor;

        // Use this for initialization
        void Start()
        {
            cursor = MouseCursor.Instance;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            transform.position = cursor.WorldPosition + offset;
        }
    }
}