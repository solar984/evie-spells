using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Evie.Titanium
{
    public class SpellFileReader
    {
        Dictionary<int, PropertyInfo> propertyMap = new Dictionary<int, PropertyInfo>();
        int fieldCount = 0;

        #region ReadSpellFileRecords
        public EQSpell[] ReadSpellFileRecords(string path)
        {
            fieldCount = 0;
            // set up property map to set each field by its ordinal position in MapSpellRecord
            propertyMap.Clear();
            foreach (var p in typeof(SpellFileRecord).GetProperties())
            {
                var ca = p.GetCustomAttributes(true).Where(a => a.GetType().Name == "ColumnAttribute").FirstOrDefault() as ColumnAttribute;
                if (ca != null)
                {
                    int fieldNumber = ca.Order;
                    propertyMap[fieldNumber] = p;
                }
            }

            List<EQSpell> lines = new List<EQSpell>();
            using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        if (line.StartsWith("#"))
                        {
                            continue;
                        }
                        var fields = line.Split('^');
                        if (fields.Length < 2 || line.StartsWith("#"))
                        {
                            continue;
                        }
                        if (fieldCount == 0)
                        {
                            // remember the number of fields from the first line read
                            fieldCount = fields.Length;
                            Console.WriteLine("Spell file has {0} fields.", fieldCount);
                        }
                        if (fieldCount != fields.Length)
                        {
                            // this prints out any lines that don't have the expected number of fields
                            Console.WriteLine("{0} {1} - {2} fields", fields[0], fields[1], fields.Length);
                        }
                        lines.Add(MapSpellRecord(fields));
                    }
                }
            }

            return lines.ToArray();
        }
        #endregion

        #region MapSpellRecord
        public EQSpell MapSpellRecord(string[] fields)
        {
            EQSpell spell = new EQSpell();

            for (int i = 0; i < fields.Length; i++)
            {
                propertyMap[i].SetValue(spell, fields[i]);
            }

            return spell;
        }
        #endregion
    }
}
