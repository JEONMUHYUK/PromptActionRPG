using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptActionRPG
{
    internal class Menu
    {
        int SelectedIndex;
        string[] _options;
        string _prompt;

        public Menu(string _prompt, string[] _options)                  // 생성자 초기화
        {
            this._prompt = _prompt;                                     // 스트링 값을 하나 받아온다.
            this._options = _options;                                   // 옵션 스트링 배열은 메뉴 이름과 길이를 정한다.
            SelectedIndex = 0;                                          // selectedIndex 초기화  

        }

        #region Options
        private void DisplayOptions()                                   // 메뉴 옵션설정
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(_prompt);                                 // _prompt에서 받아온 스트링을 출력한다.
            Console.ResetColor();
            for (int i = 0; i < _options.Length; i++)                   // 옵션 문자열 길이 만큼
            {
                string currentOption = _options[i];                     // currentOption은 문자열을 받는다.
                string prefix;

                if (i == SelectedIndex)                                 //selectedIndex라면 하얀배경에 검은글자로 출력
                {
                    prefix = "*";
                    Console.Beep(1000, 100);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Cyan;

                }
                else
                {
                    prefix = " ";
                    Console.ForegroundColor = ConsoleColor.White;       // selectedInde가 아니라면 하얀글자에 검은 배경으로 출력
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.WriteLine($"                                                    {prefix} >> {currentOption} ");
            }
            Console.ResetColor();
        }
        #endregion

        public int Run()
        {
            Console.CursorVisible = false;
            ConsoleKey keyPressed;                                              // 키를 눌럿다는 콘솔키 변수선언.
            do
            {
                Console.Clear();                                                // 콘솔창을 클리어 한다.

                DisplayOptions();                                               // 스트링과 option 들을 출력한다.

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);                 //  콘솔키 입력값을 받는다
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)                           // W를 입력한다면
                {
                    SelectedIndex--;                                            // selectedIndex --;
                    if (SelectedIndex == -1)                                    //selectedIndex가 음수로 간다면 
                    {
                        SelectedIndex = _options.Length - 1;                    //selectedIndex는 option마지막으로 간다.
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)                    // S를 입력한다면
                {
                    SelectedIndex++;
                    if (SelectedIndex == _options.Length)                       //SelectedIndex가 배열의 마지막이라면
                    {

                        SelectedIndex = 0;                                      // 다시 배열 첫번째로 돌린다.
                    }
                }

            } while (keyPressed != ConsoleKey.Enter);                           // 엔터를 누르지 않는 한 반복.

            return SelectedIndex;
        }
    }
}
