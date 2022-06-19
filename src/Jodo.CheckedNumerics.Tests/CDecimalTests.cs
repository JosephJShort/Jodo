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
using FluentAssertions;
using Jodo.Numerics;
using Jodo.Numerics.Tests;
using Jodo.Primitives;
using Jodo.Testing;
using NUnit.Framework;

namespace Jodo.CheckedNumerics.Tests
{
    public sealed class CDecimalTests : GlobalFixtureBase
    {
        public sealed class BitConverter : BitConverterTests<cdecimal> { }
        public sealed class Cast : CastTests<cdecimal> { }
        public sealed class CheckedNumeric : CheckedNumericTests<cdecimal> { }
        public sealed class ConvertTests : ConvertTests<cdecimal> { }
        public sealed class MathFloatingPoint : MathTests.FloatingPoint<cdecimal> { }
        public sealed class MathGeneral : MathTests.General<cdecimal> { }
        public sealed class MathReal : MathTests.Real<cdecimal> { }
        public sealed class MathSigned : MathTests.Signed<cdecimal> { }
        public sealed class NumericGeneral : NumericTests.General<cdecimal> { }
        public sealed class NumericReal : NumericTests.Real<cdecimal> { }
        public sealed class NumericSigned : NumericTests.Signed<cdecimal> { }
        public sealed class ParserGeneral : ParserTests.General<cdecimal> { }

#if NET5_0_OR_GREATER
        [Test, Repeat(RandomVariations)]
        public void IsFinite_RandomValue_AlwaysTrue()
        {
            //arrange
            byte[] bytes = BitConverter<cdecimal>.GetBytes(default);
            Random.NextBytes(bytes[..12]);
            cdecimal input = BitConverter<cdecimal>.FromBytes(bytes);

            //act
            bool result = Numeric<cdecimal>.IsFinite(input);

            //assert
            result.Should().BeTrue();
        }

        [Test, Repeat(RandomVariations)]
        public void IsInfinity_RandomValue_AlwaysFalse()
        {
            //arrange
            byte[] bytes = BitConverter<cdecimal>.GetBytes(default);
            Random.NextBytes(bytes[..12]);
            cdecimal input = BitConverter<cdecimal>.FromBytes(bytes);

            //act
            bool result = Numeric<cdecimal>.IsInfinity(input);

            //assert
            result.Should().BeFalse();
        }

        [Test, Repeat(RandomVariations)]
        public void IsPositiveInfinity_RandomValue_AlwaysFalse()
        {
            //arrange
            byte[] bytes = BitConverter<cdecimal>.GetBytes(default);
            Random.NextBytes(bytes[..12]);
            cdecimal input = BitConverter<cdecimal>.FromBytes(bytes);

            //act
            bool result = Numeric<cdecimal>.IsPositiveInfinity(input);

            //assert
            result.Should().BeFalse();
        }

        [Test, Repeat(RandomVariations)]
        public void IsNegativeInfinity_RandomValue_AlwaysFalse()
        {
            //arrange
            byte[] bytes = BitConverter<cdecimal>.GetBytes(default);
            Random.NextBytes(bytes[..12]);
            cdecimal input = BitConverter<cdecimal>.FromBytes(bytes);

            //act
            bool result = Numeric<cdecimal>.IsNegativeInfinity(input);

            //assert
            result.Should().BeFalse();
        }

        [Test, Repeat(RandomVariations)]
        public void IsNaN_RandomValue_AlwaysFalse()
        {
            //arrange
            byte[] bytes = BitConverter<cdecimal>.GetBytes(default);
            Random.NextBytes(bytes[..12]);
            cdecimal input = Random.NextNumeric<cdecimal>();

            //act
            bool result = Numeric<cdecimal>.IsNaN(input);

            //assert
            result.Should().BeFalse();
        }
#endif
    }
}
