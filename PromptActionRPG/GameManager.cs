using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptActionRPG
{
    internal class GameManager : SingleTon<GameManager>
    {
        #region Field       
        // Member Valiable
        public GameSettings settings { get; private set; }
        public Player player { get; private set; }

        public Random random { get; private set; }
        Items[] items;

        string[] itemsName;

        int stage;
        public int Stage { get { return stage; } set { stage = value; } }

        int lastTick;

        public bool isGameOver { get; private set; }
        public bool win { get; private set; }
        #endregion
        public void Awake()
        {
            settings    = new GameSettings();                       // 세팅 객체 생성
            random      = new Random(DateTime.Now.Millisecond);     // 랜덤객체 생성
            player      = new Player();                             // 플레이어 객체 생성
            MonsterDataManager.Instance.Awake();

            items       = new Items[3];                             // 아이템 객체 배열

            isGameOver  = false;
            win         = false;
        }

        public void Start()
        {
            // 세팅 초기화
            settings.Init();

            // 스테이지 초기 값
            stage = 1;

            #region Objects Initialize
            // item 이름 배열 초기화
            itemsName = new string[3] { "HP", "Power", "Shield" };

            MonsterDataManager.Instance.Start();

            // 아이템 객체 생성 및 초기화
            for (int i = 0; i < items.Length; i++)
            { 
                items[i] = new Items();
                items[i].Start(random, itemsName[i]);
            }

            // 플레이어 초기화
            player.Start();
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
            // 객체 업데이트
            player.Update(deltaTick);
            MonsterDataManager.Instance.Update(deltaTick);
            for (int i = 0; i < items.Length; i++) items[i].Update(deltaTick);
            #endregion


            // 잔여 보스와 몬스터가 0이면 다음 스테이지
            if (MonsterDataManager.Instance.RemainingBoss == 0 && MonsterDataManager.Instance.RemainingMonsters == 0) NextStage();

            // 승패 조건
            if (player.HP <= 0) isGameOver = true;
            else if (stage == 11) win = true;

            lastTick = currentTick;
        }

        // 출력을 위한 대리자
        delegate void RenderDelegate();
        public void Render()
        {
            // 스테이지 정보 함수를 대리자로 생성.
            RenderDelegate stageInfo = new RenderDelegate(StageInfo);
            
            // 상태창 위치
            Console.SetCursorPosition(0, 0);
            Console.Write("----------------------------------------");
            Console.SetCursorPosition(0, 9);
            Console.Write("----------------------------------------");

            // 스테이지 정보에 플레이어, 보스, 몬스터, 아이템 출력 체인.
            stageInfo += player.Render;
            stageInfo += MonsterDataManager.Instance.Render;    
            for (int i = 0; i < items.Length; i++) stageInfo += items[i].Render;

            // 체인후 출력
            stageInfo();
        }

        public void WinScene()
        { 
            // 승리시 승리씬 출력
            WinScene winScene = new WinScene();
            Console.Clear();
            winScene.Render();
        }
        public void GameOver()
        {
            // 게임오버시 게임오버씬 출력
            GameOver gameOver = new GameOver();
            Console.Clear();
            gameOver.Render();
        }
        public void StartScene()
        {
            // 시작시 시작씬 출력
            StartScene startScene = new StartScene();
            startScene.Start();
        }
        void NextStage()
        {
            // 다음 스테이지
            // 스테이지를 1올리고 몬스터 객체들 초기화
            stage++;
            MonsterDataManager.Instance.Start();
        }

        void StageInfo()
        {
            // 스테이지 정보 출력
            Console.SetCursorPosition(22, 1);
            Console.Write($"stage    : {stage}");
            Console.SetCursorPosition(22, 2);
            Console.WriteLine($"Monsters : {MonsterDataManager.Instance.RemainingMonsters} / 20");
            Console.SetCursorPosition(22, 3);
            Console.WriteLine($"Boss     : {MonsterDataManager.Instance.RemainingBoss} / 1");
        }
    }
}
