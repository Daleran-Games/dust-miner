using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DaleranGames.IO;

namespace DaleranGames.SquadronLeader
{
    public class SpeedCursor : MonoBehaviour
    {
        [Header("Position Settings")]
        [SerializeField]
        float distanceFromCursor = 26f;
        [SerializeField]
        TargetThrottleSteering trackedShip;

        [Header("Speed Indicators")]
        [SerializeField]
        Sprite stopSprite;
        [SerializeField]
        Color32 stopColor = ColorExtensions.white;
        [SerializeField]
        Sprite slowSprite;
        [SerializeField]
        Color32 slowColor = ColorExtensions.white;
        [SerializeField]
        Sprite medSprite;
        [SerializeField]
        Color32 medColor = ColorExtensions.white;
        [SerializeField]
        Sprite maxSprite;
        [SerializeField]
        Color32 maxColor = ColorExtensions.white;

        Image sprite;
        RectTransform rect;

        // Use this for initialization
        void Start()
        {
            sprite = gameObject.GetRequiredComponent<Image>();
            rect = gameObject.GetRequiredComponent<RectTransform>();
        }

        // Update is called once per frame
        void Update()
        {
            MoveAndRotate();
            CheckSpriteAndColor();
        }

        void MoveAndRotate()
        {
            Vector2 shipToCursor = trackedShip.transform.position - MouseCursor.WorldPosition;
            rect.localPosition = -shipToCursor.normalized * distanceFromCursor;


            float angle = Vector2.SignedAngle(transform.up, -shipToCursor);
            rect.Rotate(0f, 0f, angle);
        }

        void CheckSpriteAndColor()
        {
            if (trackedShip.Throttle <= 0)
            {
                sprite.sprite = stopSprite;
                sprite.color = stopColor;
            } else if (trackedShip.Throttle > 0f && trackedShip.Throttle <= 0.5f)
            {
                sprite.sprite = slowSprite;
                sprite.color = slowColor;
            } else if (trackedShip.Throttle > 0.5f && trackedShip.Throttle < 1f)
            {
                sprite.sprite = medSprite;
                sprite.color = medColor;
            } else
            {
                sprite.sprite = maxSprite;
                sprite.color = maxColor;
            }


        }
    }
}


