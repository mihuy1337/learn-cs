using System;
using System.Collections.Generic;
using System.Text;

namespace TextAnalysis;

static class TextGeneratorTask
{
    public static string ContinuePhrase(
        Dictionary<string, string> nextWords,
        string phraseBeginning,
        int wordsCount)
    {
        var words = new List<string>(phraseBeginning.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

        for (int i = 0; i < wordsCount; i++)
        {
            string nextWord = FindNextWord(words, nextWords);

            if (nextWord == null) break;

            words.Add(nextWord);
        }

        return BuildResultPhrase(words);
    }

    private static string FindNextWord(List<string> words, Dictionary<string, string> nextWords)
    {
        if (words.Count >= 2)
        {
            string twoWordsKey = GetLastTwoWords(words);
            if (nextWords.ContainsKey(twoWordsKey))
                return nextWords[twoWordsKey];
        }

        if (words.Count >= 1)
        {
            string oneWordKey = words[words.Count - 1];
            if (nextWords.ContainsKey(oneWordKey))
                return nextWords[oneWordKey];
        }

        return null;
    }

    private static string GetLastTwoWords(List<string> words)
    {
        var sb = new StringBuilder();
        sb.Append(words[words.Count - 2]);
        sb.Append(' ');
        sb.Append(words[words.Count - 1]);
        return sb.ToString();
    }

    private static string BuildResultPhrase(List<string> words)
    {
        var sb = new StringBuilder();

        for (int i = 0; i < words.Count; i++)
        {
            sb.Append(words[i]);
            if (i < words.Count - 1)
                sb.Append(' ');
        }

        return sb.ToString();
    }
}