using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTak
{
    public static class CompositionRoot
    {
        public static GameUI ComposeStartupForm()
        {
            IBoard board = new Board();
            IGameManager gameManager = new GameManager(board);

            IGameService gameService = new GameService(gameManager);

            IComputerPlayer computerPlayer = new ComputerPlayer(gameService);
            IHumanPlayer humanPlayer = new HumanPlayer(gameService);

            IController controller = new Controller(computerPlayer, humanPlayer, gameService);

            GameUI form = new GameUI(gameService, controller);

            return form;
        }
    }
}
