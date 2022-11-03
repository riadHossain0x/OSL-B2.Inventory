using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OSL_B2.Inventory.Web.Utilities
{
    public interface IFileOperation
    {
        bool Validate();
        string SaveFile(string uploadPath);
    }

    public class FileOperation : IFileOperation
    {
        private readonly HttpPostedFileBase _file;

        public FileOperation(HttpPostedFileBase file)
        {
            _file = file;
        }

        public bool Validate()
        {
            if (_file == null)
                throw new ArgumentNullException("File not found!");

            var fileLength = _file.ContentLength;
            var maxLength = 1024 * 1024 * 5; // 5Mb
            var allowedExt = new[] { ".jpg", ".jpeg", ".png", "gif" };

            if (!allowedExt.Contains(_file.FileName.Substring(_file.FileName.LastIndexOf('.'))) || fileLength > maxLength)
            {
                return false;
            }

            return true;
        }

        public string SaveFile(string uploadPath)
        {
            if (string.IsNullOrWhiteSpace(uploadPath))
                throw new ArgumentNullException("File upload path not found!");

            if (_file == null)
                throw new ArgumentNullException("File not found!");

            //string fileName = Path.GetFileNameWithoutExtension(_file.FileName);

            string fileExt = Path.GetExtension(_file.FileName);

            string fileName = string.Format($"{DateTime.Now.ToString("yyyyMMdd")}-{Guid.NewGuid()}");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var image = uploadPath + fileName;
            _file.SaveAs(image);

            return image;
        }
    }
}