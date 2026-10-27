namespace TextAnalysis;

static class FrequencyAnalysisTask
{
    public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
    {
        var resDo = CollectFrequencies(text);
        return SelectBestWords(resDo);
    }

    private static Dictionary<string, Dictionary<string, int>> CollectFrequencies(List<List<string>> text)
    {
        var resDo = new Dictionary<string, Dictionary<string, int>>();

        foreach (var sens in text)
        {
            var ngram = GetNgram(sens);

            foreach (var (key, val) in ngram)
            {
                AddNgram(resDo, key, val);
            }
        }

        return resDo;
    }

    private static void AddNgram(Dictionary<string, Dictionary<string, int>> resDo, string key, string val)
    {
        if (!resDo.ContainsKey(key))
            resDo.Add(key, new Dictionary<string, int> { { val, 1 } });
        else if (!resDo[key].ContainsKey(val))
            resDo[key].Add(val, 1);
        else
            resDo[key][val]++;
    }

    private static Dictionary<string, string> SelectBestWords(Dictionary<string, Dictionary<string, int>> resDo)
    {
        var result = new Dictionary<string, string>();

        foreach (var (key, dict) in resDo)
        {
            var maxWord = FindBestContinuation(dict);
            result.Add(key, maxWord);
        }

        return result;
    }

    private static string FindBestContinuation(Dictionary<string, int> dict)
    {
        var maxWord = "";
        var maxCount = 0;

        foreach (var (word, count) in dict)
        {
            if (count > maxCount)
            {
                maxCount = count;
                maxWord = word;
            }
            else if (count == maxCount && string.CompareOrdinal(word, maxWord) < 0)
            {
                maxWord = word;
            }
        }

        return maxWord;
    }

    private static List<(string Key, string Value)> GetNgram(List<string> sentence)
    {
        var res = new List<(string Key, string Value)>();

        for (int i = 0; i < sentence.Count; i++)
        {
            if (i + 1 < sentence.Count)
                res.Add((sentence[i], sentence[i + 1]));

            if (i + 2 < sentence.Count)
                res.Add(($"{sentence[i]} {sentence[i + 1]}", sentence[i + 2]));
        }

        return res;
    }
}