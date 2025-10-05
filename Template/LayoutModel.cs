namespace Evie.Template
{
    public class LayoutModel
    {
        public LayoutModel(TemplateRenderContext context)
        {
            Context = context;
        }

        public TemplateRenderContext Context { get; set; }
        public string PageTitle { get; set; }
        public string HeadContent { get; set; }
    }
}
