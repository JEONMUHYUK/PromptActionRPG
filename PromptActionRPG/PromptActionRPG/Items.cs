using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptActionRPG
{
    internal class Items : BaseObjects
    {
        #region Field
        public int pos_x { get; private set; }
        public int pos_y { get; private set; }

        // 아이템 정보를 담는 딕셔너리 객체 선언 및 기타 객체 선언.
        Dictionary<string, ItemInfo> itemInfo;
        Player player;
        PlayerInfo playerInfo;
        Random random;

        // 변한 데미지 값과 기존 데미지값을 담기위한 변수.
        float timesDamage;
        float beforeDamage;

        // 아이템 활성화 시간 체크를 위한 변수 선언
        int time;
        // 이전 HP와 고정될 HP를 담을 변수.
        float beforeHP;
        float staticHP;

        // 아이템 이름을 위한 문자열 변수 선언.
        string itemName;

        int sumTick;

        // 아이템 활성화 체크를 위한 변수
        public bool isActive { get; private set; }
        #endregion

        public void Start(Random random, Player player, PlayerInfo playerInfo, string itemName)
        {
            // 아이템 정보를 객체화 하여 딕셔너리에 담는다
            itemInfo = new Dictionary<string, ItemInfo>
            {
                { "HP", new ItemInfo("HP",0,"†") },          // 앞에는 키값이 될 int형 value
                { "Power", new ItemInfo("Power is 3 times",10,"◈") },          // 뒤에는 value 값이 될 가격과 나오는 시간을 담은 객체
                { "Shield", new ItemInfo("Damage is Zero",5, "▣") },
            };

            this.player = player;
            this.playerInfo = playerInfo;
            this.random = random;

            // 딕셔너리의 키값이 될 문자열 변수
            this.itemName = itemName;

            #region Item_Random_Pos_Initialize
            //pos_x 가 홀수이면 멈춘다.
            pos_x = random.Next(2, GameLoop.mapSize_x - 2);
            while (pos_x % 2 != 0) pos_x = random.Next(2, GameLoop.mapSize_x - 2);

            pos_y = random.Next(10, GameLoop.mapSize_y - 2);
            #endregion

            // 활성화 상태 초기값.
            isActive = false;

            // 변한 데미지 값과 기존 데미지값을 담기위한 변수.
            beforeDamage = playerInfo.damage;
            timesDamage = beforeDamage * 3;
            // 아이템 활성화 시간 체크를 위한 변수 선언
            time = itemInfo[itemName].time;
            // 이전 HP와 고정될 HP를 담을 변수.
            beforeHP = playerInfo.hp;
            staticHP = beforeHP;

            sumTick = 0;
        }

        public override void Update(int deltaTick)
        {
            // 시간을 담는 값
            sumTick += deltaTick;


            // 아이템이 비활성화 시에만 활성화 가능.
            if (pos_y == player.pos_y && pos_x == player.pos_x && isActive == false)
            {
                Start(random, player, playerInfo, itemName);
                isActive = true;
            }

            // 아이템이 활성화 되면 초당 계산
            if (isActive && sumTick > 1000)
            {
                // 회복템인 경우
                if (itemInfo[itemName].shape == "†") playerInfo.hp = playerInfo.maxHP;
                else if (time > 0)
                {
                    // 공격력 증가템인 경우
                    if (itemInfo[itemName].shape == "◈")
                       playerInfo.damage = timesDamage;

     
                    // 방어템인 경우
                    else if (itemInfo[itemName].shape == "▣")
                        playerInfo.hp = staticHP;

                    time--;
                }
                // 시간이 끝난 경우
                if (time == 0)
                {
                    // 시간 초기화.
                     time = itemInfo[itemName].time;
                    // 공격력 증가 초기화
                    if (itemInfo[itemName].shape == "◈")
                        playerInfo.damage = beforeDamage;
                    // 체력 원래 값으로 초기화
                    else if (itemInfo[itemName].shape == "▣")
                        playerInfo.hp = beforeHP;

                    // 비활성화
                    isActive = false;
                }
                
                sumTick = 0;
            }

            
        }
        public override void Render()
        {
            // 활성화 시에만 출력
            // ItemName에 따라 다르게 출력
            if (itemName == "HP")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.White;
                Console.SetCursorPosition(pos_x, pos_y);
                // 출력 모양은 아이템정보클래서에 정의된 값을 가져온다.
                Console.Write(itemInfo[itemName].shape);
            }
            else if (itemName == "Power")
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.SetCursorPosition(pos_x, pos_y);
                Console.Write(itemInfo[itemName].shape);
                Console.ResetColor();
                if (itemInfo[itemName].time > 0 && isActive)
                {
                    // 만약 아이템정보에 정의된 시간이 0보다 크고 활서화 되어 있다면 
                    // 상태창에 아이템정보클래스에 정의된 아이템정보를 가져온다. 초기화된 시간 같이 출력
                    Console.SetCursorPosition(0, 6);
                    Console.Write($"{itemInfo[itemName].info}   Time : {time} ");
                }
            }
            else
            {
                // 아이템 이름이 Shield일때
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(pos_x, pos_y);
                Console.Write(itemInfo[itemName].shape);
                Console.ResetColor();
                if (itemInfo[itemName].time > 0 && isActive)
                {
                    Console.SetCursorPosition(0, 7);
                    Console.Write($"{itemInfo[itemName].info}   Time : {time} ");
                }
            }
            
            Console.ResetColor();

        }

    }
}
