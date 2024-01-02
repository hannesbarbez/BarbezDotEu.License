﻿// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using System;
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
        private static readonly KeyGenerator KeyGenerator = new(ScrOneResultingSum, DIVIDER);
        private static readonly KeyVerificator keyVerificator = new(ScrOneResultingSum, DIVIDER);

        // Valid and invalid key samples depend on resulting set.
        private const string ScrOneValidKeySample = "14022-58565-CDCFA-ORRNQ-TVXPW";
        private const string ScrOneInvalidKeySample = "14022-58565-CDCFA-ORRNQ-TVXPA";

        [TestMethod]
        public async Task GeneratedKeyCanBeVerifiedAsync()
        {
            // Check numberOfKeys matches
            var numberOfKeys = 10000;
            var excludedKeys = Array.Empty<string>();
            var keys = await KeyGenerator.GenerateKeys((uint)numberOfKeys, excludedKeys);
            Assert.AreEqual(numberOfKeys, keys.Length);

            // Check keys can be verified
            Parallel.ForEach(keys, key =>
            {
                var validKey = keyVerificator.VerifyKey(key);
                Assert.IsTrue(validKey);
            });
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
