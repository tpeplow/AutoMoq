namespace Auto.Moq
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class EnumerableMockGenerator : IMockGenerator
    {
        public bool IsRelevant(Type type)
        {
            return type
                .GetInterfaces()
                .Union(new [] { type })
                .Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>));
        }

        public MockedInstance GenerateMock(MockingContext context, Type type)
        {
            var elementType = type.GetGenericArguments()[0];
            var mockInstance = context.GenerateMock(elementType);
            var list = (IList)typeof(List<>).MakeGenericType(elementType).GetConstructor(new Type[0]).Invoke(new object[0]);
            list.Add(mockInstance.InstanceToPassToConstructor);

            return new MockedInstance(elementType, mockInstance.Mock, list);
        }
    }
}