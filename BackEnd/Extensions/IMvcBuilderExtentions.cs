using BackEnd.Utilities.Formatters;

namespace BackEnd.Extensions
{
    public static class IMvcBuilderExtentions
    {
        public static IMvcBuilder AddCustomCsvFormatter(this IMvcBuilder builder)
        {
            return builder.AddMvcOptions(config=>{
                config.OutputFormatters.Add(new CsvOutputFormatter());
            });
        }
    }
}