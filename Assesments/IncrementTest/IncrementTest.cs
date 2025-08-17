using System.Diagnostics.Metrics;
using System.Threading;

namespace IncrementTest
{
    
    public class IncrementTest
    {
        Incrementor incrementor;
        public IncrementTest()
        {
             incrementor = new Incrementor();
        }

        [Fact]
        public void Increment1_IsThreadSafe()
        {
            var counter = new Incrementor();
            var tasks = new List<Task>();

            for (int i = 0; i < 1000; i++)
            {
                tasks.Add(Task.Run(() => counter.Increment1()));
            }

#pragma warning disable xUnit1031 // Do not use blocking task operations in test method
            Task.WaitAll(tasks.ToArray());
#pragma warning restore xUnit1031 // Do not use blocking task operations in test method

            Assert.Equal(1000, Incrementor.SharedVariable);
        }

        [Fact]
        public void Increment2_IsThreadSafe()
        {
            var counter = new Incrementor();
            var tasks = new List<Task>();

            for (int i = 0; i < 1000; i++)
            {
                tasks.Add(Task.Run(() => counter.Increment2()));
            }

#pragma warning disable xUnit1031 // Do not use blocking task operations in test method
            Task.WaitAll(tasks.ToArray());
#pragma warning restore xUnit1031 // Do not use blocking task operations in test method

            Assert.Equal(1000, Incrementor.SharedVariable);
        }

        [Fact]
        public void Increment_NotSafe() //just for xUnit verification, to check if it recognizes the not-safe issue
        {
            RetryHelper.Retry(() =>                 //Any FLAKY test should be retried, so we use RetryHelper
            {
                var counter = new Incrementor();
                var tasks = new List<Task>();
                for (int i = 0; i < 1000; i++)
                {
                    tasks.Add(Task.Run(() => counter.Increment_NotSafe()));
                }

#pragma warning disable xUnit1031 // Do not use blocking task operations in test method
                Task.WaitAll(tasks.ToArray());
#pragma warning restore xUnit1031 // Do not use blocking task operations in test method

                Assert.NotEqual(1000, Incrementor.SharedVariable);
            }, 10); // Retry up to 10 times if the test fails



        }
    }
}
