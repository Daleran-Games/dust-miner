using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DaleranGames.IO
{
    public class CSVData
    {
        public readonly string Name;

        List<string[]> rawData;
        public List<string[]> RawData { get { return rawData; } }

        Dictionary<string, CSVEntry> data;
        public CSVEntry this[string id]
        {
            get
            {
                CSVEntry output;

                if (data.TryGetValue(id , out output))
                    return output;
                else
                    throw new KeyNotFoundException(id + " not a valid if for this CSV Data set.");
            }
        }
        public int Count { get { return data.Count; } }
        public List<CSVEntry> Entries { get { return new List<CSVEntry>(data.Values); } }

        public CSVData (string name, string filepath)
        {
            Name = name;
            rawData = CSVUtility.ParseCSVToArray(File.ReadAllText(filepath));
            data = ParseOneHeader(rawData);
        }

        public static List<CSVData> ParseMultipleCSVSheet(List<string> names, string filepath)
        {
            List<CSVData> dataset = new List<CSVData>();
            List<string[]> csvArray = CSVUtility.ParseCSVToArray(File.ReadAllText(filepath));
            List<List<string[]>> datasets = new List<List<string[]>>();
            bool headerNext = false;
            int datasetIndex = -1;

            //Split the raw string array into sets of strings
            for (int i = 0; i < csvArray.Count; i++)
            {
                if (names.Contains(csvArray[i][0]))
                {
                    headerNext = true;
                    datasetIndex++;
                }
                else if (headerNext == true)
                {
                    datasets.Add(new List<string[]>());
                    datasets[datasetIndex].Add(csvArray[i]);
                    headerNext = false;
                }
                else
                {
                    datasets[datasetIndex].Add(csvArray[i]);
                }
            }

            //GO through each CSV set from the previos loop and convert it into CSV Data
            for (int i = 0; i < datasets.Count; i++)
            {
                dataset.Add(new CSVData(names[i], datasets[i]));
            }

            return dataset;
        }

        public CSVData(string name, List<string[]> raw)
        {
            Name = name;
            rawData = raw;
            data = ParseOneHeader(raw);
        }
        
        static Dictionary<string, CSVEntry> ParseOneHeader (List<string[]> csvArray)
        {
            string[] header = csvArray[0];
            Dictionary<string, CSVEntry> newEntries = new Dictionary<string, CSVEntry>();


            for (int i=1;i<csvArray.Count;i++)
            {
                newEntries.Add(csvArray[i][0],new CSVEntry(PadJAggedStringArray(header,csvArray[i]),header));
            }
            return newEntries;
        }

        static string[] PadJAggedStringArray(string[] header, string[] entry)
        {
            if (header.Length == entry.Length)
                return entry;

            string[] newEntry = new string[header.Length];
            entry.CopyTo(newEntry, 0);
            return newEntry;
        }

        public void Print()
        {
            Debug.Log("CSVData: " + Name);

            for (int i = 0; i < RawData.Count; i++)
            {
                string line = "";
                for (int j = 0; j < RawData[i].Length; j++)
                {
                    line += RawData[i][j]+"\t";
                }
                Debug.Log(line);
            }

        }
    }
}