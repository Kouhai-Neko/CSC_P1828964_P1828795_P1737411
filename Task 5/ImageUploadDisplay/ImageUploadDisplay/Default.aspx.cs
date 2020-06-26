using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

using Amazon.S3;
using Amazon.S3.Model;

namespace ImageUploadDisplay
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }//end of Page_Load()

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            // Display the Image
            {
                System.IO.Stream stream = FileUpload1.PostedFile.InputStream;
                System.IO.BinaryReader br = new System.IO.BinaryReader(stream);
                Byte[] bytes = br.ReadBytes((Int32)stream.Length);
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                Image1.ImageUrl = "data:image/jpeg;base64," + base64String;
                Image1.Visible = true;
            }// end of displaying image code

            // Store to S3
            {
                // Do not actually store your IAM credentials in code. EC2 Role is best
                var awsKey = "ACCESS KEY HERE";
                var awsSecretKey = "SECRET KEY HERE";
                var bucketRegion = Amazon.RegionEndpoint.USEast1;   // Your bucket region
                var s3 = new AmazonS3Client(awsKey, awsSecretKey, bucketRegion);
                var putRequest = new PutObjectRequest();
                putRequest.BucketName = "csctask5";        // Your bucket name
                putRequest.ContentType = "image/jpeg";
                putRequest.InputStream = FileUpload1.PostedFile.InputStream;
                // key will be the name of the image in your bucket
                putRequest.Key = FileUpload1.FileName;
                PutObjectResponse putResponse = s3.PutObject(putRequest);

            }
            
        }// end of btnUpload_Click()
    }
}