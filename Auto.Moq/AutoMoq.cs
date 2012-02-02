namespace Auto.Moq
{
    using System;
    using System.Collections.Generic;

    using global::Moq;

    public class AutoMoq<T>
        where T : class
    {
        private readonly MockingContext mockingContext;

        private readonly Type typeToMock = typeof(T);

        private readonly Dictionary<Type, Mock> mocks = new Dictionary<Type, Mock>();
        
        public AutoMoq(params object[] dependencies) : this(MockingContext.Create(dependencies))
        {
        }

        public AutoMoq(MockingContext mockingContext)
        {
            this.mockingContext = mockingContext;
            this.MockDependencies();
        }

        private void MockDependencies()
        {
            var constructor = this.typeToMock.GetMostSpecificConstructor();
            var constructorValues = new List<object>();
            foreach (var param in constructor.GetParameters())
            {
                var realDependency = mockingContext.FindRealDependency(param.ParameterType);
                if (realDependency != null)
                {
                    constructorValues.Add(realDependency);
                    continue;
                }
                var result = mockingContext.GenerateMock(param.ParameterType);

                constructorValues.Add(result.InstanceToPassToConstructor);
                mocks.Add(result.InstanceType, result.Mock);
            }

            this.Object = (T)constructor.Invoke(constructorValues.ToArray());
        }

        public Mock<TDependency> GetMock<TDependency>() where TDependency : class
        {
            Mock mock;
            var found = mocks.TryGetValue(typeof(TDependency), out mock);
            if (!found) throw new MockNotFoundException(typeof(TDependency));
            return (Mock<TDependency>)mock;
        }

        public T Object { get; private set; }
    }
}