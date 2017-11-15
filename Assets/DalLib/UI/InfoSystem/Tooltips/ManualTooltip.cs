using UnityEngine;

namespace DaleranGames.UI
{
    public class ManualTooltip : BaseGameObjectTooltip
    {

        [SerializeField]
        [TextArea(3, 8)]
        string text;
        public override string Info { get { return text; } }

    }
}
