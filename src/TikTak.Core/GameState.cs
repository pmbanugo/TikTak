namespace TikTak.Core
{
    public class GameState
    {
        public bool  GameDrawn { get; set; }
        public bool GameOver { get; set; }
        public Winner Winner { get; set; }
    }
}
