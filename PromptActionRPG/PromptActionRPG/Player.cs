using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptActionRPG
{
    delegate void PlayerDelegate();
    internal class Player : BaseObjects
    {
        #region Field
        // 플레이어 위치값
        public int pos_x { get; private set; }
        public int pos_y { get; private set; }

        // 무기 위치값
        public int waeponPosUp { get; private set; }
        public int waeponPosDown { get; private set; }
        public int waeponPosLeft { get; private set; }
        public int waeponPosRight { get; private set; }

        

        // 플레이어 정보 클래스 선언
        PlayerInfo info;
        ConsoleKeyInfo cki;

        // 공격인지 확인하기위한 변수
        public bool isAttack { get; set; }
        int sumTick;
        #endregion

        public void Start(PlayerInfo playerInfo)
        {
            // 플레이어 위치 초기값 
            pos_x = GameLoop.mapSize_x / 2;
            pos_y = GameLoop.mapSize_y / 2 + 10;

            info = playerInfo;
            isAttack = false;
            sumTick = 0;
            info.exp = 0;
            info.hp = info.maxHP;

            
        }

        public override void Update(int deltaTick)
        {
            sumTick += deltaTick;
            if (sumTick < 100) return;

            // Player Info LevelUp Function Delegate 
            PlayerDelegate levelManagerDele = new PlayerDelegate(info.LevelManager);
            


            #region player_Key_Manager
            if (Console.KeyAvailable)
                KeyManager();
            #endregion

            #region Level_Up_Update
            // playerInfoUpdate

            // Player Info LevelUp Function Delegate 
            if (info.exp >= info.maxExp)
                levelManagerDele();
            #endregion

                //  무기 위치 업데이트
            WeaponPosUpdate();


            sumTick = 0;
        }


        public override void Render()
        {
            #region Player_Info_Rendering

            // 플레이어 상태 랜더링
            // 플레이어 상태 클래스에서 델리게이트로 함수를 데리고 온다.
            PlayerDelegate infoRender = new PlayerDelegate(info.InfoRender);
            Console.SetCursorPosition(0, 1);
            infoRender();

            #endregion

            // 플레이어 랜더링
            Console.SetCursorPosition(pos_x, pos_y);
            #region change_Color_by_HP
            // 체력상태에 따라 객체 색 변경
            // 체력색표기 -> 초록색 > 80% > 노란색 >40% > 빨간색
            if (info.hp > (80 * info.maxHP)/100) Console.ForegroundColor = ConsoleColor.White;
            else if (info.hp <= (80 * info.maxHP) / 100 && info.hp > (40 * info.maxHP) / 100)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else Console.ForegroundColor = ConsoleColor.Red;
            #endregion
            Console.Write("◈");
            Console.ResetColor();
            // isAttack 활성화시 공격모션 출력
            if (isAttack) AttackRender();
            isAttack = false;
        }



        public void AttackRender()
        {
            // Math.Truncate(info.attackDistance) 소수점을 날린 공격 거리에 따른 위치에 출력
            for (int i = 1; i < Math.Truncate(info.attackDistance) + 1; i++)
            {
                if (waeponPosUp - i >= 10)
                { 
                    Console.SetCursorPosition(pos_x, pos_y - i);
                    Console.Write("＊");
                }
                if (waeponPosDown + i <= GameLoop.mapSize_y)
                { 
                    Console.SetCursorPosition(pos_x, pos_y + i);
                    Console.Write("＊");
                }
                if (waeponPosLeft - i * 2 >= 0)
                { 
                    Console.SetCursorPosition(pos_x - (i*2), pos_y);
                    Console.Write("＊");
                }
                if (waeponPosRight + i * 2 <= GameLoop.mapSize_x)
                { 
                    Console.SetCursorPosition(pos_x + (i * 2), pos_y);
                    Console.Write("＊");
                }
            }
        }

        public void WeaponPosUpdate()
        {
            // Math.Truncate(info.attackDistance) 위치에 따라 범위 변경
            for (int i = 1; i < Math.Truncate(info.attackDistance) + 1; i++)
            {
                waeponPosUp = pos_y - i;
                waeponPosDown = pos_y + i;
                waeponPosLeft = pos_x - (i * 2);
                waeponPosRight = pos_x + (i * 2);
            }
        }


        public void KeyManager()
        {
            cki = Console.ReadKey();
            Console.Clear();
            switch (cki.Key)
            {
                case ConsoleKey.UpArrow:
                    if (pos_y - 1 <= 10) pos_y = 10;
                    else pos_y--;
                    break;
                case ConsoleKey.DownArrow:
                    if (pos_y + 1 >= GameLoop.mapSize_y - 1) pos_y = GameLoop.mapSize_y - 1;
                    else pos_y++;
                    break;
                case ConsoleKey.LeftArrow:
                    if (pos_x - 2 <= 0) pos_x = 0;
                    else pos_x -= 2;
                    break;
                case ConsoleKey.RightArrow:
                    if (pos_x + 2 >= GameLoop.mapSize_x - 2) pos_x = GameLoop.mapSize_x - 2;
                    else pos_x += 2;
                    break;
                case ConsoleKey.Spacebar:
                    isAttack = true;
                    break;
            }

        }
    }
}
