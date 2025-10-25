using System;
using System.Collections.Generic;

namespace Evie
{
    public class EQStringDB
    {
        public int Count { get; set; }
        public Dictionary<int, Dictionary<int, string>> data = new Dictionary<int, Dictionary<int, string>>();
        public EQStringDB(string filename)
        {
            string[] lines = System.IO.File.ReadAllLines(filename);
            data.Clear();
            Count = 0;
            foreach (string line in lines)
            {
                if (!String.IsNullOrWhiteSpace(line))
                {
                    string[] fields = line.Split('^');
                    int id1 = int.Parse(fields[0]);
                    int id2 = int.Parse(fields[1]);
                    string val = fields[2];
                    if (!data.ContainsKey(id1))
                    {
                        data.Add(id1, new Dictionary<int, string>());
                    }
                    var values = data[id1];
                    values.Add(id2, val);
                    Count++;
                }
            }
        }

        public string GetString(int id, int type)
        {
            if (data.ContainsKey(id) && data[id] != null && data[id].ContainsKey(type))
                return data[id][type];
            //return String.Format("Unknown DB String {0}-{0}", id, type);
            return String.Empty;
        }
    }
}
