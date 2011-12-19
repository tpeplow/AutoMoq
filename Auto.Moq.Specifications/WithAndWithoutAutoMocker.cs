namespace Auto.Moq.Specifications
{
    using Auto.Moq;

    using global::Moq;

    public class WithAndWithoutAutoMocker
    {
        private Mock<IDependency> dependency;

        private SomethingWithADependency thingToTest;

        public void InitWithoutAutoMocker()
        {
            this.dependency = new Mock<IDependency>();
            this.thingToTest = new SomethingWithADependency(this.dependency.Object);
            this.dependency.Setup(d => d.SomeMethod()).Returns("hello world");
        }

        private AutoMoq<SomethingWithADependency> autoMockedThingToTest;

        public void InitWithAutoMocker()
        {
            this.autoMockedThingToTest = new AutoMoq<SomethingWithADependency>();
            this.autoMockedThingToTest.GetMock<IDependency>().Setup(d => d.SomeMethod()).Returns("hello world");
        }
    }
}