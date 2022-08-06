using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptActionRPG
{
    internal class Monster : BaseMoveObjects
    {
        // Field
        Random random;

        public void Start(Random random)
        {
            #region MonsterInfoInit
            // 몬스터 정보 초기화
            hp = GameManager.Instance.Stage;
            maxHp = hp;
            range = 2;
            power = GameManager.Instance.Stage;
            exp = GameManager.Instance.Stage;
            shape = $"''";
            backGroundColor = ConsoleColor.Green;
            foreGroundColor = ConsoleColor.Black;
            #endregion

            this.random = random;

            #region Monster_Random_Pos_Initialize
            // 몬스터 초기위치
            //posX 가 홀수이면 -1로 짝수를 맞춰준다.
            posX = random.Next(2, GameManager.Instance.settings.Width - 2);
            if (posX % 2 != 0) posX -= 1;
            posY = random.Next(10, GameManager.Instance.settings.Height-2);
            #endregion
         
            isActive = true;    // 객체 활성화 초기값

            isBattle = false;   // 배틀 활성화 초기값.
            
            dir = 0;            // 방향 초기값.

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
            for (int i = 1; i < range+1; i++)
            {
                if ((posX < GameManager.Instance.player.positionX && GameManager.Instance.player.positionX <= posX + (i * 2) && posY == GameManager.Instance.player.positionY) ||
                    (posX > GameManager.Instance.player.positionX && GameManager.Instance.player.positionX >= posX - (i * 2) && posY == GameManager.Instance.player.positionY) ||
                    (posX == GameManager.Instance.player.positionX && posY > GameManager.Instance.player.positionY && GameManager.Instance.player.positionY >= posY - i) ||
                    (posX == GameManager.Instance.player.positionX && posY < GameManager.Instance.player.positionY && GameManager.Instance.player.positionY <= posY + i))
                {
                    isBattle = true;    // 배틀 상태 확인

                    Battle();           // 배틀 함수 호출

                    IsDead();           // 몬스터가 죽었는지 체크


                    if (sumTick < 1000) return;

                    // 공격후 다음 문으로 넘긴다.
                    AttackPlayer();
                    break;
                }
            }
            #endregion

            // 움직임은 1초로 계산해야 하기 떄문에 1000이하일 경우 리턴
            if (sumTick < 1000) return;

            // 배틀체크가 거짓일 경우에만 랜덤한 움직임
            if(!isBattle) RandomMove();

            // 초단위로 확인하여 플레이어가 없으면 isBattle은 false;
            isBattle = false;
            sumTick = 0;
        }
        public override void Render()
        {
            // 활성화 시에만 출력
            if (isActive)
            {
                Console.SetCursorPosition(posX, posY);
                ChangeColor();
            }
        }

        protected override void Battle()
        {
            base.Battle();
        }

        protected override void RandomMove()
        {
            dir = random.Next(0, 4);
            base.RandomMove();
        }
        protected override void ChangeColor()
        {
            base.ChangeColor();
        }
        protected override void AttackPlayer()
        {
            base.AttackPlayer();
        }
        protected override void IsDead()
        {
            base.IsDead();
        }
    }
}
