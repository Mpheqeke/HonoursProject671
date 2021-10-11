using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Interfaces
{
    public interface IGoogleCloudStorage
    {
        string GetFileAsync(string fileNameForStorage);
        Task<string> UploadFileAsync(IFormFile imageFile, string fileNameForStorage);
        Task DeleteFileAsync(string fileUrl);
    }
}
