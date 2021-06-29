using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Core.Interfaces
{
	public interface IFileStorageHelper
	{
        string UploadFile(string fileName, string containerName, byte[] fileData);
        void RemoveFile(string fileName, string containerName);
        byte[] DownloadFile(Uri filePath);
        string CheckExtension(string ext);
    }
}
