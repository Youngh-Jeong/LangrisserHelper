namespace LangrisserHelper.Shared
{
    public class Play
    {
        public ActionKind KeyCode { get; set; }
        public int[] Selected { get; set; } = new int[0];
    }
    public enum ActionKind
    {
        Ban,
        Pick
    }
}
