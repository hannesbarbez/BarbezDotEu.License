﻿// Copyright (c) Hannes Barbez. All rights reserved.
// Licensed under the GNU General Public License v3.0

using System;
using BarbezDotEu.License.Shared;

namespace BarbezDotEu.License.Verification
{
    public class KeyVerificator
    {
        private const int SYMMETRICLENGTH = 5;
        private readonly int resultingSum;
        private readonly string divider;

        public KeyVerificator(int resultingSum, string divider)
        {
            if (string.IsNullOrWhiteSpace(divider) || resultingSum == default)
            {
                throw new ArgumentException(Generic.EXCEPTION);
            }

            this.resultingSum = resultingSum;
            this.divider = divider;
        }

        /// <summary>
        /// Checks if a key is valid as well as all of its segments.
        /// </summary>
        /// <returns>True if the key is valid; false otherwise.</returns>
        private bool ValidateSegments(string segment1, string segment2, string segment3, string segment4, string segment5)
        {
            try
            {
                string seq1 = $"{segment1[0]}{segment2[0]}{segment3[0]}{segment4[0]}{segment5[0]}";
                string seq2 = $"{segment1[1]}{segment2[1]}{segment3[1]}{segment4[1]}{segment5[1]}";
                string seq3 = $"{segment1[2]}{segment2[2]}{segment3[2]}{segment4[2]}{segment5[2]}";
                string seq4 = $"{segment1[3]}{segment2[3]}{segment3[3]}{segment4[3]}{segment5[3]}";
                string seq5 = $"{segment1[4]}{segment2[4]}{segment3[4]}{segment4[4]}{segment5[4]}";

                if (ValidateSegment(seq1))
                    if (ValidateSegment(seq2))
                        if (ValidateSegment(seq3))
                            if (ValidateSegment(seq4))
                                if (ValidateSegment(seq5))
                                    if (ValidKey(segment1, segment2, segment3, segment4, segment5))
                                        return true;
                return false;
            }
            catch { return false; }
        }

        public bool VerifyKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException(Generic.EXCEPTION);
            }

            var segments = key.Trim().Split(this.divider);
            if (segments.Length != SYMMETRICLENGTH)
            {
                throw new ArgumentException(Generic.EXCEPTION);
            }

            return this.ValidateSegments(segments[0], segments[1], segments[2], segments[3], segments[4]);
        }

        /// <summary>
        /// Checks if inputted key is valid
        /// </summary>
        /// <returns>True if valid, false if invalid.</returns>
        private bool ValidKey(string segment1, string segment2, string segment3, string segment4, string segment5)
        {
            int i1 = 0;
            int i2 = 0;
            int i3 = 0;
            int i4 = 0;
            int i5 = 0;

            if ((segment1 + segment2 + segment3 + segment4 + segment5).Length == (SYMMETRICLENGTH * 5))
            {
                foreach (char c in segment1)
                    i1 += c;
                foreach (char c in segment2)
                    i2 += c;
                foreach (char c in segment3)
                    i3 += c;
                foreach (char c in segment4)
                    i4 += c;
                foreach (char c in segment5)
                    i5 += c;
            }

            // Entire key has to match:
            if (i1 < i2)
                if (i2 < i3)
                    if (i3 < i4)
                        if (i4 < i5)
                            return true;

            return false;
        }

        /// <summary>
        /// Checks if a segment valid.
        /// </summary>
        /// <param name="segment">The segment to interpret.</param>
        /// <returns>True if valid, false if invalid.</returns>
        private bool ValidateSegment(string segment)
        {
            if (segment.Length == SYMMETRICLENGTH)
            {
                int n0 = segment[0];
                int n1 = segment[1];
                int n2 = segment[2];
                int n3 = segment[3];
                int n4 = segment[4];

                // Segment has to match:
                if (((n0 + n2 + n4) - (n1 + n3)) == resultingSum) return true;
            }

            return false;
        }

    }
}
