namespace Auto.Moq
{
    using System;

    public class ArrayMockGenerator : IMockGenerator
    {
        public bool IsRelevant(Type type)
        {
            return type.IsArray;
        }

        public MockedInstance GenerateMock(MockingContext context, Type type)
        {
            var dependencyType = type.GetElementType();
            var mockInstance = context.GenerateMock(dependencyType);
            var array = Array.CreateInstance(dependencyType, 1);

            for (var i = 0; i < array.Length; i++)
            {
                array.SetValue(mockInstance.InstanceToPassToConstructor, i);
            }

            return new MockedInstance(dependencyType, mockInstance.Mock, array);
        }
    }
}