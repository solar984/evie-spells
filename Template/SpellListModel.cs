using System;
using System.Linq;

namespace Evie.Template
{
    public class SpellListModel : LayoutModel
    {
        public SpellListModel(TemplateRenderContext context) : base(context)
        {
            Spells = context.SpellFileRecords.Where(s => !String.IsNullOrWhiteSpace(s.name)).OrderBy(s => s.name).ToArray();
        }

        public EQSpell[] Spells { get; set; }
    }
}
