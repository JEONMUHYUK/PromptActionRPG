using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptActionRPG
{
    internal abstract class BaseMoveObjects
    {
        protected float level;
        protected float hp;
        public float HP { get { return hp; } set { hp = value; } }

        protected float maxHp;
        public float MaxHP { get { return maxHp; } set { maxHp = value; } }

        protected float range;

        protected float power;
        public float Power { get { return power; } set { power = value; } }
        protected float exp;
        public float Exp { get { return exp; } set { exp = value; } }

        protected float maxExp;

        protected int posX;
        public int positionX { get { return posX; } set { posX = value; } }
        protected int posY;
        public int positionY { get { return posY; } set { posX = value; } }

        protected string shape;

        protected int dir;

        protected bool isActive;
        public bool IsActive { get { return isActive; } set { isActive = value; } }

        protected bool isBattle;

        protected int sumTick;

        protected ConsoleColor backGroundColor;
        protected ConsoleColor foreGroundColor;

        public abstract void Update(int deltaTime);
        public abstract void Render();

        protected virtual void RandomMove()
        {  
            Console.Clear();
            switch (dir)
            {
                case 0:
                    if (posY + 1 <= 12) dir = 1;
                    else posY--;
                    break;
                case 1:
                    if (posY + 1 >= GameManager.Instance.settings.Height - 2) dir = 0;
                    else posY++;
                    break;
                case 2:
                    if (posX - 2 <= 2) dir = 3;
                    else posX -= 2;
                    break;
                case 3:
                    if (posX + 2 >= GameManager.Instance.settings.Width - 2) dir = 2;
                    else posX += 2;
                    break;
            }
        }

        protected virtual void Battle()
        {
            // 플레이어 범위 내에서 공격 받았다면 hp는 플레이어 power 만큼 감소
            for (int i = 1; i < GameManager.Instance.player.range+1; i++)
            {
                if (GameManager.Instance.player.isAttack)
                {
                    
                    if (posX + (i * 2) == GameManager.Instance.player.positionX && posY == GameManager.Instance.player.positionY)
                    {
                        hp -= GameManager.Instance.player.power;
                        return;
                    }
                    if (posX - (i * 2) == GameManager.Instance.player.positionX && posY == GameManager.Instance.player.positionY)
                    {
                        hp -= GameManager.Instance.player.power;
                        return;
                    }
                    if (posY - (i) == GameManager.Instance.player.positionY && posX == GameManager.Instance.player.positionX)
                    {
                        hp -= GameManager.Instance.player.power;
                        return;
                    }
                    if (posY + (i) == GameManager.Instance.player.positionY && posX == GameManager.Instance.player.positionX)
                    {
                        hp -= GameManager.Instance.player.power;
                        return;
                    }
                }
            }
        }

        protected virtual void IsDead()
        {
            if (hp <= 0)
            {
                // 몬스터 죽음 판정
                isActive = false;
                GameManager.Instance.player.Exp += exp;
            }
        }

        protected virtual void AttackPlayer()
        {
            GameManager.Instance.player.HP -= power;
        }

        protected virtual void ChangeColor()
        {
            #region change_Color_by_HP
            // 체력상태에 따라 객체 색 변경
            // 체력색표기 -> 초록색 > 80% > 노란색 >40% > 빨간색
            if (hp > maxHp * 0.8f) foreGroundColor = ConsoleColor.White;
            else if (hp < maxHp * 0.8f && hp > maxHp * 0.4f) foreGroundColor = ConsoleColor.Yellow;
            else foreGroundColor = ConsoleColor.Red;
            #endregion
            Console.BackgroundColor = backGroundColor;
            Console.ForegroundColor = foreGroundColor;
            Console.Write(shape);
            Console.ResetColor();
        }
    }
}
