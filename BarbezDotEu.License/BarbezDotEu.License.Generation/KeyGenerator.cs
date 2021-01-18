// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarbezDotEu.License.Generation
{
    public class KeyGenerator
    {
        private readonly int resultingSum;
        private readonly decimal modulo25;
        private readonly decimal multiplier60;
        private readonly int upper;
        public const string EXCEPTION = "One or more parameters are invalid. NULL, negative, empty or default values are not valid parameters.";
        private static ConcurrentBag<string> keys;
        private readonly string divider;

        /// <summary>
        /// Constructs a new <see cref="KeyGenerator"/>.
        /// </summary>
        /// <param name="resultingSum">The expected "sum" of the key. All keys share the same "sum", which is what makes the key validatable. Negative or default values are not valid.</param>
        /// <param name="divider">The desored division in between segments of the license key.</param>
        public KeyGenerator(int resultingSum, string divider)
        {
            if (string.IsNullOrWhiteSpace(divider) || resultingSum <= 0)
            {
                throw new ArgumentException(EXCEPTION);
            }

            this.resultingSum = resultingSum;
            this.divider = divider;

            // 0-25 = 26 letters of English alphabet.
            this.modulo25 = resultingSum % 25;
            this.multiplier60 = Math.Floor(resultingSum / new decimal(60));
            this.upper = (int)Math.Floor(90 - (modulo25 / 3));
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
                throw new ArgumentException(EXCEPTION);
            }

            var excluded = new HashSet<string>(excludedKeys.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()));
            keys = new ConcurrentBag<string>();
            return await Task.Run(() =>
            {
                Parallel.For(default, numberOfKeys, x =>
                {
                    var validKey = false;
                    while (!validKey)
                    {
                        var key = this.GenerateKey();
                        validKey = !keys.Contains(key) && !excluded.Contains(key);
                        if (validKey)
                        {
                            keys.Add(key);
                        }
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
            var calc = Math.Floor(resultingSum / multiplier60);
            while (tempResult != calc)
            {
                // 48 = 0; 90 = Z; 57 = 9; 65 = A.
                n0 = new Random(Guid.NewGuid().GetHashCode()).Next(48, 57 - 2);
                n1 = new Random(Guid.NewGuid().GetHashCode()).Next(n0 + 2, 57);
                n2 = new Random(Guid.NewGuid().GetHashCode()).Next(65, this.upper - 3);
                n3 = new Random(Guid.NewGuid().GetHashCode()).Next(n2 + 2, this.upper - 2);
                n4 = new Random(Guid.NewGuid().GetHashCode()).Next(n3 + 2, this.upper);

                // Sequence has to match the following formula.
                tempResult = ((n0 + n2 + n4) - (n1 + n3));
            }

            return new int[] { n0, n1, n2, n3, n4 };
        }
    }
}
