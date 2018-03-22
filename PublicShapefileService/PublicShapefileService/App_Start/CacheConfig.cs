using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.IO;
using PublicShapefileService.Models;
using PublicShapefileService.DataLayer;

namespace PublicShapefileService.App_Start
{
    public class CacheConfig
    {
        public static void DoCaching(String route)
        {
            MemoryCache memory = MemoryCache.Default;
            String[] directories = Directory.GetDirectories(route);
            CacheItemPolicy policy = new CacheItemPolicy();

            //Citire din baza de date
            var publicShapefileContext = PublicShapefileServiceContext.Instance;
            var filesFromDB = publicShapefileContext.GetAllLocalities();
            var count = filesFromDB.Count;
            CacheFile[] localities = new CacheFile[count];
            int i = 0;
            foreach(var locality in filesFromDB)
            {
                localities[i] = new CacheFile(locality.CountyCode, locality.SirutaCode, locality.FullName);
                i++;
            }

            //populare layere
            var x = 0; //counter pt localitatile de pe disc
            foreach (var locality in localities)
            {
                String currentRoute = route + $@"{locality.CountyCode}" + $@"\{locality.SirutaCode}";
                String[] layersRoute = Directory.GetFiles(currentRoute);
                List<String> layers = new List<String>();
                foreach (var layerRoute in layersRoute)
                {
                    char[] delimiters = { '.', '\\' };
                    var fileName = layerRoute.Split(delimiters);
                    String layer = fileName[fileName.Length - 2];
                    layers.Add(layer);
                }
                locality.Layers = layers.ToArray();

                //Add in cache
                CacheItem cItem = new CacheItem(locality.SirutaCode, locality);
                memory.Add(cItem, policy);


                //3 localitati pe disc din Bucuresti
                x++;
                if (x == 3)
                {
                    break;
                }
            }

        }
    }
}