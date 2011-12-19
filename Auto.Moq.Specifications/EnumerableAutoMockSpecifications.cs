namespace Auto.Moq.Specifications
{
    using Machine.Specifications;

    public class When_Working_With_Array_Of_Dependencies
    {
        private static AutoMoq<SaveAllCommand> AutoMoq;

        private Establish context = () => AutoMoq = new AutoMoq<SaveAllCommand>();

        private Because of_method_invoke = () => AutoMoq.Object.Execte();

        private It should_invoke_on_all_items_in_array = () => AutoMoq.GetMock<ISaveContactComand>().Verify(x => x.Execute());
    }

    public class When_working_with_an_enumerable_dependency
    {
        private static AutoMoq<HasEnumerableDependency> AutoMoq;

        private Establish context = () => AutoMoq = new AutoMoq<HasEnumerableDependency>();

        private Because of_method_invoke = () => AutoMoq.Object.Execute();

        private It should_invoke_dependency = () => AutoMoq.GetMock<ISaveContactComand>().Verify(x => x.Execute());
    }

    public class When_working_with_a_generic_IList_dependency
    {
        private static AutoMoq<HasIListDependency> AutoMoq;

        private Establish context = () => AutoMoq = new AutoMoq<HasIListDependency>();

        private Because of_method_invoke = () => AutoMoq.Object.Execute();

        private It should_invoke_dependency = () => AutoMoq.GetMock<ISaveContactComand>().Verify(x => x.Execute());
    }

    public class When_working_with_a_generic_List_dependency
    {
        private static AutoMoq<HasListDependency> AutoMoq;

        private Establish context = () => AutoMoq = new AutoMoq<HasListDependency>();

        private Because of_method_invoke = () => AutoMoq.Object.Execute();

        private It should_invoke_dependency = () => AutoMoq.GetMock<ISaveContactComand>().Verify(x => x.Execute());
    }
}