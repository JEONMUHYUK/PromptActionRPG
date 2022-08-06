using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptActionRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameLoop gameLoop = new GameLoop();
            gameLoop.Awake();
            gameLoop.StartScene();
            gameLoop.Start();

            while (gameLoop.isGameOver == false)
            {
                gameLoop.Update();
                gameLoop.Render();
            }

            gameLoop.GameOver();
        }
    }
}
