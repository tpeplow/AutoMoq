namespace Auto.Moq
{
    using System;

    public class DefaultMockGenerator : IMockGenerator
    {
        public bool IsRelevant(Type type)
        {
            return true;
        }

        public MockedInstance GenerateMock(MockingContext context, Type type)
        {
            var mockInstance = type.CreateMock();

            return new MockedInstance(type, mockInstance);
        }
    }
}