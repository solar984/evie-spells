using Evie.Titanium;
using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Evie.Template
{
    public class SpellDetailModel : LayoutModel
    {
        public SpellDetailModel(TemplateRenderContext context, int spell_id) : base(context)
        {
            this.spell_id = spell_id;
            var spell = context.SpellFileRecords.FirstOrDefault(s => s.id == spell_id.ToString(CultureInfo.InvariantCulture));
            if (spell != null)
            {
                this.spell = spell;
            }

            Classes = FormatClasses();
        }

        public int spell_id { get; set; }
        public SpellFileRecord spell { get; set; }

        public string Classes { get; set; }
        public string FormatClasses()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var eqclass in EQClass.Classes)
            {
                if (spell.classes[eqclass.Number] != 255)
                {
                    sb.AppendFormat("{0}/{1} ", eqclass.ShortName, spell.classes[eqclass.Number]);
                }
            }

            return sb.Length == 0 ? "None" : sb.ToString().Trim();
        }

        public bool IsBlankSpellEffect(int slot)
        {
            if (spell == null) return false;

            if (spell.effect[slot] == (int)EQSpellEffect.Blank)
                return true;

            if (spell.effect[slot] == (int)EQSpellEffect.CHA && spell.calc[slot] == 100 && spell.base1[slot] == 0)
                return true;

            return false;
        }

        public string FormatTimeString(string string_ms)
        {
            return String.Format(CultureInfo.InvariantCulture, "{0:F3} sec", SpellFileRecord.ConvertToDouble(string_ms) / 1000.0);
        }

        public string FormatDetrimental(string string_val)
        {
            int val = SpellFileRecord.ConvertToInt32(string_val);
            switch (val)
            {
                case 0: return "Detrimental";
                case 1: return "Beneficial";
                case 2: return "Beneficial (Group Only)";
            }
            return string_val;
        }
    }
}
