namespace MyTested.AspNetCore.Mvc.Internal.Formatters
{
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;

    public class StringInputFormatter : TextInputFormatter
    {
        public StringInputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/plain"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        public override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding effectiveEncoding)
        {
            var request = context.HttpContext.Request;
            using (var reader = new StreamReader(request.Body, effectiveEncoding))
            {
                var stringContent = reader.ReadToEnd();
                return InputFormatterResult.SuccessAsync(stringContent);
            }
        }
    }
}
