using Evie.Template;
using Evie.Titanium;
using RazorLight;
using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.IO;
using System.Text;

namespace Evie
{
    /// <summary>
    /// Static web pages are rendered from templates using RazorLight
    /// </summary>
    internal class TemplateRender
    {
        string templateDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Template");
        string outputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "www");

        public void RenderAll(TemplateRenderContext context)
        {
            // index.html
            Render
            (
                "Index.cshtml",
                new LayoutModel(context),
                "index.html"
            );

            // alphabetical list - index_alpha.html
            Render
            (
                "SpellList.cshtml",
                new SpellListModel(context),
                "index_alpha.html"
            );

            // class spell lists - index_XXX.html
            foreach (var eqclass in EQClass.Classes)
            {
                Render
                (
                    "ClassSpellList.cshtml",
                    new ClassSpellListModel(context, eqclass.ShortName),
                    String.Format(CultureInfo.InvariantCulture, "index_{0}.html", eqclass.ShortName)
                );
            }

            // search.html
            Render
            (
                "Search.cshtml",
                new SearchModel(context),
                "search.html"
            );

            // spell detail - XXXX.html
            foreach (var spell in context.SpellFileRecords)
            {
                int spell_id = Convert.ToInt32(spell.id, CultureInfo.InvariantCulture);
                if (spell_id % 100 == 0)
                    Console.Write(".");
                try
                {
                    Render
                    (
                        "SpellDetail.cshtml",
                        new SpellDetailModel(context, spell_id),
                        String.Format(CultureInfo.InvariantCulture, "{0}.html", spell_id)
                    );
                }
                catch
                {
                    Console.WriteLine("Exception while rendering spell detail page for {0} ({1})", spell.name, spell.id);
                    throw;
                }
            }
            Console.WriteLine();

            // favicon
            File.Copy(Path.Combine("Template", "favicon.png"), Path.Combine("www", "favicon.png"), true);

            // spell file
            File.Copy(Path.Combine("Titanium", "spells_us_dc86fe0303f5282fe8790f772ecaa93c.txt"), Path.Combine("www", "spells_us_dc86fe0303f5282fe8790f772ecaa93c.txt"), true);
        }

        RazorLightEngine Engine { get; set; }
        private readonly ConcurrentDictionary<string, ITemplatePage> compiledTemplates = new ConcurrentDictionary<string, ITemplatePage>();
        public void Render<T>(string templateName, T model, string outputFileName)
        {
            // cache engine
            if (Engine == null)
            {
                Engine = new RazorLightEngineBuilder()
                    .UseFileSystemProject(templateDirectory)
                    .UseMemoryCachingProvider()
                    .Build();
            }

            // compile template
            if (!compiledTemplates.ContainsKey(templateName))
            {
                var template = Engine.CompileTemplateAsync(templateName).Result;
                compiledTemplates[templateName] = template;
            }

            // render template with model
            string result = Engine.RenderTemplateAsync(compiledTemplates[templateName], model).Result;

            // write output file
            Directory.CreateDirectory(outputDirectory);
            File.WriteAllText(Path.Combine(outputDirectory, outputFileName), result, Encoding.UTF8);
        }
    }

    public class TemplateRenderContext
    {
        public SpellFileRecord[] SpellFileRecords { get; set; }
    }
}
