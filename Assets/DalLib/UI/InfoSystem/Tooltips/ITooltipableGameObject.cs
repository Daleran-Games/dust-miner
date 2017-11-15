using UnityEngine.EventSystems;

namespace DaleranGames.UI
{
    public interface ITooltipableGameObject : IPointerEnterHandler, IPointerExitHandler, ITooltipable
    {
        void OnInfoUpdate(string newInfo);
    }
}