using System.Collections;

namespace VicStelmak.Sma.UserMicroservice.Tests
{
    internal class UpdateUserAsyncTestDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] { null, UserFixture.CreateUpdateUserRequest() },
            new object[] { Guid.NewGuid().ToString(), null }
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
