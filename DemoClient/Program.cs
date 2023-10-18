using AutoMapper;
using Newtonsoft.Json;
using System.Dynamic;
using System.Net;
using System.Text.Json.Serialization;


namespace DemoClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RestSharpService service = new RestSharpService();
            int selectedInt = new();
            bool breakNow = default;
            Dictionary<int, string> selectedTranslator = new Dictionary<int, string> 
            {
                {1, "pirate" },
                {2, "yoda" },
                {3, "valyrian" }
            };

            Console.WriteLine("Hello, World! Let's hear it from our funtranslator.");
            while (!breakNow)
            {
                Console.WriteLine("Press 1. for Pirate \nPress 2. for Yoda \nPress 3. for Valyrian");
                var select = Console.ReadKey();
                if (char.IsDigit(select.KeyChar))
                {
                    selectedInt = int.Parse(select.KeyChar.ToString()); // use Parse if it's a Digit
                    if (selectedInt > 0 && selectedInt < 4)
                    {
                        breakNow = true;
                    }
                }
                else
                {
                    Console.WriteLine("Try again, 1, 2 or 3");
                }
            }
            var response =service.GetTranslatedText($"Let's hear it in {selectedTranslator[selectedInt]} style: Hello Friends", selectedTranslator[selectedInt]);
            Console.WriteLine($"Let us print the raw content from the response: { response.Content}");

            ResponseClass responseClass = new ResponseClass();
            if (response.StatusCode.Equals(HttpStatusCode.OK) && response.Content != null)
            {  
                responseClass = JsonConvert.DeserializeObject<ResponseClass>(response.Content);
                Console.WriteLine($"Mapped to class, our text:\n{responseClass.Contents.Text}, \ntranslated to: \n{responseClass.Contents.Translated}\n with translator:\n{responseClass.Contents.Translation}");
            }
            Console.WriteLine("Press any key");
            Console.ReadKey();
        }


    }

    
    public class ResponseClass 
    {
        public SuccessResponse Success {  get; set; }
        public ContentResponse Contents { get; set; }

        public class SuccessResponse
        {
            int Total { get; set; }
        }
        public class ContentResponse
        {
            public string Translated { get; set; }
            public string Text { get; set; }
            public string Translation { get; set; }
        }
    }
}