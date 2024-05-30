using RazorEngineCore;

namespace SCJ.Booking.TaskRunner.Utils
{
    public class RazorHelper
    {
        public static async Task<string> RenderTemplate(string viewFileName, object model)
        {
            IRazorEngine razorViewEngine = new RazorEngine();
            var templatePath = $"EmailTemplates{Path.DirectorySeparatorChar}{viewFileName}";
            using StreamReader reader = new(templatePath);
            string viewText = await reader.ReadToEndAsync();
            IRazorEngineCompiledTemplate template = await razorViewEngine.CompileAsync(viewText);
            return await template.RunAsync(model);
        }
    }
}
