using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptActionRPG
{
    internal abstract class BaseObjects
    {
        public abstract void Update(int deltatick);
        public abstract void Render();
    }
}
