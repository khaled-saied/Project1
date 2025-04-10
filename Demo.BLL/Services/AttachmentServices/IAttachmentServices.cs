using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Demo.BLL.Services.AttachmentServices
{
    public interface IAttachmentServices
    {
        //Upload file
        public string? UploadFile(IFormFile file, string FolderName);


        //Delete file
        public bool DeleteFile(string filePath);
    }
}
