// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace BarbezDotEu.License.Generation
{
    /// <summary>
    /// Basic key generator class.
    /// </summary>
    public class KeyGenerator
    {
        /// <summary>
        /// One or more parameters are invalid. NULL, negative, empty or default values are not valid parameters.
        /// </summary>
        public const string EXCEPTION = "One or more parameters are invalid. NULL, negative, empty or default values are not valid parameters.";
        private readonly decimal expectedResult;
        private readonly string divider;
        private readonly int upper;
        private readonly ConcurrentBag<string> keys = [];

        /// <summary>
        /// Gets the last set of generated keys.
        /// </summary>
        public IEnumerable<string> LastKeySet => keys;

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

            // 0-25 = 26 letters of English alphabet.
            decimal modulo25 = resultingSum % 25;
            var multiplier60 = Math.Floor(resultingSum / new decimal(60));
            this.upper = (int)Math.Floor(90 - (modulo25 / 3));
            this.expectedResult = Math.Floor(resultingSum / multiplier60);
            this.divider = divider;
        }

        /// <summary>
        /// Generates a number of license keys.
        /// </summary>
        /// <param name="numberOfKeys">The amount of keys to generate.</param>
        /// <param name="excludedKeys">Keys that cannot be present in the resulting key set.</param>
        /// <returns>The generated license keys.</returns>
        public async Task<IEnumerable<string>> GenerateKeys(int numberOfKeys, IEnumerable<string> excludedKeys)
        {
            if (numberOfKeys <= 0)
            {
                throw new ArgumentException(EXCEPTION);
            }

            var excluded = excludedKeys == null ? [] : new ConcurrentBag<string>(new HashSet<string>(excludedKeys.Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x))));
            ConcurrentDictionary<string, object> allKeys = [];
            keys.Clear();
            var cancellationTokenSource = new CancellationTokenSource();
            var parallelOptions = new ParallelOptions { CancellationToken = cancellationTokenSource.Token };
            var generateKeys = GenerateKeys(parallelOptions, allKeys);
            var selectKeys = SelectKeys(numberOfKeys, excluded, cancellationTokenSource, parallelOptions, allKeys);
            generateKeys.Start();
            selectKeys.Start();

            // One task generates keys in a brute fashion, the other selects the useful ones out.
            await Task.WhenAll(new Task[] { generateKeys, selectKeys });
            return keys.Take(numberOfKeys);
        }

        private Task GenerateKeys(ParallelOptions parallelOptions, ConcurrentDictionary<string, object> allKeys)
        {
            return new Task(() =>
            {
                try
                {
                    // Theoretically ensures we'll never leave loop unless canceled.
                    // If canceled, escape loop by setting to loop-ending value.
                    Parallel.For(default, long.MaxValue, parallelOptions, x =>
                    {
                        allKeys.TryAdd(GenerateKey(), default);
                    });
                }
                catch (OperationCanceledException)
                {
                    // Do nothing, cancelling is way of communicating we have all needed keys.
                }
            });
        }

        private Task SelectKeys(
            int numberOfKeys,
            ConcurrentBag<string> excluded,
            CancellationTokenSource cancellationTokenSource,
            ParallelOptions parallelOptions,
            ConcurrentDictionary<string, object> allKeys)
        {
            return new Task(() =>
            {
                try
                {
                    while (keys.Count < numberOfKeys)
                    {
                        var snapshot = new ConcurrentBag<string>(allKeys.Keys);
                        Parallel.ForEach(snapshot, parallelOptions, key =>
                        {
                            if (!excluded.Contains(key))
                            {
                                keys.Add(key);
                                if (keys.Count >= numberOfKeys)
                                    cancellationTokenSource.Cancel(false);

                                allKeys.TryRemove(key, out _);
                            }
                        });
                    }
                }
                catch (OperationCanceledException)
                {
                    // Do nothing, cancelling is way of communicating we have all needed keys.
                }
            });
        }

        private string GenerateKey()
        {
            var seq1 = GetSequence();
            var seq2 = GetSequence();
            var seq3 = GetSequence();
            var seq4 = GetSequence();
            var seq5 = GetSequence();

            var pt1 = $"{seq1[0]}{seq2[0]}{seq3[0]}{seq4[0]}{seq5[0]}";
            var pt2 = $"{seq1[1]}{seq2[1]}{seq3[1]}{seq4[1]}{seq5[1]}";
            var pt3 = $"{seq1[2]}{seq2[2]}{seq3[2]}{seq4[2]}{seq5[2]}";
            var pt4 = $"{seq1[3]}{seq2[3]}{seq3[3]}{seq4[3]}{seq5[3]}";
            var pt5 = $"{seq1[4]}{seq2[4]}{seq3[4]}{seq4[4]}{seq5[4]}";
            return pt1 + divider + pt2 + divider + pt3 + divider + pt4 + divider + pt5;
        }

        private string GetSequence()
        {
            int tempResult = 0, n0 = 0, n1 = 0, n2 = 0, n3 = 0, n4 = 0;
            while (tempResult != expectedResult)
            {
                // 48 = 0; 90 = Z; 57 = 9; 65 = A.
                n0 = RandomNumberGenerator.GetInt32(48, 57 - 2);
                n1 = RandomNumberGenerator.GetInt32(n0 + 2, 57);
                n2 = RandomNumberGenerator.GetInt32(65, this.upper - 3);
                n3 = RandomNumberGenerator.GetInt32(n2, this.upper - 2);
                n4 = RandomNumberGenerator.GetInt32(n3 + 2, this.upper);

                // Sequence has to match the following formula.
                tempResult = n0 + n2 + n4 - (n1 + n3);
            }

            return $"{(char)n0}{(char)n1}{(char)n2}{(char)n3}{(char)n4}";
        }
    }
}
