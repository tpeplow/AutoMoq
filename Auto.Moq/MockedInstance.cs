namespace Auto.Moq
{
    using System;

    using global::Moq;

    public class MockedInstance
    {
        private readonly object instanceToPassInConstructor;

        public MockedInstance(Type instanceType, Mock instance)
        {
            if (instanceType == null)
            {
                throw new ArgumentNullException("instanceType");
            }
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            InstanceType = instanceType;
            this.Mock = instance;
        }

        public MockedInstance(Type dependencyType, Mock mockInstance, object instanceToPassInConstructor)
            : this (dependencyType, mockInstance)
        {
            this.instanceToPassInConstructor = instanceToPassInConstructor;
        }

        public Type InstanceType { get; private set; }

        public Mock Mock { get; private set; }

        public object InstanceToPassToConstructor
        {
            get
            {
                return this.instanceToPassInConstructor ?? this.Mock.Object;
            }
        }
    }
}