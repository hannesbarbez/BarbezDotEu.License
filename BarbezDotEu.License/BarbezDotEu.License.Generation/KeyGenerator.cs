// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarbezDotEu.License.Shared;

namespace BarbezDotEu.License.Generation
{
    public class KeyGenerator
    {
        private static ConcurrentBag<string> keys;
        private readonly int resultingSum;
        private readonly string divider;

        /// <summary>
        /// Constructs a new <see cref="KeyGenerator"/>.
        /// </summary>
        /// <param name="resultingSum">The expected "sum" of the key. All keys share the same "sum", which is what makes the key validatable.</param>
        /// <param name="divider">The desored division in between segments of the license key.</param>
        public KeyGenerator(int resultingSum, string divider)
        {
            if (string.IsNullOrWhiteSpace(divider) || resultingSum == default)
            {
                throw new ArgumentException(Generic.EXCEPTION);
            }

            this.resultingSum = resultingSum;
            this.divider = divider;
        }

        /// <summary>
        /// Generates a number of license keys.
        /// </summary>
        /// <param name="numberOfKeys">The amount of keys to generate.</param>
        /// <param name="excludedKeys">Keys that cannot be present in the resulting key set.</param>
        /// <returns>The generated license keys.</returns>
        public async Task<string[]> GenerateKeys(uint numberOfKeys, string[] excludedKeys)
        {
            if (numberOfKeys == default)
            {
                throw new ArgumentException(Generic.EXCEPTION);
            }

            var excluded = new HashSet<string>(excludedKeys.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()));
            keys = new ConcurrentBag<string>();
            return await Task.Run(() =>
            {
                Parallel.For(default, numberOfKeys, x =>
                {
                    var key = this.GenerateKey();
                    if (!keys.Contains(key) && !excluded.Contains(key))
                    {
                        keys.Add(key);
                    }
                });

                return keys.ToArray();
            });
        }

        private string GenerateKey()
        {
            var seq1 = GetSequence();
            var seq2 = GetSequence();
            var seq3 = GetSequence();
            var seq4 = GetSequence();
            var seq5 = GetSequence();

            var pt1 = $"{((char)seq1[0])}{((char)seq2[0])}{((char)seq3[0])}{((char)seq4[0])}{((char)seq5[0])}";
            var pt2 = $"{((char)seq1[1])}{((char)seq2[1])}{((char)seq3[1])}{((char)seq4[1])}{((char)seq5[1])}";
            var pt3 = $"{((char)seq1[2])}{((char)seq2[2])}{((char)seq3[2])}{((char)seq4[2])}{((char)seq5[2])}";
            var pt4 = $"{((char)seq1[3])}{((char)seq2[3])}{((char)seq3[3])}{((char)seq4[3])}{((char)seq5[3])}";
            var pt5 = $"{((char)seq1[4])}{((char)seq2[4])}{((char)seq3[4])}{((char)seq4[4])}{((char)seq5[4])}";
            var key = pt1 + divider + pt2 + divider + pt3 + divider + pt4 + divider + pt5;
            return key;
        }

        private int[] GetSequence()
        {
            int tempResult = 0, n0 = 0, n1 = 0, n2 = 0, n3 = 0, n4 = 0;
            while (tempResult != resultingSum)
            {
                // For "Next" parameters (numbers) exlanation, see ASCII code table.
                n0 = new Random(Guid.NewGuid().GetHashCode()).Next(48, 53);
                n1 = new Random(Guid.NewGuid().GetHashCode()).Next(n0 + 2, 57);
                n2 = new Random(Guid.NewGuid().GetHashCode()).Next(65, 76);
                n3 = new Random(Guid.NewGuid().GetHashCode()).Next(n2 + 2, 85);
                n4 = new Random(Guid.NewGuid().GetHashCode()).Next(n3 + 2, 90);

                // Sequence has to match the following formula.
                tempResult = ((n0 + n2 + n4) - (n1 + n3));
            }

            return new int[] { n0, n1, n2, n3, n4 };
        }
    }
}
