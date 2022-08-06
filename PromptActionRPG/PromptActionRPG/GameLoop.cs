using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptActionRPG
{
    internal class GameLoop
    {
        #region Field
        public const int mapSize_x = 40; 
        public const int mapSize_y = 50;
        public const int numbersOfEnemies = 20;

        Monster[] enemies;
        BossMonster boss;
        Player player;
        Random random;
        Items[] items;

        PlayerInfo playerInfo;

        int stage;

        int remainingMonsters;
        int remainingBoss;
        int lastTick;

        public bool isGameOver { get; private set; }
        #endregion
        public void Awake()
        {

            random = new Random(DateTime.Now.Millisecond);
            player = new Player();
            enemies = new Monster[numbersOfEnemies];
            boss = new BossMonster();
            items = new Items[3];

            // 아이템 객체 생성
            for (int i = 0; i < items.Length; i++)
                items[i] = new Items();

            // 플레이어 스탯 초기값
            playerInfo = new PlayerInfo(1, 10, 10, 10, 10, 1, 1);

            // 적 객체 생산.
            for (int i = 0; i < numbersOfEnemies; i++) enemies[i] = new Monster();

            isGameOver = false;
        }

        public void Start()
        {
            Console.CursorVisible = false;
            Console.BufferWidth = Console.WindowWidth = mapSize_x;
            Console.BufferHeight = Console.WindowHeight = mapSize_y;

            // 스테이지 초기 값
            stage = 1;

            #region Objects Initialize
            // 몬스터 초기화
            for (int i = 0; i < numbersOfEnemies; i++)
                enemies[i].Start(random, player, playerInfo, stage);
            boss.Start(random, player, playerInfo, stage);

            // 아이템 초기화
            items[0].Start(random, player, playerInfo, "HP");
            items[1].Start(random, player, playerInfo, "Power");
            items[2].Start(random, player, playerInfo, "Shield");

            // 플레이어 초기화
            player.Start(playerInfo);

            #endregion

            lastTick = 0;
        }

        public void Update()
        {
            #region Fps_Management
            int currentTick = Environment.TickCount;
            int deltaTick = currentTick - lastTick;
            // 100 밑이면 리턴
            if (currentTick - lastTick < 100)
                return;
            #endregion

            #region Objects Update

            player.Update(deltaTick);
            boss.Update(deltaTick);
            for (int i = 0; i < numbersOfEnemies; i++)   
                enemies[i].Update(deltaTick);

            for (int i = 0; i < items.Length; i++)
                items[i].Update(deltaTick);
            #endregion

            #region Monsters Count
            // 남은 몬스터 계산.
            // 반복문이 끝나면 초기화되는 변수을 이용해 매번 값을 새로 갱신한다.
            MonsterCount();
            #endregion


            #region NextStage_Active
            if (remainingBoss == 0 && remainingMonsters == 0) NextStage();
            #endregion

            if (playerInfo.hp <= 0) isGameOver = true;

            lastTick = currentTick;
        }

        public void Render()
        {

            // 상태창 위치
            Console.SetCursorPosition(0, 0);
            Console.Write("----------------------------------------");
            Console.SetCursorPosition(0, 9);
            Console.Write("----------------------------------------");

            // 스테이지 정보 출력
            Console.SetCursorPosition(22,1);
            Console.Write($"stage    : {stage}");
            Console.SetCursorPosition(22, 2);
            Console.WriteLine($"Monsters : {remainingMonsters} / 20");
            Console.SetCursorPosition(22, 3);
            Console.WriteLine($"Boss     : {remainingBoss} / 1");

            // 플레이어 출력
            player.Render();
            
            // 보스몬스터  출력
            boss.Render();

            // 몬스터 출력
            for (int i = 0; i < numbersOfEnemies; i++)
                enemies[i].Render();
            // 아이템 출력
            for (int i = 0; i < items.Length; i++)
                items[i].Render();
        }

        public void GameOver()
        {
            GameOver gameOver = new GameOver();
            Console.Clear();
            gameOver.Render();
        }
        public void StartScene()
        {
            StartScene startScene = new StartScene();
            startScene.Start();
        }
        public void NextStage()
        {
            stage++;
            boss.Start(random, player, playerInfo, stage);
            for (int i = 0; i < numbersOfEnemies; i++)
                enemies[i].Start(random, player, playerInfo, stage);
        }

        public void MonsterCount()
        {
            int count = 0;

            for (int i = 0; i < 20; i++) if (enemies[i].isActive) count++;
            remainingMonsters = count;
            int bossCount = 0;
            if (boss.isActive) bossCount++;
            remainingBoss = bossCount;
        }
    }
}
