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

                // load spells
                string spells_en_titanium = "Titanium/spells_us_dc86fe0303f5282fe8790f772ecaa93c.txt";
                EQSpell[] spellRecords = new SpellFileReader().ReadSpellFileRecords(spells_en_titanium);
                Console.WriteLine("Read {0} spell records.", spellRecords.Length);

                // load spell effects (old)
                SpellEffectOld[] oldSpellEffects = EQSpellEffectsOld.ReadFile("Titanium/spells.eff");
                Console.WriteLine("Read {0} old spell effect records.", oldSpellEffects.Length);

                // load spell effects (new)
                SpellEffectNew[] newSpellEffects = EQSpellEffectsNew.ReadFile("Titanium/spellsnew.eff");
                Console.WriteLine("Read {0} new spell effect records.", newSpellEffects.Length);

                // load dbstr
                EQStringDB dbstr = new EQStringDB("Titanium/dbstr_us.txt");
                Console.WriteLine("Read {0} string database records.", dbstr.Count);

                // generate the static website pages
                new TemplateRender().RenderAll(new TemplateRenderContext()
                {
                    SpellFileRecords = spellRecords,
                    EQStringDB = dbstr,
                    OldSpellEffects = oldSpellEffects,
                    NewSpellEffects = newSpellEffects
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.Read();
            }
        }
    }
}
