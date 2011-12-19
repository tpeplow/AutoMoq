namespace Auto.Moq.Specifications
{
    using System.Collections.Generic;

    using Machine.Specifications.Utility;

    public class SomethingWithADependency
    {
        public SomethingWithADependency(IDependency dependency)
        {
            Dependency = dependency;
        }

        public IDependency Dependency { get; set; }
    }

    public class HasGenericDependency
    {
        public HasGenericDependency(IGenericDependency<IDependency> dependency)
        {
            Dependency = dependency;
        }

        public IGenericDependency<IDependency> Dependency { get; set; }
    }

    public class HasMultipleConstructors
    {
        public IDependency Dependency { get; set; }

        public IGenericDependency<IDependency> AnotherDependency { get; set; }

        public bool WasConstructorWithTwoArgumentsCalled { get; private set; }

        public HasMultipleConstructors(IDependency dependency)
        {
            Dependency = dependency;
        }

        public HasMultipleConstructors(IDependency dependency, IGenericDependency<IDependency> anotherDependency)
        {
            WasConstructorWithTwoArgumentsCalled = true;
            Dependency = dependency;
            AnotherDependency = anotherDependency;
        }
    }

    public interface IDependency
    {
        void AMethod();

        string SomeMethod();
    }

    public interface IGenericDependency<T>
    {
        void Invoke();
    }

    public class SaveClientCommand
    {
        private readonly SaveContactCommand contactCommand;

        private string ContactName;

        public SaveClientCommand(SaveContactCommand contactCommand)
        {
            this.contactCommand = contactCommand;
        }

        public void Execute()
        {
            // save client

            this.contactCommand.Firstname = this.ContactName;
        }
    }

    public class SaveContactCommand
    {
        public int ClientId { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public void Execute()
        {
            
        }
    }

    public class SaveAllCommand
    {
        private readonly ISaveContactComand[] command;

        public SaveAllCommand(ISaveContactComand[] command)
        {
            this.command = command;
        }

        public void Execte()
        {
            command.Each(x => x.Execute());
        }
    }

    public class HasEnumerableDependency
    {
        private readonly IEnumerable<ISaveContactComand> commands;

        public HasEnumerableDependency(IEnumerable<ISaveContactComand> commands)
        {
            this.commands = commands;
        }

        public void Execute()
        {
            commands.Each(x => x.Execute());
        }
    }

    public class HasIListDependency : HasEnumerableDependency
    {
        public HasIListDependency(IList<ISaveContactComand> commands) : base (commands)
        {
        }
    }

    public class HasListDependency : HasEnumerableDependency
    {
        public HasListDependency(List<ISaveContactComand> commands)
            : base(commands)
        {
        }
    }

    public interface ISaveContactComand
    {
        void Execute();
    }
}