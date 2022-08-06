using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptActionRPG
{
    internal class GameSettings
    {
        int width = 40;
        int height = 50;

        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public void Init()
        {
            Console.CursorVisible = false;
            Console.BufferWidth = Console.WindowWidth = width;
            Console.BufferHeight = Console.WindowHeight = height;
        }
    }
}
