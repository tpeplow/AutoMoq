namespace Auto.Moq
{
    using System;
    using System.Collections;
    using System.Linq;

    public class ConcreteMockGenerator : IMockGenerator
    {
        public bool IsRelevant(Type type)
        {
            return type.IsClass && !type.IsAbstract && !typeof(IEnumerable).IsAssignableFrom(type);
        }

        public MockedInstance GenerateMock(MockingContext context, Type type)
        {
            var mocks = type
                           .GetMostSpecificConstructor()
                           .GetParameters()
                           .Select(param => context.GenerateMock(param.ParameterType))
                           .Select(mock => mock.Mock.Object)
                           .ToArray();

            return new MockedInstance(type, type.CreateMock(mocks));
        }
    }
}