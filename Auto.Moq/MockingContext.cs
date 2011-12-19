namespace Auto.Moq
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MockingContext
    {
        private readonly List<IMockGenerator> mockGenerators = new List<IMockGenerator>();
        private readonly List<object> realDependencies = new List<object>();

        public MockingContext()
        {
            DefaultMockGenerator = new DefaultMockGenerator();
        }

        public static MockingContext Create(object[] dependencies)
        {
            var context = new MockingContext();
            context.AddMockGenerator(new ArrayMockGenerator());
            context.AddMockGenerator(new EnumerableMockGenerator());
            context.AddMockGenerator(new ConcreteMockGenerator());

            if (dependencies != null)
            {
                foreach (var dependency in dependencies)
                {
                    context.AddDependency(dependency);
                }
            }
            return context;
        }

        protected IMockGenerator DefaultMockGenerator { get; private set; }

        public MockingContext AddDependency(object dependency)
        {
            if (dependency == null)
            {
                throw new ArgumentNullException("dependency");
            }
            if (realDependencies.Any(x => x.GetType() == dependency.GetType()))
            {
                throw new ArgumentException("A specific dependency of this type has already been added", "dependency");
            }

            realDependencies.Add(dependency);

            return this;
        }

        public MockingContext AddMockGenerator(IMockGenerator mockGenerator)
        {
            if (mockGenerator == null)
            {
                throw new ArgumentNullException("mockGenerator");
            }

            mockGenerators.Add(mockGenerator);

            return this;
        }

        public MockingContext SetDefaultMockGenerator(IMockGenerator mockGenerator)
        {
            if (mockGenerator == null)
            {
                throw new ArgumentNullException("mockGenerator");
            }

            DefaultMockGenerator = mockGenerator;
            return this;
        }

        public object FindRealDependency(Type parameterType)
        {
            return realDependencies.FirstOrDefault(x => parameterType.IsAssignableFrom(x.GetType()));
        }

        public MockedInstance GenerateMock(Type parameterType)
        {
            return this.GetMockGenerator(parameterType).GenerateMock(this, parameterType);
        }

        private IMockGenerator GetMockGenerator(Type parameterType)
        {
            return (this.mockGenerators.FirstOrDefault(x => x.IsRelevant(parameterType)) ?? this.DefaultMockGenerator);
        }
    }
}