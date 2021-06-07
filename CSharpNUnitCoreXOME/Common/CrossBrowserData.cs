using NUnit.Framework;
using System.Collections;

namespace CSharpNUnitCoreXOME.Common
{
    class CrossBrowserData
    {
        public static IEnumerable LatestConfigurations
        {
            get
            {
                yield return new TestFixtureData("Chrome");
                yield return new TestFixtureData("Firefox");
            }
        }

      
    }

}
