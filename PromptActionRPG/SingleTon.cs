using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptActionRPG
{
    internal class SingleTon<T> where T : class, new()
    {
        static volatile T instance = null;
        static object instanceLock = new object();

        public static T Instance
        { 
            get 
            {
                if (instance == null)
                {
                    lock (instanceLock)
                    {
                        if (instance == null)
                        { 
                            instance = new T();
                        } 
                        return instance;
                    }
                }
                return instance; 
            } 
        }
    }
}
