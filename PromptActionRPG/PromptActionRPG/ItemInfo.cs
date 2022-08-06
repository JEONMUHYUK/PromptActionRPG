using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptActionRPG
{
    // 아이템 정보
    internal class ItemInfo
    {
        public string info;
        public int time;
        public string shape;

        public ItemInfo(string info , int time, string shape)
        {   
            this.info = info;
            this.time = time;
            this.shape = shape;
        }
        
    }
}
