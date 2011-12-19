namespace Auto.Moq
{
    using System;
    using System.Linq;
    using System.Reflection;

    using global::Moq;

    public static class TypeMockingExtenstions
    {
        public static Mock CreateMock(this Type dependencyType)
        {
            var notSoGenericMock = typeof(Mock<>).MakeGenericType(dependencyType);

            return (Mock)notSoGenericMock.GetConstructor(new Type[0]).Invoke(new object[0]);
        }

        public static Mock CreateMock(this Type dependencyType, params object[] mocks)
        {
            var notSoGenericMock = typeof(Mock<>).MakeGenericType(dependencyType);

            return (Mock)notSoGenericMock.GetConstructor(new [] { typeof(object[])}).Invoke(new object[] { mocks });
        }

        public static ConstructorInfo GetMostSpecificConstructor(this Type type)
        {
            return type.GetConstructors().OrderByDescending(c => c.GetParameters().Count()).First();
        }
    }
}