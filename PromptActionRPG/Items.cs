using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptActionRPG
{
    // 아이템 정보 클래스를 구조체로 사용.
    internal class ItemInfo
    {
        public string info;
        public int time;
        public string shape;
        public ConsoleColor color;

        public ItemInfo(string info, int time, string shape, ConsoleColor color)
        {
            this.info = info;
            this.time = time;
            this.shape = shape;
            this.color = color;
        }
    }

    internal class Items
    {
        #region Field
        public int posX { get; private set; }
        public int posY { get; private set; }

        // 아이템 정보를 담는 딕셔너리 객체 선언 및 기타 객체 선언.
        Dictionary<string, ItemInfo> itemInfo;
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

        public void Start(Random random, string itemName)
        {
            // 아이템 정보를 객체화 하여 딕셔너리에 담는다
            itemInfo = new Dictionary<string, ItemInfo>
            {
                { "HP", new ItemInfo("HP", 0, "†", ConsoleColor.Red) },          // 앞에는 키값이 될 int형 value
                { "Power", new ItemInfo("Power is 3 times", 10, "◈", ConsoleColor.Magenta) },          // 뒤에는 value 값이 될 가격과 나오는 시간을 담은 객체
                { "Shield", new ItemInfo("Damage is Zero", 5, "▣", ConsoleColor.Blue) },
            };

            this.random = random;

            // 딕셔너리의 키값이 될 문자열 변수
            this.itemName = itemName;

            #region Item_Random_Pos_Initialize
            //positionX 가 홀수이면 -1.
            posX = random.Next(2, GameManager.Instance.settings.Width - 2);
            if (posX % 2 != 0) posX -= 1;
            posY = random.Next(10, GameManager.Instance.settings.Height - 2);
            #endregion

            // 활성화 상태 초기값.
            isActive = false;

            // 변한 데미지 값과 기존 데미지값을 담기위한 변수.
            beforeDamage = GameManager.Instance.player.Power;
            timesDamage = beforeDamage * 3;

            // 아이템 활성화 시간 체크를 위한 변수 선언
            time = itemInfo[itemName].time;

            // 이전 HP와 고정될 HP를 담을 변수.
            beforeHP = GameManager.Instance.player.HP;
            staticHP = beforeHP;

            sumTick = 0;
        }

        public void Update(int deltaTick)
        {
            // 시간을 담는 값
            sumTick += deltaTick;

            ItemReset();

            // 아이템이 활성화 되면 초당 계산
            if (isActive && sumTick > 1000)
            {
                OnItem();
                OffItem();
                sumTick = 0;
            }
        }
        public void Render()
        {
            ItemsRender();
        }

        void ItemsRender()
        {
            // ItemName에 따라 다르게 출력
            if (itemName == "HP")
            {
                Console.BackgroundColor = ConsoleColor.White;
                ItemCustom();
            }
            else if (itemName == "Power")
            {
                ItemCustom();
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

                ItemCustom();
                if (itemInfo[itemName].time > 0 && isActive)
                {
                    Console.SetCursorPosition(0, 7);
                    Console.Write($"{itemInfo[itemName].info}   Time : {time} ");
                }
            }
            Console.ResetColor();
        }

        void ItemCustom()
        {
            Console.ForegroundColor = itemInfo[itemName].color;
            Console.SetCursorPosition(posX, posY);
            Console.Write(itemInfo[itemName].shape);
        }

        void ItemReset()
        {
            // 아이템이 비활성화 시에만 활성화 가능.
            if (posY == GameManager.Instance.player.positionY && posX == GameManager.Instance.player.positionX && isActive == false)
            {
                Start(random, itemName);
                isActive = true;
            }
        }

        void OnItem()
        {
            // 회복템인 경우
            if (itemInfo[itemName].shape == "†") GameManager.Instance.player.HP = GameManager.Instance.player.MaxHP;
            else if (time > 0)
            {
                // 공격력 증가템인 경우
                if (itemInfo[itemName].shape == "◈")
                    GameManager.Instance.player.Power = timesDamage;

                // 방어템인 경우
                else if (itemInfo[itemName].shape == "▣")
                    GameManager.Instance.player.HP = staticHP;
                time--;
            }
        }

        void OffItem()
        {
            // 시간이 끝난 경우
            if (time == 0)
            {
                // 시간 초기화.
                time = itemInfo[itemName].time;
                // 공격력 증가 초기화
                if (itemInfo[itemName].shape == "◈")
                    GameManager.Instance.player.Power = beforeDamage;
                // 체력 원래 값으로 초기화
                else if (itemInfo[itemName].shape == "▣")
                    GameManager.Instance.player.HP = beforeHP;

                // 비활성화
                isActive = false;
            }
        }
    }
}
