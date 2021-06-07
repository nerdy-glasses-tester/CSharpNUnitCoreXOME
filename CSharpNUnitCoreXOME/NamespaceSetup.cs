using NUnit.Framework;

namespace CSharpNUnitCoreXOME
{
    [SetUpFixture]
    public static class NamespaceSetup
    {
        [OneTimeSetUp]
        public static void ExecuteForCreatingReportsNamespace()
        {
            Reporter.StartReporter();
        }
    }
}
