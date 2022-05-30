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
    public static class MathTests
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
            [Test]
            public void E_EquivalentToSystemMath()
            {
                Math<N>.E.Should().BeApproximately(Cast<N>.ToValue(Math.E));
            }

            [Test]
            public void PI_EquivalentToSystemMath()
            {
                Math<N>.PI.Should().BeApproximately(Cast<N>.ToValue(Math.PI));
            }

            [Test]
            public void Tau_TwoPi()
            {
                Math<N>.Tau.Should().BeApproximately(Cast<N>.ToValue(Math.PI * 2));
            }

            [Test]
            public void Abs_Zero_SameValue()
            {
                //arrange
                var input = Numeric<N>.Zero;

                //act
                var result = Math<N>.Abs(input);

                //assert
                result.Should().Be(input);
            }

            [Test, Repeat(RandomVariations)]
            public void Abs_Unsigned_SameValue()
            {
                //arrange
                OnlyApplicableTo.Unsigned();
                var input = Random.NextNumeric<N>();

                //act
                var result = Math<N>.Abs(input);

                //assert
                result.Should().Be(input);
            }

            [Test, Repeat(RandomVariations)]
            public void Abs_Positive_SameValue()
            {
                //arrange
                var input = Random.NextNumeric(Numeric<N>.Zero, Numeric<N>.MaxValue);

                //act
                var result = Math<N>.Abs(input);

                //assert
                result.Should().Be(input);
            }

            [Test, Repeat(RandomVariations)]
            public void Abs_Negative_PositiveEquivalent()
            {
                //arrange
                OnlyApplicableTo.Signed();
                var input = Random.NextNumeric(Numeric<N>.MinValue + Numeric<N>.MaxUnit, Numeric<N>.MinUnit);
                var expected = Numeric<N>.MinUnit * input;

                //act
                var result = Math<N>.Abs(input);

                //assert
                result.Should().Be(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Acos_RandomValue_EquivalentToSystemMath()
            {
                //arrange
                var randomValue = Math.Round(Random.NextDouble(Numeric<N>.IsSigned ? -1 : 0, 1), 2);
                if (!Numeric<N>.IsReal) randomValue = Math.Round(randomValue);
                var input = Cast<N>.ToValue(randomValue);
                var expected = Cast<N>.ToValue(Math.Acos(randomValue));

                //act
                var result = Math<N>.Acos(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test]
            public void Acos_LowerBound_EquivalentToSystemMath()
            {
                //arrange
                var lowerBound = Numeric<N>.IsSigned ? -1 : 0;
                var input = Cast<N>.ToValue(lowerBound);
                var expected = Cast<N>.ToValue(Math.Acos(lowerBound));

                //act
                var result = Math<N>.Acos(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test]
            public void Acos_UpperBound_EquivalentToSystemMath()
            {
                //arrange
                var upperBound = 1;
                var input = Cast<N>.ToValue(upperBound);
                var expected = Cast<N>.ToValue(Math.Acos(upperBound));

                //act
                var result = Math<N>.Acos(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Acosh_RandomValue_EquivalentToSystemMath()
            {
                //arrange
                double randomValue;
                do
                {
                    randomValue = Math.Round(Random.NextDouble(1, 10), 2);
                    if (!Numeric<N>.IsReal) randomValue = Math.Round(randomValue);
                }
                while (!double.IsFinite(Math.Acosh(randomValue)));
                var input = Cast<N>.ToValue(randomValue);
                var expected = Cast<N>.ToValue(Math.Acosh(randomValue));

                //act
                var result = Math<N>.Acosh(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test]
            public void Acosh_LowerBound_EquivalentToSystemMath()
            {
                //arrange
                var lowerBound = 1;
                var input = Cast<N>.ToValue(lowerBound);
                var expected = Cast<N>.ToValue(Math.Acosh(lowerBound));

                //act
                var result = Math<N>.Acosh(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test]
            public void Acosh_UpperBound_EquivalentToSystemMath()
            {
                //arrange
                var upperBound = ToDoubleSafe(Numeric<N>.MaxValue);
                var input = Cast<N>.ToValue(upperBound);
                var expected = Cast<N>.ToValue(Math.Acosh(upperBound));

                //act
                var result = Math<N>.Acosh(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Asin_RandomValue_EquivalentToSystemMath()
            {
                //arrange
                var randomValue = Math.Round(Random.NextDouble(Numeric<N>.IsSigned ? -1 : 0, 1), 2);
                if (!Numeric<N>.IsReal) randomValue = Math.Round(randomValue);
                var input = Cast<N>.ToValue(randomValue);
                var expected = Cast<N>.ToValue(Math.Asin(randomValue));

                //act
                var result = Math<N>.Asin(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test]
            public void Asin_LowerBound_EquivalentToSystemMath()
            {
                //arrange
                var lowerBound = Numeric<N>.IsSigned ? -1 : 0;
                var input = Cast<N>.ToValue(lowerBound);
                var expected = Cast<N>.ToValue(Math.Asin(lowerBound));

                //act
                var result = Math<N>.Asin(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test]
            public void Asin_UpperBound_EquivalentToSystemMath()
            {
                //arrange
                var upperBound = 1;
                var input = Cast<N>.ToValue(upperBound);
                var expected = Cast<N>.ToValue(Math.Asin(upperBound));

                //act
                var result = Math<N>.Asin(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Asinh_RandomValue_EquivalentToSystemMath()
            {
                //arrange
                var randomValue = Math.Round(Random.NextDouble(), 2);
                var input = Cast<N>.ToValue(randomValue);
                var expected = Cast<N>.ToValue(Math.Asinh(randomValue));

                //act
                var result = Math<N>.Asinh(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test]
            public void Asinh_LowerBound_EquivalentToSystemMath()
            {
                //arrange
                var lowerBound = ToDoubleSafe(Numeric<N>.MinValue);
                var input = Cast<N>.ToValue(lowerBound);
                var expected = Cast<N>.ToValue(Math.Asinh(lowerBound));

                //act
                var result = Math<N>.Asinh(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test]
            public void Asinh_UpperBound_EquivalentToSystemMath()
            {
                //arrange
                var upperBound = ToDoubleSafe(Numeric<N>.MaxValue);
                var input = Cast<N>.ToValue(upperBound);
                var expected = Cast<N>.ToValue(Math.Asinh(upperBound));

                //act
                var result = Math<N>.Asinh(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Atan_RandomValue_EquivalentToSystemMath()
            {
                //arrange
                var randomValue = ToDoubleSafe(Random.NextNumeric<N>());
                var input = Cast<N>.ToValue(randomValue);
                var expected = Cast<N>.ToValue(Math.Atan(randomValue));

                //act
                var result = Math<N>.Atan(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test]
            public void Atan_LowerBound_EquivalentToSystemMath()
            {
                //arrange
                var lowerBound = ToDoubleSafe(Numeric<N>.MinValue);
                var input = Cast<N>.ToValue(lowerBound);
                var expected = Cast<N>.ToValue(Math.Atan(lowerBound));

                //act
                var result = Math<N>.Atan(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test]
            public void Atan_UpperBound_EquivalentToSystemMath()
            {
                //arrange
                var upperBound = ToDoubleSafe(Numeric<N>.MaxValue);
                var input = Cast<N>.ToValue(upperBound);
                var expected = Cast<N>.ToValue(Math.Atan(upperBound));

                //act
                var result = Math<N>.Atan(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Atan2_RandomValues_EquivalentToSystemMath()
            {
                //arrange
                var randomValue1 = ToDoubleSafe(Random.NextNumeric<N>());
                var randomValue2 = ToDoubleSafe(Random.NextNumeric<N>());
                var input1 = Cast<N>.ToValue(randomValue1);
                var input2 = Cast<N>.ToValue(randomValue2);
                var expected = Cast<N>.ToValue(Math.Atan2(randomValue1, randomValue2));

                //act
                var result = Math<N>.Atan2(input1, input2);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Atan2_RandomBoundaries_EquivalentToSystemMath()
            {
                //arrange
                var randomBoundary1 = ToDoubleSafe(Random.NextBoolean() ? Numeric<N>.MinValue : Numeric<N>.MaxValue);
                var randomBoundary2 = ToDoubleSafe(Random.NextBoolean() ? Numeric<N>.MinValue : Numeric<N>.MaxValue);
                var input1 = Cast<N>.ToValue(randomBoundary1);
                var input2 = Cast<N>.ToValue(randomBoundary2);
                var expected = Cast<N>.ToValue(Math.Atan2(randomBoundary1, randomBoundary2));

                //act
                var result = Math<N>.Atan2(input1, input2);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Atanh_RandomValue_EquivalentToSystemMath()
            {
                //arrange
                double randomValue;
                do
                {
                    randomValue = Math.Round(Random.NextDouble(Numeric<N>.IsSigned ? -1 : 0, 1), 2);
                    if (!Numeric<N>.IsReal) randomValue = Math.Round(randomValue);
                }
                while (!double.IsFinite(Math.Atanh(randomValue)));
                var input = Cast<N>.ToValue(randomValue);
                var expected = Cast<N>.ToValue(Math.Atanh(randomValue));

                //act
                var result = Math<N>.Atanh(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Cbrt_RandomValue_EquivalentToSystemMath()
            {
                //arrange
                var randomValue = ToDoubleSafe(Random.NextNumeric<N>());
                var input = Cast<N>.ToValue(randomValue);
                var expected = Cast<N>.ToValue(Math.Cbrt(randomValue));

                //act
                var result = Math<N>.Cbrt(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test]
            public void Cbrt_Zero_EquivalentToSystemMath()
            {
                //arrange
                var input = Cast<N>.ToValue(0);
                var expected = Cast<N>.ToValue(Math.Cbrt(0d));

                //act
                var result = Math<N>.Cbrt(input);

                //assert
                result.Should().Be(expected);
            }

            [Test]
            public void Cbrt_LowerBound_EquivalentToSystemMath()
            {
                //arrange
                var lowerBound = ToDoubleSafe(Numeric<N>.MinValue);
                var input = Cast<N>.ToValue(lowerBound);
                var expected = Cast<N>.ToValue(Math.Cbrt(lowerBound));

                //act
                var result = Math<N>.Cbrt(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test]
            public void Cbrt_UpperBound_EquivalentToSystemMath()
            {
                //arrange
                var upperBound = ToDoubleSafe(Numeric<N>.MaxValue);
                var input = Cast<N>.ToValue(upperBound);
                var expected = Cast<N>.ToValue(Math.Cbrt(upperBound));

                //act
                var result = Math<N>.Cbrt(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Ceiling_RandomValue_EquivalentToSystemMath()
            {
                //arrange
                var randomValue = ToDoubleSafe(Random.NextNumeric(
                    Numeric<N>.MinValue + Numeric<N>.One, Numeric<N>.MaxValue - Numeric<N>.One));
                var input = Cast<N>.ToValue(randomValue);
                var expected = Cast<N>.ToValue(Math.Ceiling(randomValue));

                //act
                var result = Math<N>.Ceiling(input);

                //assert
                result.Should().Be(expected);
            }

            [Test]
            public void Ceiling_LowerBound_EquivalentToSystemMath()
            {
                //arrange
                var lowerBound = ToDoubleSafe(Numeric<N>.MinValue + Numeric<N>.One);
                var input = Cast<N>.ToValue(lowerBound);
                var expected = Cast<N>.ToValue(Math.Ceiling(lowerBound));

                //act
                var result = Math<N>.Ceiling(input);

                //assert
                result.Should().Be(expected);
            }

            [Test]
            public void Ceiling_UpperBound_EquivalentToSystemMath()
            {
                //arrange
                var upperBound = ToDoubleSafe(Numeric<N>.MaxValue - Numeric<N>.One);
                var input = Cast<N>.ToValue(upperBound);
                var expected = Cast<N>.ToValue(Math.Ceiling(upperBound));

                //act
                var result = Math<N>.Ceiling(input);

                //assert
                result.Should().Be(expected);
            }

            [Test]
            public void Ceiling_Zero_EquivalentToSystemMath()
            {
                //arrange
                var input = Cast<N>.ToValue(0);
                var expected = Cast<N>.ToValue(Math.Ceiling(0d));

                //act
                var result = Math<N>.Ceiling(input);

                //assert
                result.Should().Be(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Cos_RandomValue_EquivalentToSystemMath()
            {
                //arrange
                var randomValue = ToDoubleSafe(Random.NextNumeric<N>());
                var input = Cast<N>.ToValue(randomValue);
                var expected = Cast<N>.ToValue(Math.Cos(randomValue));

                //act
                var result = Math<N>.Cos(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test]
            public void Cos_LowerBound_EquivalentToSystemMath()
            {
                //arrange
                var lowerBound = ToDoubleSafe(Numeric<N>.MinValue);
                var input = Cast<N>.ToValue(lowerBound);
                var expected = Cast<N>.ToValue(Math.Cos(lowerBound));

                //act
                var result = Math<N>.Cos(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test]
            public void Cos_UpperBound_EquivalentToSystemMath()
            {
                //arrange
                var upperBound = ToDoubleSafe(Numeric<N>.MaxValue);
                var input = Cast<N>.ToValue(upperBound);
                var expected = Cast<N>.ToValue(Math.Cos(upperBound));

                //act
                var result = Math<N>.Cos(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Cosh_RandomValue_EquivalentToSystemMath()
            {
                //arrange
                double randomValue;
                do
                {
                    randomValue = Math.Round(Random.NextDouble(Numeric<N>.IsSigned ? -1 : 0, 1), 2);
                    if (!Numeric<N>.IsReal) randomValue = Math.Round(randomValue);
                }
                while (!double.IsFinite(Math.Cosh(randomValue)));
                var input = Cast<N>.ToValue(randomValue);
                var expected = Cast<N>.ToValue(Math.Cosh(randomValue));

                //act
                var result = Math<N>.Cosh(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Exp_Random_EquivalentToSystemMath()
            {
                //arrange
                var randomValue = Math.Round(Random.NextDouble(Numeric<N>.IsSigned ? -1 : 0, 4), 2);
                if (!Numeric<N>.IsReal) randomValue = Math.Round(randomValue);
                var input = Cast<N>.ToValue(randomValue);
                var expected = Cast<N>.ToValue(Math.Exp(randomValue));

                //act
                var result = Math<N>.Exp(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test]
            public void Exp_Zero_EquivalentToSystemMath()
            {
                //arrange
                var input = Cast<N>.ToValue(0);
                var expected = Cast<N>.ToValue(Math.Exp(0d));

                //act
                var result = Math<N>.Exp(input);

                //assert
                result.Should().Be(expected);
            }

            [Test]
            public void Exp_One_EquivalentToSystemMath()
            {
                //arrange
                var input = Cast<N>.ToValue(1);
                var expected = Cast<N>.ToValue(Math.Exp(1d));

                //act
                var result = Math<N>.Exp(input);

                //assert
                result.Should().Be(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Floor_RandomValue_EquivalentToSystemMath()
            {
                //arrange
                var randomValue = ToDoubleSafe(Random.NextNumeric(
                    Numeric<N>.MinValue + Numeric<N>.One, Numeric<N>.MaxValue - Numeric<N>.One));
                var input = Cast<N>.ToValue(randomValue);
                var expected = Cast<N>.ToValue(Math.Floor(randomValue));

                //act
                var result = Math<N>.Floor(input);

                //assert
                result.Should().Be(expected);
            }

            [Test]
            public void Floor_LowerBound_EquivalentToSystemMath()
            {
                //arrange
                var lowerBound = ToDoubleSafe(Numeric<N>.MinValue + Numeric<N>.One);
                var input = Cast<N>.ToValue(lowerBound);
                var expected = Cast<N>.ToValue(Math.Floor(lowerBound));

                //act
                var result = Math<N>.Floor(input);

                //assert
                result.Should().Be(expected);
            }

            [Test]
            public void Floor_UpperBound_EquivalentToSystemMath()
            {
                //arrange
                var upperBound = ToDoubleSafe(Numeric<N>.MaxValue - Numeric<N>.One);
                var input = Cast<N>.ToValue(upperBound);
                var expected = Cast<N>.ToValue(Math.Floor(upperBound));

                //act
                var result = Math<N>.Floor(input);

                //assert
                result.Should().Be(expected);
            }

            [Test]
            public void Floor_Zero_EquivalentToSystemMath()
            {
                //arrange
                var input = Cast<N>.ToValue(0);
                var expected = Cast<N>.ToValue(Math.Floor(0d));

                //act
                var result = Math<N>.Floor(input);

                //assert
                result.Should().Be(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void IEEERemainder_RandomValue_EquivalentToSystemMath()
            {
                //arrange
                double randomValue1;
                double randomValue2;
                do
                {
                    randomValue1 = Math.Round(Random.NextDouble(0, 10), 2);
                    randomValue2 = Math.Round(Random.NextDouble(1, 10), 2);
                    if (!Numeric<N>.IsReal)
                    {
                        randomValue1 = Math.Round(randomValue1);
                        randomValue2 = Math.Round(randomValue2);
                    }
                } while (!Numeric<N>.IsSigned && Math.IEEERemainder(randomValue1, randomValue2) < 0);
                var input1 = Cast<N>.ToValue(randomValue1);
                var input2 = Cast<N>.ToValue(randomValue2);
                var expected = Cast<N>.ToValue(Math.IEEERemainder(randomValue1, randomValue2));

                //act
                var result = Math<N>.IEEERemainder(input1, input2);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Log_RandomValue_EquivalentToSystemMath()
            {
                //arrange
                var randomValue = ToDoubleSafe(Random.NextNumeric(Numeric<N>.One, Numeric<N>.MaxValue));
                var input = Cast<N>.ToValue(randomValue);
                var expected = Cast<N>.ToValue(Math.Log(randomValue));

                //act
                var result = Math<N>.Log(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Log_LowerBound_EquivalentToSystemMath()
            {
                //arrange
                var randomValue = Cast<N>.ToDouble(Numeric<N>.Epsilon);
                var input = Cast<N>.ToValue(randomValue);
                var expected = Cast<N>.ToValue(Math.Log(randomValue));

                //act
                var result = Math<N>.Log(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Log_UpperBound_EquivalentToSystemMath()
            {
                //arrange
                var randomValue = ToDoubleSafe(Numeric<N>.MaxValue);
                var input = Cast<N>.ToValue(randomValue);
                var expected = Cast<N>.ToValue(Math.Log(randomValue));

                //act
                var result = Math<N>.Log(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void LogN_RandomValues_EquivalentToSystemMath()
            {
                //arrange
                double randomValue1;
                double randomValue2;
                do
                {
                    randomValue1 = ToDoubleSafe(Random.NextNumeric(Numeric<N>.Zero, Numeric<N>.MaxValue));
                    randomValue2 = ToDoubleSafe(Random.NextNumeric(Numeric<N>.One, Numeric<N>.MaxValue));
                } while (randomValue1 <= 0 || randomValue2 <= 1);
                var input1 = Cast<N>.ToValue(randomValue1);
                var input2 = Cast<N>.ToValue(randomValue2);
                var expected = Cast<N>.ToValue(Math.Log(randomValue1, randomValue2));

                //act
                var result = Math<N>.Log(input1, input2);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Log10_RandomValue_EquivalentToSystemMath()
            {
                //arrange
                var randomValue = ToDoubleSafe(Random.NextNumeric(Numeric<N>.One, Numeric<N>.MaxValue));
                var input = Cast<N>.ToValue(randomValue);
                var expected = Cast<N>.ToValue(Math.Log10(randomValue));

                //act
                var result = Math<N>.Log10(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Log10_LowerBound_EquivalentToSystemMath()
            {
                //arrange
                var randomValue = Cast<N>.ToDouble(Numeric<N>.Epsilon);
                var input = Cast<N>.ToValue(randomValue);
                var expected = Cast<N>.ToValue(Math.Log10(randomValue));

                //act
                var result = Math<N>.Log10(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Log10_UpperBound_EquivalentToSystemMath()
            {
                //arrange
                var randomValue = ToDoubleSafe(Numeric<N>.MaxValue);
                var input = Cast<N>.ToValue(randomValue);
                var expected = Cast<N>.ToValue(Math.Log10(randomValue));

                //act
                var result = Math<N>.Log10(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Max_RandomValues_LargestValue()
            {
                //arrange
                var input1 = Random.NextNumeric<N>();
                var input2 = Random.NextNumeric<N>();
                var expected = input1 > input2 ? input1 : input2;

                //act
                var result = Math<N>.Max(input1, input2);

                //assert
                result.Should().Be(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Min_RandomValues_SmallestValue()
            {
                //arrange
                var input1 = Random.NextNumeric<N>();
                var input2 = Random.NextNumeric<N>();
                var expected = input1 < input2 ? input1 : input2;

                //act
                var result = Math<N>.Min(input1, input2);

                //assert
                result.Should().Be(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Pow_RandomValues_EquivalentToSystemMath()
            {
                //arrange
                double randomValue1;
                double randomValue2;
                do
                {
                    randomValue1 = ToDoubleSafe(Random.NextNumeric(Numeric<N>.Zero, Cast<N>.ToValue(3)));
                    randomValue2 = ToDoubleSafe(Random.NextNumeric(Numeric<N>.Zero, Cast<N>.ToValue(3)));
                } while (randomValue1 < .9);
                var input1 = Cast<N>.ToValue(randomValue1);
                var input2 = Cast<N>.ToValue(randomValue2);
                var expected = Cast<N>.ToValue(Math.Pow(randomValue1, randomValue2));

                //act
                var result = Math<N>.Pow(input1, input2);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Pow_RandomValueByZero_One()
            {
                //arrange
                var input = Random.NextNumeric<N>();

                //act
                var result = Math<N>.Pow(input, Numeric<N>.Zero);

                //assert
                result.Should().Be(Numeric<N>.One);
            }

            [Test, Repeat(RandomVariations)]
            public void Pow_RandomValueByOne_SameValue()
            {
                //arrange
                var input = Random.NextNumeric<N>();

                //act
                var result = Math<N>.Pow(input, Numeric<N>.One);

                //assert
                result.Should().Be(input);
            }

            [Test, Repeat(RandomVariations)]
            public void Round1_RandomIntegral_SameValue()
            {
                //arrange
                OnlyApplicableTo.Integral();
                var input = Random.NextNumeric<N>();

                //act
                var result = Math<N>.Round(input);

                //assert
                result.Should().Be(input);
            }

            [Test, Repeat(RandomVariations)]
            public void Round1_RandomFloatingPoint_EquivalentToSystemMath()
            {
                //arrange
                OnlyApplicableTo.FloatingPoint();
                var randomValue = Random.NextDouble(Numeric<N>.IsSigned ? -10 : 0, 10);
                var input = Cast<N>.ToValue(randomValue);
                var expected = Cast<N>.ToValue(Math.Round(randomValue));

                //act
                var result = Math<N>.Round(input);

                //assert
                result.Should().Be(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Round1_RandomReal_EquivalentToSystemMath()
            {
                //arrange
                OnlyApplicableTo.Real();
                var randomValue = Math.Round(Random.NextDouble(Numeric<N>.IsSigned ? -10 : 0, 10), 1);
                var input = Cast<N>.ToValue(randomValue);
                var expected = Cast<N>.ToValue(Math.Round(randomValue));

                //act
                var result = Math<N>.Round(input);

                //assert
                result.Should().Be(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Round2_RandomIntegral_SameValue()
            {
                //arrange
                OnlyApplicableTo.Integral();
                var input = Random.NextNumeric<N>();
                var digits = Random.NextInt32(0, 10);

                //act
                var result = Math<N>.Round(input, digits);

                //assert
                result.Should().Be(input);
            }

            [Test, Repeat(RandomVariations)]
            public void Round2_RandomFloatingPoint_EquivalentToSystemMath()
            {
                //arrange
                OnlyApplicableTo.FloatingPoint();
                var randomValue = Random.NextDouble(Numeric<N>.IsSigned ? -10 : 0, 10);
                var digits = Random.NextInt32(0, 3);
                var input = Cast<N>.ToValue(randomValue);
                var expected = Cast<N>.ToValue(Math.Round(randomValue, digits));

                //act
                var result = Math<N>.Round(input, digits);

                //assert
                result.Should().Be(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Round3_RandomIntegral_SameValue()
            {
                //arrange
                OnlyApplicableTo.Integral();
                var input = Random.NextNumeric<N>();
                var mode = Random.NextEnum<MidpointRounding>();

                //act
                var result = Math<N>.Round(input, mode);

                //assert
                result.Should().Be(input);
            }

            [Test, Repeat(RandomVariations)]
            public void Round3_RandomFloatingPoint_SameValue()
            {
                //arrange
                OnlyApplicableTo.FloatingPoint();
                var randomValue = Random.NextDouble(Numeric<N>.IsSigned ? -10 : 0, 10);
                var mode = Random.NextEnum<MidpointRounding>();
                var input = Cast<N>.ToValue(randomValue);
                var expected = Cast<N>.ToValue(Math.Round(randomValue, mode));

                //act
                var result = Math<N>.Round(input, mode);

                //assert
                result.Should().Be(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Round4_RandomIntegral_SameValue()
            {
                //arrange
                OnlyApplicableTo.Integral();
                var input = Random.NextNumeric<N>();
                var digits = Random.NextInt32(0, 10);
                var mode = Random.NextEnum<MidpointRounding>();

                //act
                var result = Math<N>.Round(input, digits, mode);

                //assert
                result.Should().Be(input);
            }

            [Test, Repeat(RandomVariations)]
            public void Round4_RandomFloatingPoint_EquivalentToSystemMath()
            {
                //arrange
                OnlyApplicableTo.FloatingPoint();
                var randomValue = Random.NextDouble(Numeric<N>.IsSigned ? -10 : 0, 10);
                var digits = Random.NextInt32(0, 3);
                var mode = Random.NextEnum<MidpointRounding>();
                var input = Cast<N>.ToValue(randomValue);
                var expected = Cast<N>.ToValue(Math.Round(randomValue, digits, mode));

                //act
                var result = Math<N>.Round(input, digits, mode);

                //assert
                result.Should().Be(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Sin_RandomSigned_EquivalentToSystemMath()
            {
                //arrange
                double randomValue;
                do { randomValue = ToDoubleSafe(Random.NextNumeric<N>()); }
                while (!Numeric<N>.IsSigned && Math.Sin(randomValue) < 0);
                var input = Cast<N>.ToValue(randomValue);
                var expected = Cast<N>.ToValue(Math.Sin(randomValue));

                //act
                var result = Math<N>.Sin(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test]
            public void Sin_LowerBound_EquivalentToSystemMath()
            {
                //arrange
                var lowerBound = ToDoubleSafe(Numeric<N>.MinValue);
                var input = Cast<N>.ToValue(lowerBound);
                var expected = Cast<N>.ToValue(Math.Sin(lowerBound));

                //act
                var result = Math<N>.Sin(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test]
            public void Sin_UpperBound_EquivalentToSystemMath()
            {
                //arrange
                var upperBound = ToDoubleSafe(Numeric<N>.MaxValue);
                var input = Cast<N>.ToValue(upperBound);
                var expected = Cast<N>.ToValue(Math.Sin(upperBound));

                //act
                var result = Math<N>.Sin(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Sinh_Random_EquivalentToSystemMath()
            {
                //arrange
                N input;
                do { input = Random.NextNumeric<N>(-1, 1); }
                while (!double.IsFinite(Math.Sinh(input.ToDouble())));

                //act
                var result = Math<N>.Sinh(input);

                //assert
                result.Should().BeApproximately(Math.Sinh(input.ToDouble()));
            }

            [Test, Repeat(RandomVariations)]
            public void Sqrt_RandomValue_EquivalentToSystemMath()
            {
                //arrange
                var randomValue = ToDoubleSafe(Random.NextNumeric<N>(Numeric<N>.Zero, Numeric<N>.MaxValue));
                var input = Cast<N>.ToValue(randomValue);
                var expected = Cast<N>.ToValue(Math.Sqrt(randomValue));

                //act
                var result = Math<N>.Sqrt(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test]
            public void Sqrt_LowerBound_EquivalentToSystemMath()
            {
                //arrange
                var lowerBound = 0;
                var input = Cast<N>.ToValue(lowerBound);
                var expected = Cast<N>.ToValue(Math.Sqrt(lowerBound));

                //act
                var result = Math<N>.Sqrt(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test]
            public void Sqrt_UpperBound_EquivalentToSystemMath()
            {
                //arrange
                var upperBound = ToDoubleSafe(Numeric<N>.MaxValue);
                var input = Cast<N>.ToValue(upperBound);
                var expected = Cast<N>.ToValue(Math.Sqrt(upperBound));

                //act
                var result = Math<N>.Sqrt(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Tan_RandomValue_EquivalentToSystemMath()
            {
                //arrange
                var randomValue = Math.Round(Random.NextDouble(Numeric<N>.IsSigned ? -10 : 0, 10), 2);
                if (!Numeric<N>.IsReal) randomValue = Math.Round(randomValue);
                var input = Cast<N>.ToValue(randomValue);
                var expected = Cast<N>.ToValue(Math.Tan(randomValue));

                //act
                var result = Math<N>.Tan(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test]
            public void Tan_LowerBound_EquivalentToSystemMath()
            {
                //arrange
                var lowerBound = Numeric<N>.IsSigned ? -10 : 0;
                var input = Cast<N>.ToValue(lowerBound);
                var expected = Cast<N>.ToValue(Math.Tan(lowerBound));

                //act
                var result = Math<N>.Tan(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test]
            public void Tan_UpperBound_EquivalentToSystemMath()
            {
                //arrange
                var upperBound = 10;
                var input = Cast<N>.ToValue(upperBound);
                var expected = Cast<N>.ToValue(Math.Tan(upperBound));

                //act
                var result = Math<N>.Tan(input);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Tanh_Random_EquivalentToSystemMath()
            {
                //arrange
                double input;
                do
                {
                    input = Math.Round(Random.NextDouble(Numeric<N>.IsSigned ? -1 : 0, 1), 2);
                }
                while (!double.IsFinite(Math.Tanh(input)));
                var sut = Cast<N>.ToValue(input);
                var expected = Cast<N>.ToValue(Math.Tanh(input));

                //act
                var result = Math<N>.Tanh(sut);

                //assert
                result.Should().BeApproximately(expected);
            }

            [Test, Repeat(RandomVariations)]
            public void Truncate_RandomValue_EquivalentToSystemMath()
            {
                //arrange
                var randomValue = ToDoubleSafe(Random.NextNumeric<N>());
                var input = Cast<N>.ToValue(randomValue);
                var expected = Cast<N>.ToValue(Math.Truncate(randomValue));

                //act
                var result = Math<N>.Truncate(input);

                //assert
                result.Should().Be(expected);
            }
        }
    }
}
