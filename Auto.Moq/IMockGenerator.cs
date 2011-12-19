namespace Auto.Moq
{
    using System;

    public interface IMockGenerator
    {
        bool IsRelevant(Type type);

        MockedInstance GenerateMock(MockingContext context, Type type);
    }
}