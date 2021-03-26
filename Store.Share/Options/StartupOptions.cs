namespace Store.Shared.Options
{
    public class StartupOptions
    {
        public string Version { get; set; }
        public string TitleApplication { get; set; }
        public string Bearer { get; set; }
        public string SwaggerUrl { get; set; }
        public string SwaggerName { get; set; }
        public string RootPath { get; set; }
        public string SourcePath { get; set; }
    }

}
