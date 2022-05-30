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

using FluentAssertions;
using NUnit.Framework;
using System;

namespace Jodo.Extensions.Numerics.Tests
{
    public static class CastTests
    {
        public class XByte : Base<xbyte> { }
        public class XDecimal : Base<xdecimal> { }
        public class XDouble : Base<xdouble> { }
        public class XFloat : Base<xfloat> { }
        public class XInt : Base<xint> { }
        public class XLong : Base<xlong> { }
        public class XSByte : Base<xsbyte> { }
        public class XShort : Base<xshort> { }
        public class XUInt : Base<xuint> { }
        public class XULong : Base<xulong> { }
        public class XUShort : Base<xushort> { }

        public abstract class Base<N> : NumericTestBase<N> where N : struct, INumeric<N>
        {

            [Test, Repeat(RandomVariations)]
            public void ToByte_RoundTrip_SameValue()
            {
                //arrange
                var input = Math<N>.Truncate(Random.NextNumeric<N>(byte.MinValue, byte.MaxValue));

                //act
                var result = Cast<N>.ToValue(Cast<N>.ToByte(input));

                //assert
                result.Should().Be(input);
            }

            [Test, Repeat(RandomVariations)]
            public void ToDecimal_SmallValueRoundTrip_SameValue()
            {
                //arrange
                var input = Random.NextNumeric<N>(-10, 10);

                //act
                var result = Cast<N>.ToValue(Cast<N>.ToDecimal(input));

                //assert
                result.Should().BeApproximately(input);
            }

            [Test, Repeat(RandomVariations)]
            public void ToDouble_SmallValueRoundTrip_SameValue()
            {
                //arrange
                var input = Random.NextNumeric<N>(-10, 10);

                //act
                var result = Cast<N>.ToValue(Cast<N>.ToDouble(input));

                //assert
                result.Should().BeApproximately(input);
            }

            [Test, Repeat(RandomVariations)]
            public void ToInt16_RoundTrip_SameValue()
            {
                //arrange
                var input = Math<N>.Truncate(Random.NextNumeric<N>(short.MinValue, short.MaxValue));

                //act
                var result = Cast<N>.ToValue(Cast<N>.ToInt16(input));

                //assert
                result.Should().Be(input);
            }

            [Test, Repeat(RandomVariations)]
            public void ToInt32_RoundTrip_SameValue()
            {
                //arrange
                var input = Math<N>.Truncate(Random.NextNumeric<N>(int.MinValue, int.MaxValue));

                //act
                var result = Cast<N>.ToValue(Cast<N>.ToInt32(input));

                //assert
                result.Should().Be(input);
            }

            [Test, Repeat(RandomVariations)]
            public void ToInt64_RoundTrip_SameValue()
            {
                //arrange
                var input = Math<N>.Truncate(Random.NextNumeric<N>(long.MinValue, long.MaxValue));

                //act
                var result = Cast<N>.ToValue(Cast<N>.ToInt64(input));

                //assert
                result.Should().Be(input);
            }

            [Test, Repeat(RandomVariations)]
            public void ToSByte_RoundTrip_SameValue()
            {
                //arrange
                var input = Math<N>.Truncate(Random.NextNumeric<N>(sbyte.MinValue, sbyte.MaxValue));

                //act
                var result = Cast<N>.ToValue(Cast<N>.ToSByte(input));

                //assert
                result.Should().Be(input);
            }

            [Test, Repeat(RandomVariations)]
            public void ToSingle_SmallValueRoundTrip_SameValue()
            {
                //arrange
                var input = Random.NextNumeric<N>(-10, 10);

                //act
                var result = Cast<N>.ToValue(Cast<N>.ToSingle(input));

                //assert
                result.Should().BeApproximately(input);
            }

            [Test, Repeat(RandomVariations)]
            public void ToUInt16_RoundTrip_SameValue()
            {
                //arrange
                var input = Math<N>.Truncate(Random.NextNumeric<N>(ushort.MinValue, ushort.MaxValue));

                //act
                var result = Cast<N>.ToValue(Cast<N>.ToUInt16(input));

                //assert
                result.Should().Be(input);
            }

            [Test, Repeat(RandomVariations)]
            public void ToUInt32_RoundTrip_SameValue()
            {
                //arrange
                var input = Math<N>.Truncate(Random.NextNumeric<N>(uint.MinValue, uint.MaxValue));

                //act
                var result = Cast<N>.ToValue(Cast<N>.ToUInt32(input));

                //assert
                result.Should().Be(input);
            }

            [Test, Repeat(RandomVariations)]
            public void ToUInt64_RoundTrip_SameValue()
            {
                //arrange
                var input = Math<N>.Truncate(Random.NextNumeric<N>(ulong.MinValue, ulong.MaxValue));

                //act
                var result = Cast<N>.ToValue(Cast<N>.ToUInt64(input));

                //assert
                result.Should().Be(input);
            }
        }
    }
}