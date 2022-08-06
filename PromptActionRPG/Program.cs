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
            GameManager.Instance.StartScene();
            GameManager.Instance.Awake();
            GameManager.Instance.Start();

            while (!GameManager.Instance.isGameOver || !GameManager.Instance.win)
            {
                GameManager.Instance.Update();
                GameManager.Instance.Render();
            }

            if(GameManager.Instance.win) GameManager.Instance.WinScene();
            else GameManager.Instance.GameOver();
        }
    }
}
