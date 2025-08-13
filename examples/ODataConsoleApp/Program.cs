//System.Web.Http.OData.o

using Microsoft.Data.OData;

var e = new ODataError
{
    Code = "ErrorCode",
    Message = "An error occurred while processing your request.",
    InnerError = new ODataInnerError
    {
        Message = "Detailed error message",
        TypeName = "ExactOnline.Api.Client.ExampleError"
    }
};

//var client = new ExactOnlineClient("https://start.exactonline.nl", GetAccessTokenCallback);

//await client.InitializeDivisionAsync();

//var me = await client.For<Me>().Select("UserName").GetAsync();
//var division = me.List.FirstOrDefault()?.CurrentDivision;

//var p = await client.For<TimeTransaction>().Select(x => x.Created).Select(x => x.ID).GetAsync();

//foreach (var tt in p.List)
//{
//    Console.WriteLine($"TimeTransaction ID: {tt.ID}, Created: {tt.Created}");
//}

//int xxx = 0;

//return;

//async Task<string> GetAccessTokenCallback(CancellationToken arg)
//{
//    return "";
//}





//await GenerateOpenApiDescription();

//return;

//static async Task GenerateOpenApiDescription()
//{
//    IEdmModel model = GetEdmModel();
//    OpenApiConvertSettings settings = new OpenApiConvertSettings
//    {
//        // configuration
        
//    };
//    OpenApiDocument document = model.ConvertToOpenApi(settings);
//    var outputJSON = await document.SerializeAsJsonAsync(OpenApiSpecVersion.OpenApi3_0);

//    int x = 9;
//    //var outputYAML = document.SerializeAsYaml(OpenApiSpecVersion.OpenApi3_0);
//}

//static IEdmModel GetEdmModel()
//{
//    string csdlFilePath = @"C:\temp\edmx.xml";
//    string csdl = File.ReadAllText(csdlFilePath);
//    IEdmModel model = CsdlReader.Parse(XElement.Parse(csdl).CreateReader());
//    return model;
//}
