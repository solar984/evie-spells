using Evie.Titanium;
using System;

namespace Evie
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // set the invariant culture for string formatting to not depend on localization settings (1.23 vs 1,23 for example)
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

                string spells_en_titanium = "Titanium/spells_us_dc86fe0303f5282fe8790f772ecaa93c.txt";
                EQSpell[] spellRecords = new SpellFileReader().ReadSpellFileRecords(spells_en_titanium);
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
