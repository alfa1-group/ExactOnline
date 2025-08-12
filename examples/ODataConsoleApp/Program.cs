using System.Xml.Linq;
using ExactOnline.Client.Models.Current;
using ExactOnline.Client.Models.Project;
using ExactOnline.Client.Sdk.Controllers;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Csdl;
using Microsoft.OpenApi;
using Microsoft.OpenApi.OData;

var client = new ExactOnlineClient("https://start.exactonline.nl", GetAccessTokenCallback);

await client.InitializeDivisionAsync();

var me = await client.For<Me>().Select("UserName").GetAsync();
var division = me.List.FirstOrDefault()?.CurrentDivision;

var p = await client.For<TimeTransaction>().Select(x => x.Created).Select(x => x.ID).GetAsync();

foreach (var tt in p.List)
{
    Console.WriteLine($"TimeTransaction ID: {tt.ID}, Created: {tt.Created}");
}

int xxx = 0;

return;

async Task<string> GetAccessTokenCallback(CancellationToken arg)
{
    return "stampNL001.gAAAACj_jJ4lIVPT7fwzu0ZAY3hrawU-p72tS_fLEB-G1drjsOZGX9_iiytJ2CDn6TJk7A17B9r4PPYg9YaWjg_oJUi_4gdqH_JxFjclNLre5yjfB7UIi00vPQz3q8RknmM5E-QETvD0f4wihOZAp-PFpFoyZb-lIUWTIdqNRvTQWr5PNAIAAIAAAABkCEiB3v81NBf8CIkbxDWAzhFKh86EaL5Y10uFFsDbGbuvdRR2quANkn-HPdMpNJI5X7Uyy00RoVm6BCvcK1fz74vzT1-igMaa94U-xPzpDo5_ugfHyvojxEj81oTAMUICenHbYx174mqTwV88A1zFOOCwol5TMlZGjvFl-cNxnxeo5aFJpZ5TJm3cD5iD8Qw34rIkkNWQYzFHGMcu392iUeEe2xus4OY5XzV60U7R5QvxHbd_4Ks7pZgDu2fjQOezUTiZ-MH9quQWKeOwRTWXN1ckPWIXq5HkTW5ruPmcvTFX0kaQ-9G8Khc3LuTA4rvcH4L84aeYBhReC1EEgALPOKroLt-sE2q6JFnSYdufIoh57vu2OEhQHROzAEpc8N837sDgL0km6Mla3Qaf2FEe1H412KA1eMAXwei4vsPcK1voG69t4g-4_8bLu-vnmNbYXk-3rzKI7Jsl0vs7UnaCqqYCTMrrCfKksErtrxbr9rX6G6wMTVadZTsQhhXgcHaYLmgzt1asmbEegsLEx8sj-pZLz6nm4TbsiDmcI6sMrsujLxOJotZRxt_GiXRGakr7NdQ2ZxCkEW4iLebUpZhKoLfZgbD_dvuAo7rO_vC6ay3GGFbRO8g4KEW-d_ogYB0p-8zmzWr5YEB0dy09JByyAFDzWIdKekHeXPDPd6rTRvHLEI8myxn5NOf3Vk_zMoDuUgdsXfQtB0Ij_v4PhIrhMVaW88NSfWY7DLsU0hnEOw";
}





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
