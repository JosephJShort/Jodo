# Jodo.Extensions <img src="PackageIcon.png" alt="Logo" height="128"/>

[![Test](https://github.com/JosephJShort/Jodo.Extensions/actions/workflows/tests.yml/badge.svg)](https://github.com/JosephJShort/Jodo.Extensions/actions/workflows/tests.yml) [![CodeQL](https://github.com/JosephJShort/Jodo.Extensions/actions/workflows/codeql-analysis.yml/badge.svg)](https://github.com/JosephJShort/Jodo.Extensions/actions/workflows/codeql-analysis.yml)

Useful C# libraries written in the style of the .NET SDK.

## Jodo.Extensions.CheckedNumerics

> Note: This package contains work in progress.

Provides numeric value types and utilities that have built-in protection from overflow. Useful for preventing unexpected negative/positive, infinite or `NaN` values from entering a system.

### Numeric value types

> Note: These types increases CPU and memory usage compared to using built-in numeric types. See the [Performance considerations](#performance-considerations) section for more details.

The following table summarizes the types available and their behaviour:

| Type | Corresponding CLR type | Difference in behaviour |
| - | - | - |
| `cint`<br />`ucint` | `int`<br />`uint` | <ul><li>Operations that would overflow instead return `MinValue` or `MaxValue` depending on the direction of the overflow.</li><li>Division by zero does NOT throw a [DivideByZeroException](https://docs.microsoft.com/en-us/dotnet/api/system.dividebyzeroexception) but returns `MaxValue`.</li></ul> |
| `cfloat`<br />`cdouble` | `float`<br />`double` | <ul><li>Operations that would overflow do NOT return `NegativeInfinity` or `PositiveInfinity` but return `MinValue` or `MaxValue` respectively.</li><li>Division by zero does NOT return `NegativeInfinity`, `PositiveInfinity` or `NaN` but returns `MaxValue`.</li><li>It is not possible for values to be `NegativeInfinity`, `PositiveInfinity` or `NaN`.</li></ul> |
| `fix64`<br />`ufix64` | _N/A_ | <ul><li>A fixed-precision number with a 40 bit integral part and a 24 bit mantissa.</li><li>Useful in systems where high precision is required regardless of magnitude.</li></ul> |

Usage is the same as with built-in numeric types but yields different results as demonstrated by the following code example:
```csharp
var x1 = cint.MaxValue + 1;
Console.WriteLine(x1);  // output: 2147483647

var x2 = (cfloat)4 / 0;
Console.WriteLine(x2);  // output: 3.402823E+38
```

### Other types

| Type | Description |
| - | - |
| `INumeric<T>` | <ul><li>Implemented by all the [numeric value types](#numeric-value-types).</li><li>Allows for the creation of generic numerical systems.</li><li>Can be implmented to create further user-defined numeric value types.</li></ul> |
| `Math<T>` | <ul><li>A static class that provides equivalent methods to [Math](https://docs.microsoft.com/en-us/dotnet/api/system.math), e.g. `Log(T)`, `Pow(T)`, `Sqrt(T)`, etc.</li><li>Available for all types that implement `INumeric<T>` including user-defined types.</li></ul> |
| `BitConverter<T>` | <ul><li>A static class similar to [BitConverter](https://docs.microsoft.com/en-us/dotnet/api/system.bitconverter) that provides conversion to and from `ReadOnlySpan<byte>`.</li><li>Available for all types that implement `INumeric<T>` including user-defined types.</li></ul> |
| `StringFormatter<T>` | <ul><li>A static class that provides string parsing and formatting methods.</li><li>Available for all types that implement `INumeric<T>` including user-defined types.</li></ul> |
| `CheckedMath` | <ul><li>A static class that provides checked arithmetic methods for the built-in numeric types.</li></ul> |

### Other features

| Feature | Notes |
| - | - |
| Overloaded operators | <ul><li>`==`, `!=`, `>`, `>=`, `<`, `<=`, `+`, `++`, `-`, `*`, `/` and `%` are overloaded, allowing for use in expressions.</li><li>`&`, `\|`, `^`, `~`, `<<` and `>>` are overloaded for types based on integral primitives.</li><li>Implicit conversions from built-in numeric types are provided, allowing use in expressions with numeric literals.</li><li>The `INumeric<T>` interface defines overloads the `>`, `>=`, `<`, `<=`, `+`, `++`, `-`, `*`, `/` and `%` operators, allowing for limited expressions in a generic context (note: equality and conversion operators are not supported on interfaces).</li></ul> |
| Float conversion | <ul><li>A method on `INumeric\<T\>`, `float Approximate(float offset)`, provides easy conversion to floats.</li><li>For use in graphical applications.</li></ul> |
| Formattable | <ul><li>All the [numeric value types](#numeric-value-types) implement [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable) and can be used with [standard numeric format strings](https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings).</li></ul> |
| Random generation | <ul><li>An extension method for [Random](https://docs.microsoft.com/en-us/dotnet/api/system.random), `NextNumeric\<T\>`, provides randomly generated values.</li><li>Available for all types that implement `INumeric<T>` including user-defined types.</li><li>Values can be generated between two bounds or without bounds.</li></ul> |
| Serialization | <ul><li>All the [numeric value types](#numeric-value-types) implement [ISerializable](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.iserializable), have the [Serializable](https://docs.microsoft.com/en-us/dotnet/api/system.serializableattribute) and a deserialization constructor.</li></ul> |
| _Miscellaneous_ | <ul><li>All numeric value types implement [IEquatable\<T\>](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1), [IComparable\<T\>](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable-1) and [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), override `EqualTo(object)`, `GetHashCode()` and `ToString()` and have a [DebuggerDisplay](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.debuggerdisplayattribute) attribute.</li></ul>

### Performance considerations

The [numeric value types](#numeric-value-types) are [readonly structs](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/struct#readonly-struct) that wrap built-in value types, and use the [checked](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/checked) keyword when performing arithmetic. This means that they have higher CPU and memory usage compared to built-in numeric types.

If developing a performance-sensitive application, consider using a profiler to assess the impact. As a rule of thumb the impact is likely to be acceptable in logical applications, but not in graphical or big-data applications.

## Jodo.Extensions.CheckedGeometry

> Note: This package contains work in progress.

Provides geometric value types that make use of generic maths from CheckedNumerics.

| CheckedGeometry type | Notes |
| - | - |
| AARectangle\<T\> | Axis aligned rectangle |
| Rectangle\<T\> |  |
| Circle\<T\> |  |
| Angle\<T\> |  |
| Unit\<T\> |  |
| Vector2\<T\> |  |
| Vector3\<T\> |  |

## Jodo.Extensions.Collections

> Note: This package contains work in progress.
