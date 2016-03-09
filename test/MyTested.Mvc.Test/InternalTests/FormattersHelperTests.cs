namespace MyTested.Mvc.Test.InternalTests
{
    using Internal.Formatters;
    using Setups;
    using Setups.Models;
    using System;
    using System.IO;
    using System.Text;
    using Xunit;

    public class FormattersHelperTests
    {
        [Fact]
        public void ReadFromStreamShouldWorkCorrectly()
        {
            var stream = new MemoryStream();
            var streamWriter = new StreamWriter(stream);
            streamWriter.WriteLine(@"{""Integer"":1,""RequiredString"":""Test""}");
            streamWriter.Flush();

            var result = FormattersHelper.ReadFromStream<RequestModel>(stream, ContentType.ApplicationJson, Encoding.UTF8);

            Assert.NotNull(result);
            Assert.Equal(1, result.Integer);
            Assert.Equal("Test", result.RequiredString);
            Assert.Null(result.NonRequiredString);
            Assert.Equal(0, result.NotValidateInteger);
        }

        [Fact]
        public void ReadFromStreamShouldReturnNullIfContentCannotBeParsed()
        {
            var stream = new MemoryStream();
            var streamWriter = new StreamWriter(stream);
            streamWriter.WriteLine(@"{""Integer"":1,""RequiredString"":""Test""");
            streamWriter.Flush();

            var result = FormattersHelper.ReadFromStream<RequestModel>(stream, ContentType.ApplicationJson, Encoding.UTF8);

            Assert.Null(result);
        }

        [Fact]
        public void ReadFromStreamShouldThrowExceptionIfNoFormatterIsFound()
        {
            Test.AssertException<NullReferenceException>(
                () =>
                {
                    var stream = new MemoryStream();
                    var streamWriter = new StreamWriter(stream);
                    streamWriter.WriteLine(@"test");
                    streamWriter.Flush();

                    FormattersHelper.ReadFromStream<RequestModel>(stream, ContentType.TextHtml, Encoding.UTF8);
                },
                "Formatter able to process 'text/html' could not be resolved from the services provider. Before running this test case, the formatter should be registered in the 'StartsFrom' method and cannot be null.");
        }

        [Fact]
        public void ReadFromStreamShouldThrowExceptionIfNoModelIsDifferentType()
        {
            Test.AssertException<InvalidDataException>(
                () =>
                {
                    var stream = new MemoryStream();
                    var streamWriter = new StreamWriter(stream);
                    streamWriter.WriteLine(@"test");
                    streamWriter.Flush();

                    FormattersHelper.ReadFromStream<RequestModel>(stream, ContentType.TextPlain, Encoding.UTF8);
                },
                "Expected stream content to be formatted to RequestModel when using 'text/plain', but instead received String.");

        }

        [Fact]
        public void WriteToStreamShouldWorkCorrectlyWithString()
        {
            var result = FormattersHelper.WriteToStream("test", ContentType.TextPlain, Encoding.UTF8);

            using (var reader = new StreamReader(result))
            {
                var text = reader.ReadToEnd();

                Assert.Equal("test", text);
            }
        }

        [Fact]
        public void WriteToStreamShouldWorkCorrectlyWithObject()
        {
            var result = FormattersHelper.WriteToStream(new { id = 1, text = "test" }, ContentType.ApplicationJson, Encoding.UTF8);

            using (var reader = new StreamReader(result))
            {
                var text = reader.ReadToEnd();

                Assert.Equal(@"{""id"":1,""text"":""test""}", text);
            }
        }
    }
}
