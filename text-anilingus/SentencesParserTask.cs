using System.Text;

namespace TextAnalysis;

public class SentencesParserTask
{
    private static readonly char[] SentenceSeparators = { '.', '!', '?', ';', ':', '(', ')' };

    public static List<List<string>> ParseSentences(string text)
    {
        var sentencesList = new List<List<string>>();
        var rawSentences = text.Split(SentenceSeparators, StringSplitOptions.RemoveEmptyEntries);

        foreach (var rawSentence in rawSentences)
        {
            var words = ParseWords(rawSentence);
            if (words.Count > 0)
                sentencesList.Add(words);
        }

        return sentencesList;
    }

    private static List<string> ParseWords(string sentence)
    {
        var words = new List<string>();
        var currentWord = new StringBuilder();

        foreach (var symbol in sentence)
        {
            if (IsWordChar(symbol))
                currentWord.Append(symbol);

            else if (currentWord.Length > 0)
            {
                words.Add(currentWord.ToString().ToLower());
                currentWord.Clear();
            }
        }

        if (currentWord.Length > 0)
            words.Add(currentWord.ToString().ToLower());

        return words;
    }

    private static bool IsWordChar(char c) => char.IsLetter(c) || c == '\'';
}