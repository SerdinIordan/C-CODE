using PublicShapefileService.Models;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Caching;

namespace PublicShapefileService.Helpers
{
    public class DownloadHelper : IDownloadHelper
    {
        MemoryCache memory = MemoryCache.Default;

        public FileStream[] DownloadFile(CacheFile locality, String[] layers)
        {
            var route = ConfigurationManager.AppSettings["route"];
            var layerExtension = ConfigurationManager.AppSettings["fileExtension"];
            var judet = locality.CountyCode;
            var siruta = locality.SirutaCode;
            var count = layers.Count();
            FileStream[] filesToTransfer = new FileStream[count];
            var index = 0;
            
            foreach (var layerName in layers)
            {  
                var layer = layerName + layerExtension;
                String fileRoute = route + $@"\{judet}\{siruta}\{layer}";
                filesToTransfer[index] = GetFileStream(fileRoute);
                index++;
            }
            return filesToTransfer;
        }

        private FileStream GetFileStream(String route)
        {
            FileStream fileToTransfer = new FileStream(route, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileToTransfer.Length;
                byte[] buffer = new byte[length];
                int count;
                int sum = 0;

                while ((count = fileToTransfer.Read(buffer, sum, length - sum)) > 0)
                    sum += count;
            }
            catch
            {

            }
            finally
            {
                //fileToTransfer.Close();
            }
            return fileToTransfer;
        }
    }
}