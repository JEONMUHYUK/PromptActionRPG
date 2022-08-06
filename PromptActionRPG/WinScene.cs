using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptActionRPG
{
    internal class WinScene
    {
        public void Render()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"
 

                                  
       _____    _____    _____    _____ 
      |     |  |     |  |   | |  |   __|
      |   --|  |  |  |  | | | |  |  |  |
      |_____|  |_____|  |_|___|  |_____|                            
       _____    _____    _____    _____ 
      | __  |  |  _  |  |_   _|  |  |  |
      |    -|  |     |    | |    |  |  |
      |__|__|  |__|__|    |_|    |_____|                                  
       __       _____    _____    _____ 
      |  |     |  _  |  |_   _|  |     |
      |  |__   |     |    | |    |-   -|
      |_____|  |__|__|    |_|    |_____|                                  
                               __           
             _____    _____   |  |          
            |     |  |   | |  |  |          
            |  |  |  | | | |  |__|          
            |_____|  |_|___|  |__|          
                                  

        ");
        }
    }
}
