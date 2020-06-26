using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cloudmersive.APIClient.NET.ImageRecognition.Api;
using Cloudmersive.APIClient.NET.ImageRecognition.Client;
using Cloudmersive.APIClient.NET.ImageRecognition.Model;

namespace CSC_TASK_8.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            
            return View();
        }
        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    System.Threading.Thread.Sleep(5000);
        //}

        [HttpPost]
        public ActionResult Test1(HttpPostedFileBase ImageFile)
        {
            
            if (ImageFile == null || ImageFile.ContentLength == 0)
            {
                //Show spinner
                ViewBag.Error = "Please select a file.<br>";
                return View("Index");
            }
            else
            {
                if (ImageFile.FileName.EndsWith("jpg") || ImageFile.FileName.EndsWith("png"))
                {
                    
                    string path = Server.MapPath("~/Doc/" + ImageFile.FileName);
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                    ImageFile.SaveAs(path);
                    // Configure API key authorization: Apikey
                    Configuration.Default.AddApiKey("Apikey", "APIKEYHERE");

                    var apiInstance = new RecognizeApi();
                    var imageFile = new System.IO.FileStream(path, System.IO.FileMode.Open); // System.IO.Stream | Image file to perform the operation on.  Common file formats such as PNG, JPEG are supported.

                    try
                    {
                        // Describe an image in natural language
                        ImageDescriptionResponse result = apiInstance.RecognizeDescribe(imageFile);
                        Debug.WriteLine(result);
                        ViewBag.BestOutcomeDesc = result.BestOutcome.Description;
                        ViewBag.BestOutcomeScore = result.BestOutcome.ConfidenceScore;
                        //Hide spinner 
                        return View("Success");
                    }
                    catch (Exception e)
                    {
                        Debug.Print("Exception when calling RecognizeApi.RecognizeDescribe: " + e.Message);
                        return View("Index");
                    }
                }
                else
                {
                    ViewBag.Error = "File type is incorrect.<br>";
                    return View("Index");
                }
            }
        }

    }
}
