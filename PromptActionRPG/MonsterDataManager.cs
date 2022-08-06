using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptActionRPG
{
    internal class MonsterDataManager : SingleTon<MonsterDataManager>
    {
        const int numbersOfEnemies = 20;      // 생성할 몬스터 수
        Monster[] Monster;
        BossMonster boss;


        int remainingMonsters;
        int remainingBoss;
        public int RemainingMonsters { get { return remainingMonsters; } }
        public int RemainingBoss { get { return remainingBoss; } }

        public void Awake()
        {
            boss = new BossMonster();                        // 보스몬스터 객체 생성
            Monster = new Monster[numbersOfEnemies];            // 몬스터 객체 배열 생성
        }
        public void Start()
        {
            // 몬스터 객체 생성 및 초기화.
            for (int i = 0; i < numbersOfEnemies; i++)
            {
                Monster[i] = new Monster();
                Monster[i].Start(GameManager.Instance.random);
            }
            // 보스 몬스터 초기화
            boss.Start(GameManager.Instance.random);
        }
        public void Update(int deltaTick)
        {
            boss.Update(deltaTick);
            for (int i = 0; i < numbersOfEnemies; i++) Monster[i].Update(deltaTick);

            MonsterCount();
        }
        public void Render()
        {
            boss.Render();
            for (int i = 0; i < numbersOfEnemies; i++) Monster[i].Render();
        }

        public void MonsterCount()
        {
            int count = 0;      // 몬스터 카운트
            int bossCount = 0;  // 보스 카운트

            // 남은 몬스터 카운트            
            for (int i = 0; i < 20; i++) if (Monster[i].IsActive) count++;
            if (boss.IsActive) bossCount++;
            remainingMonsters = count;
            remainingBoss = bossCount;
        }

    }
}
