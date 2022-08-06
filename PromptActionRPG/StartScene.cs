using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptActionRPG
{
    internal class StartScene
    {
        public void Start()
        {

            Console.Title = "Prompt_Action_RPG";
            RunMainMenu();


        }

        private void RunMainMenu()
        {
            string prompt = @"
                                  /\                                                 /\
                       _          )(______________________   ______________________  )(        _
                      (_)///////(**)______________________> <______________________(**)\\\\\\\(_)
                                  )(                                                 )(
                                  \/                                                 \/
                                           _             _     _                 
                                          / \      ___  | |_  (_)   ___    _ __  
                                         / _ \    / __| | __| | |  / _ \  | '_ \ 
                                        / ___ \  | (__  | |_  | | | (_) | | | | |
                                       /_/__ \_\  \___|  \__| |_|_ \___/  |_| |_|
                                                |  _ \     |  _ \     / ___|              
                                                | |_) |    | |_) |   | |  _               
                                                |  _ <     |  __/    | |_| |              
                                                |_| \_\    |_|        \____|              
                                           
                                                                      

";            // 프롬프트 값
            string[] options = { "Play", "Exit" };
            Menu mainMenu = new Menu(prompt, options);
            int selectedIndex = mainMenu.Run();
            switch (selectedIndex)
            {
                case 0:
                    GameStart();
                    break;
                case 1:
                    ExitGame();
                    break;
            }
        }

        private void ExitGame()
        {
            Console.WriteLine("\nPressed any Key to exit....");
            Console.ReadKey(true);
            Environment.Exit(0);
        }

        private void GameStart()
        {
            Console.Clear();
            return;
        }
    }
}
