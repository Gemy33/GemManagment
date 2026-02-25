using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.BLL.Services.AttachmentService
{
    public interface IAttachmentService
    {
        string? Upload(string folderName , IFormFile formFile);
        bool Delete(string folderName , string fileName);
    }
}
