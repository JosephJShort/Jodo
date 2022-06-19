﻿// Copyright (c) 2022 Joseph J. Short
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.

using System;
using FluentAssertions.Execution;
using FluentAssertions.Numeric;
using Jodo.Numerics;

namespace FluentAssertions
{
    public static class FluentAssertionsExtensions
    {
        public static AndConstraint<ComparableTypeAssertions<N>> BeApproximately<N>(this ComparableTypeAssertions<N> parent, double expected, string because = "", params object[] becauseArgs) where N : struct, INumeric<N>
        {
            if (!double.IsFinite(expected)) throw new ArgumentOutOfRangeException(nameof(expected), expected, "Must be finite.");

            double actual = Convert<N>.ToDouble((N)parent.Subject);
            if (!Numeric<N>.IsReal)
            {
                double expectedValue = Math.Truncate(expected);
                Execute.Assertion
                    .ForCondition(actual == expectedValue)
                    .BecauseOf(because, becauseArgs)
                    .FailWith("Expected {context:value} to be {0} (integral equivalent to {1}){reason}, but it was {2}.", expectedValue, expected, actual);
            }
            else
            {
                double expectedValue = Math.Round(expected, 2);
                double actualValue = Math.Round(actual, 2);
                Execute.Assertion
                    .ForCondition(actualValue == expectedValue)
                    .BecauseOf(because, becauseArgs)
                    .FailWith("Expected {context:value} to be approximately {0}{reason}, but it was {1}.", expectedValue, actualValue);
            }

            return new AndConstraint<ComparableTypeAssertions<N>>(parent);
        }

        public static AndConstraint<ComparableTypeAssertions<N>> BeApproximately<N>(this ComparableTypeAssertions<N> parent, N expected, byte significantDigits = 3, string because = "", params object[] becauseArgs) where N : struct, INumeric<N>
        {
            if (!Numeric<N>.IsFinite(expected)) throw new ArgumentOutOfRangeException(nameof(expected), expected, "Must be finite.");

            N actual = (N)parent.Subject;
            N difference = Math<N>.Max(actual, expected).Subtract(Math<N>.Min(actual, expected));

            N tolerance = expected.IsGreaterThan(Numeric<N>.MinUnit) && expected.IsLessThan(Numeric<N>.MaxUnit) ?
                (Numeric<N>.One.Divide(Numeric<N>.Ten)) :
                Math<N>.Abs(expected.Divide(Numeric<N>.Ten));

            Execute.Assertion
                .ForCondition(difference.IsLessThanOrEqualTo(tolerance))
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:value} to be approximately {0}{reason}, but it was {1} (difference of {2}).",
                expected, parent.Subject, difference);

            return new AndConstraint<ComparableTypeAssertions<N>>(parent);
        }
    }
}