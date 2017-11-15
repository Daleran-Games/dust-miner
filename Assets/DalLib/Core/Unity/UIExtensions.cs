using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityEngine.UI
{
    public static class UIExtensions
    {
        
        public static void AutoColor (this ColorBlock colorBlock, Color32 newColor)
        {
            colorBlock.normalColor = newColor;
            colorBlock.highlightedColor = Color.Lerp(newColor, Color.white, 0.5f);
            colorBlock.pressedColor = Color.Lerp(newColor, Color.black, 0.5f);
            colorBlock.disabledColor = Color.Lerp(newColor, Color.clear, 0.5f);
            colorBlock.colorMultiplier = 1f;
            colorBlock.fadeDuration = 0.1f;
        }

        public static void ClampToRect (RectTransform transform, Rect bounds)
        {
            Vector3 minPosition = bounds.min - transform.rect.min;
            Vector3 maxPosition = bounds.max - transform.rect.max;

            transform.localPosition = new Vector3
                ( 
                Mathf.Clamp(transform.localPosition.x,minPosition.x,maxPosition.x),
                Mathf.Clamp(transform.localPosition.y, minPosition.y,maxPosition.y),
                transform.localPosition.z
                );
        }
       
    }

}
