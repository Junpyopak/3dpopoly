using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class CSVReader
{
    public static List<Dictionary<string, string>> ReadCSV(TextAsset csvFile)
    {
        List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();

        StringReader reader = new StringReader(csvFile.text);

        // 첫 줄은 헤더
        string headerLine = reader.ReadLine();
        string[] headers = headerLine.Split(',');

        string line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] values = SplitCSVLine(line);
            Dictionary<string, string> entry = new Dictionary<string, string>();

            for (int i = 0; i < headers.Length; i++)
            {
                entry[headers[i]] = values[i];
            }

            data.Add(entry);
        }

        return data;
    }

    // 텍스트 안에 쉼표(,)가 있는 경우 대응
    private static string[] SplitCSVLine(string line)
    {
        List<string> result = new List<string>();
        bool inQuotes = false;
        string value = "";

        foreach (char c in line)
        {
            if (c == '"')
            {
                inQuotes = !inQuotes;
            }
            else if (c == ',' && !inQuotes)
            {
                result.Add(value);
                value = "";
            }
            else
            {
                value += c;
            }
        }
        result.Add(value);

        return result.ToArray();
    }
}
