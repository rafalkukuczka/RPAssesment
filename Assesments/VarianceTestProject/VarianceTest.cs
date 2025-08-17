namespace VarianceTestProject
{
    public class VarianceTest
    {
        [Fact]
        public void CovarianceTest()
        {
            // Covariance with IEnumerable<T>
            IEnumerable<string> strings = new List<string> { "a", "b", "c" };
            IEnumerable<object> objects = strings; // Covariance: IEnumerable<string> -> IEnumerable<object>

            Assert.Equal(3, objects.Count());
            Assert.Contains("a", objects);
        }

        [Fact]
        public void ContravarianceTest()
        {
            // Contravariance with Action<T>
            Action<object> actObject = o => Assert.NotNull(o);
            Action<string> actString = actObject; // Contravariance: Action<object> -> Action<string>

            actString("hello"); // Should not throw
        }

    }
}
