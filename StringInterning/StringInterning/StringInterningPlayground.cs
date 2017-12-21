using FluentAssertions;
using Xunit;

namespace StringInterning
{
    public class StringInterningPlayground
    {
        private string SameStringClassLevel = "originalString";

        [Fact]
        public unsafe void StringCanBeMutable()
        {
            var originalString = "originalString";
            var replacedString = "replacedString";

            fixed (char* pointer = originalString)
            {
                for (int i = 0; i < replacedString.Length; i++)
                {
                    pointer[i] = replacedString[i];
                }
            }

            originalString.Should().Be(replacedString);
        }

        [Fact]
        public unsafe void StringInterningMutation()
        {
            var originalString = "originalString";
            var sameString = "originalString";
            var replacedString = "replacedString";

            fixed (char* pointer = originalString)
            {
                for (int i = 0; i < originalString.Length; i++)
                {
                    pointer[i] = replacedString[i];
                }
            }

            originalString.Should().Be(replacedString);
            originalString.Should().Be(sameString);
            originalString.Should().BeSameAs(sameString);
        }

        [Fact]
        public unsafe void StringInterningMutationClassLevel()
        {
            var originalString = "originalString";

            var replacedString = "replacedString";

            fixed (char* pointer = originalString)
            {
                for (int i = 0; i < originalString.Length; i++)
                {
                    pointer[i] = replacedString[i];
                }
            }

            originalString.Should().Be(replacedString);
            originalString.Should().Be(SameStringClassLevel);
        }

        [Fact]
        public unsafe void NoStringInterningWhenStringGeneratedInRuntimeByConcatanatingLocalVariables()
        {
            var originalString = "originalString";
            var @string = "String";
            var sameStringOnRuntime = "original" + @string;
            var replacedString = "replacedString";

            fixed (char* pointer = originalString)
            {
                for (int i = 0; i < originalString.Length; i++)
                {
                    pointer[i] = replacedString[i];
                }
            }

            originalString.Should().Be(replacedString);
            sameStringOnRuntime.Should().NotBe(originalString);
        }

        [Fact]
        public unsafe void StringInterningWhenStringGeneratedInRuntimeByConcatanatingInlineStrings()
        {
            var originalString = "originalString";
            var sameStringOnRuntime = "original" + "String";
            var replacedString = "replacedString";

            fixed (char* pointer = originalString)
            {
                for (int i = 0; i < originalString.Length; i++)
                {
                    pointer[i] = replacedString[i];
                }
            }

            originalString.Should().Be(replacedString);
            sameStringOnRuntime.Should().Be(originalString);
            sameStringOnRuntime.Should().BeSameAs(originalString);
        }

        [Fact]
        public unsafe void StringInterningWhenStringGeneratedInRuntimeAndForcedToBeInterned()
        {
            var originalString = "originalString";
            var @string = "String";
            var sameStringOnRuntime = "original" + @string;
            sameStringOnRuntime = string.Intern(sameStringOnRuntime);
            var replacedString = "replacedString";

            fixed (char* pointer = originalString)
            {
                for (int i = 0; i < originalString.Length; i++)
                {
                    pointer[i] = replacedString[i];
                }
            }

            originalString.Should().Be(replacedString);
            sameStringOnRuntime.Should().Be(originalString);
            sameStringOnRuntime.Should().BeSameAs(originalString);
        }

        [Fact]
        public void StringInterningWhenReturningStringLiteralFromStaticMethod()
        {
            var stringLiteral = "stringLiteral";

            var literal = GetStringLiteral();

            literal.Should().BeSameAs(stringLiteral);
        }

        [Fact]
        public void StringInterningWhenReturningStringLiteralFromInstanceMethod()
        {
            var stringLiteral = "stringLiteral";

            var literal = GetStringLiteralInstance();

            literal.Should().BeSameAs(stringLiteral);
        }

        private static string GetStringLiteral()
        {
            return "stringLiteral";
        }

        public string GetStringLiteralInstance()
        {
            return "stringLiteral";
        }
    }
}