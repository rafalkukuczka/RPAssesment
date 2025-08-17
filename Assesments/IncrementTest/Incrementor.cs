using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncrementTest
{
    internal class Incrementor
    {
        private static int _sharedVariable = 0;
        private static readonly object _lock = new object();

        public static int SharedVariable
        {
            get { return _sharedVariable; }            
        }

        public Incrementor()
        {
            _sharedVariable = 0;
        }

        //1. Using `Interlocked` Critical Section keyword
        public void Increment1()
        {
            lock (_lock)
            {
                _sharedVariable++;
            }
        }

        //2. Using `Interlocked` 
        public void Increment2()
        {
            Interlocked.Increment(ref _sharedVariable);
        }

        //3. Using `Increment_NotSafe` just for xUnit verification
        public void Increment_NotSafe()
        {
            _sharedVariable++;
        }
    }
}
