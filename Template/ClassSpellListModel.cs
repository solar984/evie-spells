using Evie.Titanium;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Evie.Template
{
    public class ClassSpellListModel : LayoutModel
    {
        public ClassSpellListModel(TemplateRenderContext context, string eqclass) : base(context)
        {
            ClassName = EQClass.Classes.First(c => c.ShortName == eqclass).LongName;

            for (int level = 1; level <= 70; level++)
            {
                List<SpellFileRecord> spells = new List<SpellFileRecord>();

                if (eqclass == "WAR")
                {
                    spells = context.SpellFileRecords.Where(s => s.classes1 == level.ToString(CultureInfo.InvariantCulture)).ToList();
                }
                if (eqclass == "CLR")
                {
                    spells = context.SpellFileRecords.Where(s => s.classes2 == level.ToString(CultureInfo.InvariantCulture)).ToList();
                }
                if (eqclass == "PAL")
                {
                    spells = context.SpellFileRecords.Where(s => s.classes3 == level.ToString(CultureInfo.InvariantCulture)).ToList();
                }
                if (eqclass == "RNG")
                {
                    spells = context.SpellFileRecords.Where(s => s.classes4 == level.ToString(CultureInfo.InvariantCulture)).ToList();
                }
                if (eqclass == "SHD")
                {
                    spells = context.SpellFileRecords.Where(s => s.classes5 == level.ToString(CultureInfo.InvariantCulture)).ToList();
                }
                if (eqclass == "DRU")
                {
                    spells = context.SpellFileRecords.Where(s => s.classes6 == level.ToString(CultureInfo.InvariantCulture)).ToList();
                }
                if (eqclass == "MNK")
                {
                    spells = context.SpellFileRecords.Where(s => s.classes7 == level.ToString(CultureInfo.InvariantCulture)).ToList();
                }
                if (eqclass == "BRD")
                {
                    spells = context.SpellFileRecords.Where(s => s.classes8 == level.ToString(CultureInfo.InvariantCulture)).ToList();
                }
                if (eqclass == "ROG")
                {
                    spells = context.SpellFileRecords.Where(s => s.classes9 == level.ToString(CultureInfo.InvariantCulture)).ToList();
                }
                if (eqclass == "SHM")
                {
                    spells = context.SpellFileRecords.Where(s => s.classes10 == level.ToString(CultureInfo.InvariantCulture)).ToList();
                }
                if (eqclass == "NEC")
                {
                    spells = context.SpellFileRecords.Where(s => s.classes11 == level.ToString(CultureInfo.InvariantCulture)).ToList();
                }
                if (eqclass == "WIZ")
                {
                    spells = context.SpellFileRecords.Where(s => s.classes12 == level.ToString(CultureInfo.InvariantCulture)).ToList();
                }
                if (eqclass == "MAG")
                {
                    spells = context.SpellFileRecords.Where(s => s.classes13 == level.ToString(CultureInfo.InvariantCulture)).ToList();
                }
                if (eqclass == "ENC")
                {
                    spells = context.SpellFileRecords.Where(s => s.classes14 == level.ToString(CultureInfo.InvariantCulture)).ToList();
                }
                if (eqclass == "BST")
                {
                    spells = context.SpellFileRecords.Where(s => s.classes15 == level.ToString(CultureInfo.InvariantCulture)).ToList();
                }
                if (eqclass == "BER")
                {
                    spells = context.SpellFileRecords.Where(s => s.classes16 == level.ToString(CultureInfo.InvariantCulture)).ToList();
                }

                if (spells.Count > 0)
                {
                    Group group = new Group();
                    group.Name = String.Format(CultureInfo.InvariantCulture, "Level {0}", level);
                    group.Spells.AddRange(spells.OrderBy(s => Convert.ToInt32(s.id, CultureInfo.InvariantCulture)));
                    Groups.Add(group);
                }
            }
        }

        public string ClassName { get; set; }

        public class Group
        {
            public string Name { get; set; }
            public List<SpellFileRecord> Spells { get; set; } = new List<SpellFileRecord>();
        }
        public List<Group> Groups { get; set; } = new List<Group>();
    }
}
