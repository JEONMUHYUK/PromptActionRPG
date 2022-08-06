using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptActionRPG
{
    internal class Player : BaseMoveObjects
    {
        #region Field
        // 플레이어 위치값
        // 무기 위치값
        public int waeponPosUp { get; private set; }
        public int waeponPosDown { get; private set; }
        public int waeponPosLeft { get; private set; }
        public int waeponPosRight { get; private set; }

        

        // 플레이어 정보 클래스 선언
        ConsoleKeyInfo cki;

        // 공격인지 확인하기위한 변수
        public bool isAttack { get; set; }
        #endregion

        public void Start()
        {
            // 플레이어 위치 초기값 
            posX = GameManager.Instance.settings.Width / 2;
            posY = GameManager.Instance.settings.Height / 2 + 10;

            isAttack = false;
            sumTick = 0;

            level = 1;
            hp = 10;
            maxHp = 10;
            exp = 0;
            maxExp = 10;
            power = 1;
            range = 1;
            foreGroundColor = ConsoleColor.White;
            backGroundColor = ConsoleColor.Black;
            shape = "◈";
            
        }

        public override void Update(int deltaTick)
        {
            sumTick += deltaTick;
            if (sumTick < 100) return;

            if (Console.KeyAvailable) KeyManager();

            if (exp >= maxExp) LevelManager();


            //  무기 위치 업데이트
            WeaponPosUpdate();

            sumTick = 0;
        }


        public override void Render()
        {
            // 플레이어 상태 랜더링
            Console.SetCursorPosition(0, 1);
            InfoRender();

            // 플레이어 랜더링
            Console.SetCursorPosition(posX, posY);
            ChangeColor();
            // isAttack 활성화시 공격모션 출력
            if (isAttack) AttackRender();
            isAttack = false;
        }


        public void InfoRender()
        {
            // 플레이어 상태 창 랜더링 함수
            Console.Write($"LV       : {level}");
            Console.WriteLine();
            Console.Write($"HP       : {Math.Truncate(hp)} / {Math.Truncate(maxHp)}");
            Console.WriteLine();
            Console.Write($"EXP      : {exp} / {Math.Truncate(maxExp)}");
            Console.WriteLine();
            Console.Write($"Power    : {Math.Truncate(power)}");
            Console.WriteLine();
            Console.Write($"Range    : {Math.Truncate(range)}");
            Console.WriteLine();
        }
        public void AttackRender()
        {
            for (int i = 1; i < Math.Truncate(range) + 1; i++)
            {
                if (waeponPosUp - i > 8)
                { 
                    Console.SetCursorPosition(posX, posY - i);
                    Console.Write("＊");
                }
                if (waeponPosDown + i <= GameManager.Instance.settings.Height)
                { 
                    Console.SetCursorPosition(posX, posY + i);
                    Console.Write("＊");
                }
                if (waeponPosLeft - i * 2 >= -2)
                { 
                    Console.SetCursorPosition(posX - (i*2), posY);
                    Console.Write("＊");
                }
                if (waeponPosRight + i * 2 <= GameManager.Instance.settings.Width)
                { 
                    Console.SetCursorPosition(posX + (i * 2), posY);
                    Console.Write("＊");
                }
            }
        }

        public void WeaponPosUpdate()
        {
            for (int i = 1; i < Math.Truncate(range) + 1; i++)
            {
                waeponPosUp = posY - i;
                waeponPosDown = posY + i;
                waeponPosLeft = posX - (i * 2);
                waeponPosRight = posX + (i * 2);
            }
        }

        public void LevelManager()
        {
            exp = 0;
            maxExp *= 1.5f;
            maxHp *= 1.5f;
            hp = maxHp;
            power *= 1.5f;
            level++;

            if (Math.Truncate(range) < 2) range *= 1.5f;
        }

        public void KeyManager()
        {
            cki = Console.ReadKey();
            Console.Clear();
            switch (cki.Key)
            {
                case ConsoleKey.UpArrow:
                    if (posY - 1 <= 10) posY = 10;
                    else posY--;
                    break;
                case ConsoleKey.DownArrow:
                    if (posY + 1 >= GameManager.Instance.settings.Height - 1) posY = GameManager.Instance.settings.Height - 1;
                    else posY++;
                    break;
                case ConsoleKey.LeftArrow:
                    if (posX - 2 <= 0) posX = 0;
                    else posX -= 2;
                    break;
                case ConsoleKey.RightArrow:
                    if (posX + 2 >= GameManager.Instance.settings.Width - 2) posX = GameManager.Instance.settings.Width - 2;
                    else posX += 2;
                    break;
                case ConsoleKey.Spacebar:
                    isAttack = true;
                    break;
            }

        }

        protected override void ChangeColor()
        {
            base.ChangeColor();
        }
    }
}
