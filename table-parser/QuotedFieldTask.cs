using System.Text;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace TableParser;

[TestFixture]
public class QuotedFieldTaskTests
{
    [TestCase("text", new[] { "text" })]
    [TestCase("hello world", new[] { "hello", "world" })]

    [TestCase("", new string[0])]
    [TestCase(" ", new string[0])]
    [TestCase("     ", new string[0])]

    [TestCase("  a", new[] { "a" })]
    [TestCase("a  ", new[] { "a" })]
    [TestCase("a    b", new[] { "a", "b" })]

    [TestCase(@"\", new[] { @"\" })]
    [TestCase(@"\\", new[] { @"\\" })]
    [TestCase(@"a\b", new[] { @"a\b" })]

    [TestCase("\"\"", new[] { "" })]
    [TestCase("''", new[] { "" })]

    [TestCase("\"a b c\"", new[] { "a b c" })]
    [TestCase("'a b c'", new[] { "a b c" })]

    [TestCase(@"""'b'""", new[] { "'b'" })]
    [TestCase(@"'""b""'", new[] { "\"b\"" })]

    [TestCase(@"""\""""", new[] { "\"" })]
    [TestCase(@"'\'\''", new[] { "''" })]
    [TestCase(@"""\\""", new[] { @"\" })]
    [TestCase(@"""a \""b\""""", new[] { "a \"b\"" })]

    [TestCase("a\"b\"", new[] { "a", "b" })]
    [TestCase("\"a\"b", new[] { "a", "b" })]
    [TestCase("\"a\"\"b\"", new[] { "a", "b" })]
    [TestCase("a\"b\"c", new[] { "a", "b", "c" })]

    [TestCase("\"", new[] { "" })]
    [TestCase("'hello", new[] { "hello" })]
    [TestCase("a \"b c d", new[] { "a", "b c d" })]
    public void Test(string line, int startIndex, string expectedValue, int expectedLength)
    {
        var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
        ClassicAssert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
    }
}

public class QuotedFieldTask
{
    public static Token ReadQuotedField(string line, int startIndex)
    {
        var quoteChar = line[startIndex];
        var builder = new StringBuilder();
        var currentIndex = startIndex + 1;

        while (currentIndex < line.Length)
        {
            if (line[currentIndex] == '\\')
            {
                AppendEscapedCharacter(line, ref currentIndex, builder);
            }
            else if (line[currentIndex] == quoteChar)
            {
                currentIndex++;
                break;
            }
            else
            {
                AppendRegularCharacter(line, ref currentIndex, builder);
            }
        }

        var length = currentIndex - startIndex;
        return new Token(builder.ToString(), startIndex, length);
    }

    private static void AppendEscapedCharacter(string line, ref int currentIndex, StringBuilder builder)
    {
        currentIndex++;
        if (currentIndex < line.Length)
        {
            builder.Append(line[currentIndex]);
            currentIndex++;
        }
    }

    private static void AppendRegularCharacter(string line, ref int currentIndex, StringBuilder builder)
    {
        builder.Append(line[currentIndex]);
        currentIndex++;
    }
}