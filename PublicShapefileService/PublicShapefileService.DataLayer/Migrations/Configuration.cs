namespace PublicShapefileService.DataLayer.Migrations
{
    using ExcelDataReader;
    using PublicShapefileService.Common.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PublicShapefileService.DataLayer.PublicShapefileServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(PublicShapefileService.DataLayer.PublicShapefileServiceContext context)
        {
            context.DownLoadLinks.Add(new DownloadLink
            {
                InternalLink = "file_link1",
                PublicId = new Guid("5fb7097c-335c-4d07-b4fd-000004e2d28e"),
                Timestamp = DateTime.Now,
                Validity = 4,
                ShapefileRequest = null
            });
            context.DownLoadLinks.Add(new DownloadLink
            {
                InternalLink = "file_link2",
                PublicId = new Guid("5fb7097c-335c-4d07-b4fd-000004e2d28e"),
                Timestamp = DateTime.Now,
                Validity = 4,
                ShapefileRequest = null
            });

            context.DownLoadLinks.Add(new DownloadLink
            {
                InternalLink = "file_link",
                PublicId = new Guid("5fb7097c-335c-4d07-b4fd-000004e2d28e"),
                Timestamp = DateTime.Now,
                Validity = 4,
                ShapefileRequest = null
            });

            context.DownLoadLinks.Add(new DownloadLink
            {
                InternalLink = "file_link3",
                PublicId = new Guid("5fb7097c-335c-4d07-b4fd-000004e2d28e"),
                Timestamp = DateTime.Now,
                Validity = 4,
                ShapefileRequest = null
            });


            context.Layers.Add(new Layer
            {
                LayerName = "212413"
            });

            context.Layers.Add(new Layer
            {
                LayerName = "212112"
            });
            context.Layers.Add(new Layer
            {
                LayerName = "2321412"
            });

            context.Layers.Add(new Layer
            {
                LayerName = "2222412"
            });
            context.Layers.Add(new Layer
            {
                LayerName = "3125455"
            });

            context.OperatorResolutions.Add(new OperatorResolution
            {
                OperatorName = "Operator1",
                Resolution = false,
                ResolutionDetails = "details",
                Timestamp = DateTime.Now,
                ShapefileRequest = null
            });
            context.OperatorResolutions.Add(new OperatorResolution
            {
                OperatorName = "Operator2",
                Resolution = false,
                ResolutionDetails = "details",
                Timestamp = DateTime.Now,
                ShapefileRequest = null
            });
            context.OperatorResolutions.Add(new OperatorResolution
            {
                OperatorName = "Operator3",
                Resolution = false,
                ResolutionDetails = "details",
                Timestamp = DateTime.Now,
                ShapefileRequest = null
            });
            context.OperatorResolutions.Add(new OperatorResolution
            {
                OperatorName = "Operator4",
                Resolution = false,
                ResolutionDetails = "details",
                Timestamp = DateTime.Now,
                ShapefileRequest = null
            });


            context.ShapefileRequests.Add(
            new ShapefileRequest
            {
                CUI = "aaa",
                RequestDetails = "none",
                SolicitantEmail = "ceva@altceva",
                SolicitantName = "Operator1",
            });
            context.ShapefileRequests.Add(new ShapefileRequest
            {
                CUI = "bbb",
                RequestDetails = "none",
                SolicitantEmail = "ceva@altceva",
                SolicitantName = "Operator2",
            });
            context.ShapefileRequests.Add(new ShapefileRequest
            {
                CUI = "ccc",
                RequestDetails = "none",
                SolicitantEmail = "ceva@altceva",
                SolicitantName = "Operator3",
            });
            context.ShapefileRequests.Add(new ShapefileRequest
            {
                CUI = "dddd",
                RequestDetails = "none",
                SolicitantEmail = "ceva@altceva",
                SolicitantName = "Operator4",
            });
            context.SaveChanges();

            Locality locality = new Locality();
            using (var stream = File.Open("D:/Localitati.xlsx", FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    reader.Read();

                    reader.GetString(0);
                    reader.GetString(1);
                    reader.GetString(2);
                    do
                    {
                        while (reader.Read())
                        {
                            locality.CountyCode = reader.GetString(0);
                            locality.SirutaCode = reader.GetDouble(1).ToString();
                            locality.LocalityName = reader.GetString(2);
                            locality.FullName = reader.GetString(3);

                            Console.WriteLine(locality.CountyCode);
                            Console.WriteLine(locality.SirutaCode);
                            Console.WriteLine(locality.LocalityName);
                            Console.WriteLine(locality.FullName);

                            context.Localities.Add(locality);
                            context.SaveChanges();
                        }
                    } while (reader.NextResult());
                }
            }

            

            base.Seed(context);

          

        }
    }
}
