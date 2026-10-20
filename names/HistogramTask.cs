using System;

namespace Names;

internal static class HistogramTask
{
    public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
    {
        var daysCount = 31;
        var xLabels = new string[daysCount];
        for (var day = 1; day <= daysCount; day++)
            xLabels[day - 1] = day.ToString();

        var birthsCounts = new double[daysCount];

        foreach (var person in names)
        {
            if (person.Name != name || person.BirthDate.Day == 1)
                continue;

            birthsCounts[person.BirthDate.Day - 1]++;
        }

        return new HistogramData(
            $"Рождаемость людей с именем '{name}'",
            xLabels,
            birthsCounts
        );
    }
}