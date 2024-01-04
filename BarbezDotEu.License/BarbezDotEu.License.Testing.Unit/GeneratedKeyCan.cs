// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
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
        [DataRow(1)]
        [DataRow(10)]
        [DataRow(100)]
        [DataRow(1000)]
        [DataRow(2500)]
        [DataRow(5000)]
        [DataRow(10000)]
        [DataRow(20000)]
        [DataRow(30000)]
        [DataRow(50000)]
        [DataRow(62500)]
        [DataRow(75000)]
        [DataRow(87500)]
        [DataRow(100000)]
        [DataRow(150000)]
        [DataRow(300000)]
        [DataRow(1000000)]
        public async Task BeVerified(int numberOfKeys)
        {
            // Act
            var watch = Stopwatch.StartNew();
            var keys = await KeyGenerator.GenerateKeys(numberOfKeys, []);
            watch.Stop();
            Console.WriteLine($"| {numberOfKeys:### ### ###} | {watch.ElapsedMilliseconds:### ### ###} | {((decimal)watch.ElapsedMilliseconds/numberOfKeys):#####.###} |");
            Console.WriteLine($"{watch.ElapsedMilliseconds / 1000}s for {numberOfKeys} keys.");

            // Assert
            var uniques = new HashSet<string>(keys);
            Assert.AreEqual(numberOfKeys, uniques.Count);
            Assert.AreEqual(numberOfKeys, keys.Count());
            Parallel.ForEach(keys, key =>
            {
                var validKey = keyVerificator.VerifyKey(key);
                Assert.IsTrue(validKey);
            });
        }
    }
}
