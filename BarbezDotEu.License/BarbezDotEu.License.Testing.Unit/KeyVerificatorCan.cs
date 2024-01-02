// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using BarbezDotEu.License.Verification;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbezDotEu.License.Testing.Unit
{
    [TestClass]
    public class KeyVerificatorCan
    {
        private const int ScrOneResultingSum = 68;
        private const string DIVIDER = "-";
        private static readonly KeyVerificator keyVerificator = new(ScrOneResultingSum, DIVIDER);

        // Valid and invalid key samples depend on resulting set.
        private const string ScrOneValidKeySample = "14022-58565-CDCFA-ORRNQ-TVXPW";
        private const string ScrOneInvalidKeySample = "14022-58565-CDCFA-ORRNQ-TVXPA";

        [TestMethod]
        public void PassVerification()
        {
            var validKey = keyVerificator.VerifyKey(ScrOneValidKeySample);
            Assert.IsTrue(validKey);
        }

        [TestMethod]
        public void FailVerification()
        {
            var validKey = keyVerificator.VerifyKey(ScrOneInvalidKeySample);
            Assert.IsFalse(validKey);
        }
    }
}
