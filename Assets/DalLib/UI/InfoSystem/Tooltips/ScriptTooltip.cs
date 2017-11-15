namespace DaleranGames.UI
{
    public class ScriptTooltip : BaseGameObjectTooltip
    {
        string text;
        public string Text
        {
            get { return text; }
            set
            {
                text = value;

                if (IsActive)
                    TooltipController.Instance.ShowTooltip(Info);
            }
        }

        public override string Info { get { return text; } }
    }
}
