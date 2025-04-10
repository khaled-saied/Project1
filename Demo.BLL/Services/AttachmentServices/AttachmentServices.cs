using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Demo.BLL.Services.AttachmentServices
{
    public class AttachmentServices : IAttachmentServices
    {

        List<string> allowedExtension = new List<string> { ".jpg", ".png", ".jpeg" };
        const int MaxFileSize = 2_097_152; // 2 MB

        //Upload file
        public string? UploadFile(IFormFile file, string FolderName)
        {
            //1.Check Extension
            if (!allowedExtension.Contains(Path.GetExtension(file.FileName)))
                return null;

            //2.Check Size
            if(file.Length == 0 || file.Length > MaxFileSize)
                return null;

            //3.Get Located Folder Path
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", FolderName);

            //4.Make Attachment Name Unique-- GUID
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";

            //5.Get File Path
            var filePath = Path.Combine(folderPath, fileName);

            //6.Create File Stream To Copy File[Unmanaged]
            using FileStream Fs = new FileStream(filePath, FileMode.Create);

            //7.Use Stream To Copy File
            file.CopyTo(Fs);

            //8.Return FileName To Store In Database
            return fileName;

        }



        public bool DeleteFile(string filePath)
        {
            if(File.Exists(filePath)) 
                return false;
            File.Delete(filePath);
            return true;
        }

    }
}
