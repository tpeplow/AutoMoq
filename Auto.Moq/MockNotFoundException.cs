using System;

namespace Auto.Moq
{
    public class MockNotFoundException : Exception
    {
        public MockNotFoundException(Type dependency)
            : base(string.Format("The object being auto mocked does not have a mock for {0}.  Is it a dependency, or have you provided a concrete implementation?", dependency))
        {
            Dependency = dependency;
        }

        public Type Dependency { get; protected set; }
    }
}