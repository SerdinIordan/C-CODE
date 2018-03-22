using PublicShapefileService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;

namespace PublicShapefileService.Controllers
{
    public class DownloadController : Controller
    {
        MemoryCache memory = MemoryCache.Default;

        [HttpGet]
        public CacheFile[] GetLocality(String word)
        {
            var elements = memory
                .Where(obj => (obj.Value as CacheFile) != null)
                .Select(pair => (CacheFile)pair.Value)
                .Where(value => value.Name.ToLower().StartsWith(word.ToLower()));

            return elements.ToArray();      
        }

        [HttpPost]
        public String[] GetLayers(String siruta)
        {
            var layers = memory
                .Where(obj => (obj.Value as CacheFile) != null)
                .Where(pair => pair.Key == siruta)
                .Select(pair => ((CacheFile)pair.Value).Layers);

            return layers.First();
        }
    }
}