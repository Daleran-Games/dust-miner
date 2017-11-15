using System.Collections.Generic;

namespace DaleranGames.IO
{
    public class CSVEntry
    {
        public readonly string ID;
        Dictionary<string, string> entryData;
        public List<KeyValuePair<string,string>> Data { get { return new List<KeyValuePair<string, string>>(entryData); } }

        public string this[string header]
        {
            get
            {
                //Debug.Log("Attempting to retrieve: " + header);
                string output;

                if (entryData.TryGetValue(header, out output))
                    return output;
                else
                    throw new KeyNotFoundException(header + " not a valid header for this entry.");
            }
        }

        public CSVEntry(string[]csvLine, string[] csvHeader)
        {
            ID = csvLine[0];
            entryData = ParseCSVArrays(csvLine, csvHeader);
        }

        Dictionary<string,string> ParseCSVArrays(string[] csvLine, string[] csvHeader)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            for (int i=0; i<csvHeader.Length; i++)
            {
                //Debug.Log("Adding:" + csvHeader[i] + "," + csvLine[i]);
                if (!string.IsNullOrEmpty(csvHeader[i]))
                {
                    result.Add(csvHeader[i], csvLine[i]);
                    //Debug.Log("Added");
                }
            }
            return result;
        }

        public List<string> ParseList(string header)
        {
            //Debug.Log("Parsing List " + header);
            return CSVUtility.ParseList(this[header]);
        }
    }
}
