using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncrementTest
{
    public static class RetryHelper
    {
        public static void Retry(Action action, int attempts = 3)
        {
            for (int i = 1; i <= attempts; i++)
            {
                try
                {
                    action();
                    return; 
                }
                catch when (i < attempts) { }
            }
        }
    }
}
