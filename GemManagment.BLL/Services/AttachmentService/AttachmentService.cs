using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.BLL.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        //public AttachmentService(IWebHostEnvironment webhost)
        //{
        //    this.webhost = webhost;
        //}
        readonly List<string> allowedExtensions = new List<string>() { ".png", ".jpg" , ".jpeg" };
        readonly long maxSize = 5 * 1024 * 1024;
        //private readonly IWebHostEnvironment webhost;

        public bool Delete(string folderName, string fileName)
        {
            try
            {
                if (string.IsNullOrEmpty(folderName) || string.IsNullOrEmpty(fileName)) return false;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images",folderName, fileName);
                if (File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"{ex.Message} from delet the file");
                return false;
            }
        }

        public string? Upload(string folderName, IFormFile formFile)
        {
            try
            {
                // check extension and size and folder file name
                if (string.IsNullOrEmpty(folderName) || string.IsNullOrEmpty(formFile.FileName)) return null;

                var Extension = Path.GetExtension(formFile.FileName).ToLower();
                if (!allowedExtensions.Contains(Extension)) return null;




                // get the located folder path wwwroot/images/{member}
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "images", folderName);

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                // unique name for file
                var filename = Guid.NewGuid() + Extension;

                var filePath = Path.Combine(folderPath, filename);
                // stream to copy in it
                using var stream = new FileStream(filePath, FileMode.Create);
                formFile.CopyTo(stream);
                return filename;
                // return filenaem
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} from upload");
                return null;
            }

        }
    }
}
