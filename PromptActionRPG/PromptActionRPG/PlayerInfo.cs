using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptActionRPG
{
    // 플레이어 정보 클래스
    internal class PlayerInfo
    {
        public int level;
        public float hp;
        public float maxHP;
        public float exp;
        public float maxExp;
        public float damage;
        public float attackDistance;

        public PlayerInfo(int level, float hp, float maxHP, float exp, float maxExp, float damage, float attackDistance)
        {
            this.level = level;
            this.maxHP = hp;
            this.maxHP = maxHP;
            this.maxExp = exp;
            this.maxExp = maxExp;
            this.damage = damage;
            this.attackDistance = attackDistance;

        }
        public void LevelManager()
        {
            exp = 0;
            maxExp *= 1.5f;
            maxHP *= 1.5f;
            hp = maxHP;
            damage *= 1.5f;
            level++;

            if (attackDistance < 2)
                attackDistance *= 1.5f;
        }
        public void InfoRender()
        {
            // 플레이어 상태 창 랜더링 함수
            Console.Write($"LV       : {level}");
            Console.WriteLine();
            Console.Write($"HP       : {Math.Truncate(hp)} / {Math.Truncate(maxHP)}");
            Console.WriteLine();
            Console.Write($"EXP      : {exp} / {Math.Truncate(maxExp)}");
            Console.WriteLine();
            Console.Write($"Damage   : {Math.Truncate(damage)}");
            Console.WriteLine();
            Console.Write($"Distance : {Math.Truncate(attackDistance)}");
            Console.WriteLine();
        }
    }
}
