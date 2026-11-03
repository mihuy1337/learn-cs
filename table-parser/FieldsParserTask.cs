using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace TableParser;

[TestFixture]
public class FieldParserTaskTests
{
    public static void Test(string input, string[] expectedResult)
    {
        var actualResult = FieldsParserTask.ParseLine(input);
        ClassicAssert.AreEqual(expectedResult.Length, actualResult.Count);
        for (int i = 0; i < expectedResult.Length; ++i)
        {
            ClassicAssert.AreEqual(expectedResult[i], actualResult[i].Value);
        }
    }

    [TestCase("text", new[] { "text" })]
    [TestCase("hello world", new[] { "hello", "world" })]
    [TestCase("", new string[0])]
    [TestCase(" ", new string[0])]
    [TestCase("  a", new[] { "a" })]
    [TestCase("a  ", new[] { "a" })]
    [TestCase("a  b", new[] { "a", "b" })]
    [TestCase("\\", new[] { "\\" })]
    [TestCase("\\\\", new[] { "\\\\" })]
    [TestCase("\"\"", new[] { "" })]
    [TestCase("''", new[] { "" })]
    [TestCase("\"a\"", new[] { "a" })]
    [TestCase("'a'", new[] { "a" })]
    [TestCase("\"a b\"", new[] { "a b" })]
    [TestCase("\"'\"", new[] { "'" })]
    [TestCase("'\"'", new[] { "\"" })]
    [TestCase("\"\\\"\"", new[] { "\"" })]
    [TestCase("'\\''", new[] { "'" })]
    [TestCase("\"\\\\\"", new[] { "\\" })]
    [TestCase("\"a\\\\\"", new[] { "a\\" })]
    [TestCase("a\"b\"", new[] { "a", "b" })]
    [TestCase("\"a\"b", new[] { "a", "b" })]
    [TestCase("\"a\"\"b\"", new[] { "a", "b" })]
    [TestCase("\"", new[] { "" })]
    [TestCase("'a", new[] { "a" })]
    [TestCase("\"a ", new[] { "a " })]
    public static void RunTests(string input, string[] expectedOutput)
    {
        Test(input, expectedOutput);
    }
}

public class FieldsParserTask
{
    public static List<Token> ParseLine(string line)
    {
        var tokens = new List<Token>();
        var currentIndex = 0;

        while (currentIndex < line.Length)
        {
            currentIndex = SkipSpaces(line, currentIndex);
            if (currentIndex >= line.Length) break;

            var token = ReadField(line, currentIndex);
            tokens.Add(token);
            currentIndex = token.GetIndexNextToToken();
        }

        return tokens;
    }

    private static int SkipSpaces(string line, int startIndex)
    {
        while (startIndex < line.Length && line[startIndex] == ' ')
            startIndex++;
        return startIndex;
    }

    private static Token ReadField(string line, int startIndex)
    {
        if (line[startIndex] == '"' || line[startIndex] == '\'')
            return ReadQuotedField(line, startIndex);

        return ReadSimpleField(line, startIndex);
    }

    private static Token ReadSimpleField(string line, int startIndex)
    {
        var endIndex = startIndex;
        while (endIndex < line.Length && line[endIndex] != ' ' &&
               line[endIndex] != '"' && line[endIndex] != '\'')
        {
            endIndex++;
        }

        var length = endIndex - startIndex;
        return new Token(line.Substring(startIndex, length), startIndex, length);
    }

    public static Token ReadQuotedField(string line, int startIndex)
    {
        return QuotedFieldTask.ReadQuotedField(line, startIndex);
    }
}