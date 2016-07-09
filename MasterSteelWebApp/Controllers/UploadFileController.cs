using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace MasterSteelWebApp.Controllers
{
    public class UploadFileController : ApiController
    {
        [HttpPost]
        [ActionName("UploadFileImage")]
        public String UploadFileImage()
        {
            String result = "Fail";

            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                var imageFileName = HttpContext.Current.Request["ImageFileName"];
                result = httpPostedFile.ContentType.ToString();
                result = httpPostedFile.FileName;
                result = httpPostedFile.InputStream.Length.ToString();

                Stream streamFile = httpPostedFile.InputStream;
                result = streamFile.Length.ToString();
                byte[] buffer = new byte[streamFile.Length];
                result = buffer.Length.ToString();
                streamFile.Read(buffer, 0, buffer.Length);
                streamFile.Close();

                //FTP Server URL.
                string ftp = "ftp://**********/";

                //FTP Folder name. Leave blank if you want to upload to root folder.
                string ftpFolder = "**********";

                //Create FTP Request.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp + ftpFolder + imageFileName);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                //Enter FTP Server credentials.
                request.Credentials = new NetworkCredential("**********", "**********");
                request.ContentLength = buffer.Length;
                //request.UsePassive = false;
                request.UseBinary = true;
                request.ServicePoint.ConnectionLimit = buffer.Length;
                request.EnableSsl = false;


                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(buffer, 0, buffer.Length);
                    requestStream.Close();
                }

                result = "Success";

            }

            return result;
        }

    }
}
