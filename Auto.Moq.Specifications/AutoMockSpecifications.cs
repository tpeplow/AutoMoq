namespace Auto.Moq.Specifications
{
    using System.Collections.Generic;

    using Auto.Moq;

    using Machine.Specifications;

    using global::Moq;

    using It = Machine.Specifications.It;

    public class When_auto_mocking
    {
        protected static AutoMoq<SomethingWithADependency> AutoMoq;

        Because of = () => AutoMoq = new AutoMoq<SomethingWithADependency>();

        It should_mock_dependency = () => AutoMoq.Object.Dependency.ShouldNotBeNull();
    }

    public class When_working_with_automocked_type
    {
        protected static AutoMoq<SomethingWithADependency> AutoMoq;

        Establish context = () => AutoMoq = new AutoMoq<SomethingWithADependency>();

        Because of_method_being_invoked = () => AutoMoq.Object.Dependency.AMethod();

        It can_verify_expectations = () => AutoMoq.GetMock<IDependency>().Verify(d => d.AMethod());
    }

    public class When_auto_mocking_type_with_single_generic_dependency
    {
        private static AutoMoq<HasGenericDependency> AutoMoq;

        Because of = () => AutoMoq = new AutoMoq<HasGenericDependency>();

        It should_mock_dependency = () => AutoMoq.Object.Dependency.ShouldNotBeNull();

        private It dependency_works_when_called = () => AutoMoq.Object.Dependency.Invoke();
    }

    public class When_working_with_generic_mock
    {
        static AutoMoq<HasGenericDependency> AutoMoq;

        Establish context = () => AutoMoq = new AutoMoq<HasGenericDependency>();

        Because of_method_being_invoked = () => AutoMoq.Object.Dependency.Invoke();

        It can_verify_expectations = () => AutoMoq.GetMock<IGenericDependency<IDependency>>().Verify(d => d.Invoke());
    }

    public class When_object_to_mock_has_multiple_constructors
    {
        private static AutoMoq<HasMultipleConstructors> AutoMoq;

        Because of = () => AutoMoq = new AutoMoq<HasMultipleConstructors>();

        It should_pick_constructor_with_most_overrides = () => AutoMoq.Object.WasConstructorWithTwoArgumentsCalled.ShouldBeTrue();

        It should_mock_first_parameter = () => AutoMoq.Object.Dependency.ShouldNotBeNull();

        It should_mock_second_parameter = () => AutoMoq.Object.AnotherDependency.ShouldNotBeNull();

        It mock_exists_for_first_paremeter = () => AutoMoq.GetMock<IDependency>().ShouldNotBeNull();

        It mock_exists_for_second_parameter = () => AutoMoq.GetMock<IGenericDependency<IDependency>>().ShouldNotBeNull();
    }
    
    public class When_dependency_is_concrete_type
    {
        private static AutoMoq<HasConcreteDependency> AutoMoq;

        private Because of = () => AutoMoq = new AutoMoq<HasConcreteDependency>();

        private It should_mock_concrete_dependency = () => AutoMoq.GetMock<ConcreteDependency>().ShouldNotBeNull();

        private It concrete_dependencies_dependencies_should_be_mocked = () => AutoMoq.GetMock<ConcreteDependency>().Object.Dependency.ShouldNotBeNull();
    }

    public class When_providing_an_instance
    {
        private static AutoMoq<SomethingWithADependency> AutoMoq;

        private Because of = () => AutoMoq = new AutoMoq<SomethingWithADependency>(new RealDependency());

        private It should_use_real_dependency_in_place_of_moq = () => AutoMoq.Object.Dependency.ShouldBeOfType<RealDependency>();

        private It should_not_generate_mock = () => Catch.Exception(() => AutoMoq.GetMock<RealDependency>()).ShouldBeOfType<KeyNotFoundException>();

        private class RealDependency : IDependency
        {
            public void AMethod()
            {
            }

            public string SomeMethod()
            {
                return string.Empty;
            }
        }
    }

    public class HasConcreteDependency
    {
        public ConcreteDependency Dependency { get; set; }

        public HasConcreteDependency(ConcreteDependency dependency)
        {
            Dependency = dependency;
        }
    }

    public class ConcreteDependency
    {
        public IDependency Dependency { get; set; }

        public ConcreteDependency(IDependency dependency)
        {
            Dependency = dependency;
        }
    }
}