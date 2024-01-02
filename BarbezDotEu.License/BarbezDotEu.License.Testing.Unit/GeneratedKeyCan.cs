// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BarbezDotEu.License.Generation;
using BarbezDotEu.License.Verification;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbezDotEu.License.Testing.Unit
{
    [TestClass]
    public class GeneratedKeyCan
    {
        private const int ScrOneResultingSum = 68;
        private const string DIVIDER = "-";
        private static readonly KeyGenerator KeyGenerator = new(ScrOneResultingSum, DIVIDER);
        private static readonly KeyVerificator keyVerificator = new(ScrOneResultingSum, DIVIDER);

        [TestMethod]
        public void BeVerified()
        {
            // Check numberOfKeys matches
            var numberOfKeys = 30000;
            var excludedKeys = Array.Empty<string>();
            var watch = Stopwatch.StartNew();
            var keys = KeyGenerator.GenerateKeys((uint)numberOfKeys, excludedKeys);
            watch.Stop();
            Console.WriteLine($"{numberOfKeys} keys in {watch.ElapsedMilliseconds / 1000} seconds.");
            Assert.AreEqual(numberOfKeys, keys.Count());

            // Check keys can be verified
            Parallel.ForEach(keys, key =>
            {
                var validKey = keyVerificator.VerifyKey(key);
                Assert.IsTrue(validKey);
            });
        }
    }
}
