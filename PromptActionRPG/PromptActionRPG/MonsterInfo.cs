using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptActionRPG
{
    // 몬스터 정보 정의 클래스
    internal class MonsterInfo
    {
        public float hp;
        public float damage;
        public float attackDistance;
        public int exp;

        public MonsterInfo(float hp, float damage, float attackDistance , int exp)
        {

            this.hp = hp;
            this.damage = damage;
            this.attackDistance = attackDistance;
            this.exp = exp;
        }

    }
}
