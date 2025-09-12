using System.Collections.Generic;

namespace Evie.Template
{
    public class SearchModel : LayoutModel
    {
        public SearchModel(TemplateRenderContext context) : base(context)
        {
            List<string[]> names = new List<string[]>();
            foreach(var spell in context.SpellFileRecords)
            {
                if (!string.IsNullOrWhiteSpace(spell.name))
                {
                    List<string> s = new List<string>();
                    s.Add(spell.id);
                    s.Add(spell.name);
                    names.Add(s.ToArray());
                }
            }
            Names = names.ToArray();
        }

        public string[][] Names { get; set; }
    }
}
