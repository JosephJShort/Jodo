// Copyright (c) 2022 Joseph J. Short
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

using Jodo.Numerics.Tests;

namespace Jodo.CheckedNumerics.Tests
{
    public static class UInt16CTests
    {
        public sealed class BitConvertTests : BitConvertTests<UInt16C> { }
        public sealed class CastTests : CastTests<UInt16C> { }
        public sealed class CheckedNumeric : CheckedNumericTests<UInt16C> { }
        public sealed class ConvertTests : ConvertTests<UInt16C> { }
        public sealed class MathGeneral : MathTests.General<UInt16C> { }
        public sealed class MathIntegral : MathTests.Integral<UInt16C> { }
        public sealed class MathUnsigned : NumericTests.UnsignedOnly<UInt16C> { }
        public sealed class NumericGeneral : NumericTests.General<UInt16C> { }
        public sealed class NumericIntegral : NumericTests.Integral<UInt16C> { }
        public sealed class NumericNoFloatingPoint : NumericTests.NoFloatingPoint<UInt16C> { }
        public sealed class NumericNoInfinity : NumericTests.NoInfinity<UInt16C> { }
        public sealed class NumericNoNaN : NumericTests.NoNaN<UInt16C> { }
        public sealed class NumericUnsigned : NumericTests.UnsignedOnly<UInt16C> { }
        public sealed class ObjectTests : Primitives.Tests.ObjectTests<UInt16C> { }
        public sealed class ParserGeneral : StringParserTests.General<UInt16C> { }
        public sealed class RandomTests : Primitives.Tests.RandomTests<UInt16C> { }
        public sealed class SerializableTests : Primitives.Tests.SerializableTests<UInt16C> { }
        public sealed class StringParserIntegral : StringParserTests.Integral<UInt16C> { }
    }
}
