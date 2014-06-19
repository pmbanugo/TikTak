namespace TikTak.Core
{
    public class GameState
    {
        public bool  GameTied { get; set; }
        public bool GameOver { get; set; }
        public Winner Winner { get; set; }
    }
}
