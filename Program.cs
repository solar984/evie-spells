using Evie.Titanium;
using System;
using System.Linq;

namespace Evie
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string spells_en_titanium = "Titanium/spells_us_dc86fe0303f5282fe8790f772ecaa93c.txt";
                SpellFileRecord[] spellRecords = new SpellFileReader().ReadSpellFileRecords(spells_en_titanium);
                Console.WriteLine("Read {0} spell records.", spellRecords.Length);

                new TemplateRender().RenderAll(new TemplateRenderContext() { SpellFileRecords = spellRecords });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.Read();
            }
        }
    }
}
