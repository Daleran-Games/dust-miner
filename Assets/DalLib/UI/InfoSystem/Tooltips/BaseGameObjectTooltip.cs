using UnityEngine;
using UnityEngine.EventSystems;

namespace DaleranGames.UI
{
    public abstract class BaseGameObjectTooltip : MonoBehaviour, ITooltipableGameObject
    {
        private bool isActive = false;
        public bool IsActive { get { return isActive; } }

        public abstract string Info { get; }

        public virtual void OnInfoUpdate(string newInfo)
        {
            
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            TooltipController.Instance.ShowTooltip(Info);
            isActive = true;
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            TooltipController.Instance.HideTooltip();
            isActive = false;
        }



    }
}
