using System.Linq;
using System.Threading.Tasks;
using BarbezDotEu.License.Generation;
using BarbezDotEu.License.Verification;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbezDotEu.License.Testing.Unit
{
    [TestClass]
    public class UnitTests
    {
        private const int ScrOneResultingSum = 68;
        private const string DIVIDER = "-";
        private static readonly KeyGenerator KeyGenerator = new KeyGenerator(ScrOneResultingSum, DIVIDER);
        private static readonly KeyVerificator keyVerificator = new KeyVerificator(ScrOneResultingSum, DIVIDER);

        // Valid and invalid key samples depend on resulting set.
        private const string ScrOneValidKeySample = "14022-58565-CDCFA-ORRNQ-TVXPW";
        private const string ScrOneInvalidKeySample = "14022-58565-CDCFA-ORRNQ-TVXPA";

        [TestMethod]
        public async Task GeneratedKeyCanBeVerifiedAsync()
        {
            // Check numberOfKeys matches
            var numberOfKeys = 2500;
            var excludedKeys = new string[] { };
            var keys = await KeyGenerator.GenerateKeys((uint)numberOfKeys, excludedKeys);
            Assert.AreEqual(numberOfKeys, keys.Length);

            // Check keys can be verified
            var validKey = keyVerificator.VerifyKey(keys.First());
            Assert.IsTrue(validKey);
        }

        [TestMethod]
        public void OlderKeyCanPassVerification()
        {
            var validKey = keyVerificator.VerifyKey(ScrOneValidKeySample);
            Assert.IsTrue(validKey);
        }

        [TestMethod]
        public void OlderKeyCanFailVerification()
        {
            var validKey = keyVerificator.VerifyKey(ScrOneInvalidKeySample);
            Assert.IsFalse(validKey);
        }
    }
}
