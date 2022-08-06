using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptActionRPG
{

    internal class Monster : BaseObjects
    {
        #region Field
        public int pos_x { get; private set; }
        public int pos_y { get; private set; }

        // 몬스터 스테이지 정보를 담을 딕셔너리 선언
        public Dictionary<int, MonsterInfo> stageInfo;

        // 객체 선언.
        Player player;
        PlayerInfo playerInfo;
        Random random;

        // 보스 몬스터가 랜덤하게 움직일 방향
        int dir;

        // 스테이지 값을 담을 변수 선언
        int stage;

        // 활성화상태를 체크할 변수 선언
        public bool isActive { get; private set; }
        // 전투 상황인지 체크할 변수 선언
        bool isBattle;

        int sumTick;
        #endregion

        public void Start(Random random, Player player, PlayerInfo playerInfo, int stage)
        {
            
            // 몬스터 스테이지 딕셔너리 객체 생성및 초기화
            stageInfo = new Dictionary<int, MonsterInfo>
            {
                { 1, new MonsterInfo(1, 1, 2, 1) },          // 앞에는 키값이 될 int형 value
                { 2, new MonsterInfo(2, 2, 2, 2) },          // 뒤에는 value 값이 될 가격과 나오는 시간을 담은 객체
                { 3, new MonsterInfo(3, 3, 2, 3) },
                { 4, new MonsterInfo(4, 4, 2, 4) },
                { 5, new MonsterInfo(5, 5, 2, 5) },
                { 6, new MonsterInfo(6, 6, 2, 6) },
                { 7, new MonsterInfo(7, 7, 2, 7) },
                { 8, new MonsterInfo(8, 8, 2, 8) },
                { 9, new MonsterInfo(9, 9, 2, 9) },
                { 10, new MonsterInfo(10, 10, 2, 10) }
            };

            this.player = player;
            this.playerInfo = playerInfo;
            this.stage = stage;
            this.random = random;

            #region Monster_Random_Pos_Initialize
            // 몬스터 초기위치
            //pos_x 가 홀수이면 멈춘다.
            pos_x = random.Next(2, GameLoop.mapSize_x - 2);
            while (pos_x % 2 != 0) pos_x = random.Next(2, GameLoop.mapSize_x - 2);

            pos_y = random.Next(10, GameLoop.mapSize_y-2);
            #endregion

            // 객체 활성화 초기값
            isActive = true;
            // 배틀 활성화 초기값.
            isBattle = false;
            // 방향 초기값.
            dir = 0;
            sumTick = 0;
        }

        public override void Update(int deltaTick)
        {
            #region Fps_manager
            sumTick += deltaTick;
            #endregion


            // 활성화가 아니면 리턴.
            if (!isActive) return;

            #region Player_Battle
            // 적이 플레이어와 조우했을때 배틀
            // for문 stageInfo[stage].attackDistance 은 적 판정 범위
            for (int i = 1; i < stageInfo[stage].attackDistance+1 ; i++)
            {
                if ((pos_x < player.pos_x && player.pos_x <= pos_x + (i * 2) && pos_y == player.pos_y) ||
                    (pos_x > player.pos_x && player.pos_x >= pos_x - (i * 2) && pos_y == player.pos_y) ||
                    (pos_x == player.pos_x && pos_y > player.pos_y && player.pos_y >= pos_y - i) ||
                    (pos_x == player.pos_x && pos_y < player.pos_y && player.pos_y <= pos_y + i))
                {
                    // 배틀 함수 호출
                    Battle();
                    // 배틀 체크를 트루로 한다.
                    isBattle = true;
                    // 스테이지 딕셔너리에 담긴 hp가 0보다 작거나 같다면 객체 비활성화.
                    if (stageInfo[1].hp <= 0)
                    {
                        isActive = false;
                        playerInfo.exp += stageInfo[stage].exp;
                    }
               
                    // 배틀 체크를 트루로 한다.
                    if (sumTick < 1000) return;
                    // 범위안에 들어오면 한번만 타격판정 하기위한 if문
                    if (i == 2)
                        playerInfo.hp -= stageInfo[stage].damage;
                }
            }
            #endregion

            // 움직임은 1초로 계산해야 하기 떄문에 1000이하일 경우 리턴
            if (sumTick < 1000) return;
            // 배틀체크가 거짓일 경우에만 랜덤한 움직임
            if(!isBattle)
            {
                #region Enemy_Random_Move
                dir = random.Next(0, 4);
                Console.Clear();
                switch (dir)
                {
                    case 0:
                        if (pos_y +1<= 12) dir = 1;
                        else pos_y--;
                        break;
                    case 1:
                        if (pos_y+1 >= GameLoop.mapSize_y - 2) dir = 0;
                        else pos_y++;
                        break;
                    case 2:
                        if (pos_x-2 <= 2) dir = 3;
                        else pos_x -= 2;
                        break;
                    case 3:
                        if (pos_x+2 >= GameLoop.mapSize_x - 2) dir = 2;
                        else pos_x += 2;
                        break;
                }
                #endregion
  
                
            }
            // 초단위로 확인하여 플레이어가 없으면 isBattle은 false;
            isBattle = false;
            sumTick = 0;
        }
        public override void Render()
        {
            // 활성화 시에만 출력
            if (isActive)
            {
                Console.SetCursorPosition(pos_x, pos_y);
                #region change_Color_by_HP
                // 체력상태에 따라 객체 색 변경
                // 체력색표기 -> 초록색 > 80% > 노란색 >40% > 빨간색
                if (stageInfo[stage].hp == stage) Console.BackgroundColor = ConsoleColor.White;
                else if ((stageInfo[stage].hp <= (80 * stage) / 100) && stageInfo[stage].hp > ((40 * stage) / 100))
                    Console.BackgroundColor = ConsoleColor.Yellow;
                else Console.BackgroundColor = ConsoleColor.Red;
                #endregion
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("''");
                Console.ResetColor();
            }
        }

        public void Battle()
        {
            // 플레이어의 공격이 참이고 플레이어의 무기위치와 같다면 몬스터 hp-1
            if (player.isAttack && (pos_x >= player.waeponPosLeft || pos_x <= player.waeponPosRight || pos_y > player.waeponPosUp || pos_y <= player.waeponPosDown))
            {
                stageInfo[1].hp -= playerInfo.damage;
            }


        }
    }
}
