using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.Effects
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Lower numbers appear farther away. 1 matches the the speed of the parent.")]
        Vector3 parallax = Vector3.one;

        Vector3 lastPosition;
        Transform parent;

        private void Start()
        {
            parent = transform.parent;

            if (parent == null)
                Debug.LogError("DL Error: Parallax requires a moving parent object. Usually this is the camera or player.");

            lastPosition = parent.position;
        }

        private void LateUpdate()
        {
            if (parent.hasChanged)
            {
                Vector3 change = parent.position - lastPosition;

                transform.localPosition = transform.localPosition - new Vector3(change.x * parallax.x, change.y * parallax.y, change.z * parallax.z);
                lastPosition = parent.position;
            }
        }
    }
}
