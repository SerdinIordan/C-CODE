using PublicShapefileService.Common.Interfaces;
using PublicShapefileService.Common.Models;
using PublicShapefileService.DataLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;

namespace PublicShapefileService.DataLayer
{
    public class PublicShapefileServiceContext : DbContext, IRepository
    {
        private static PublicShapefileServiceContext instance;

        public static PublicShapefileServiceContext Instance
        {
            get
            {
                if (instance == null)
                    instance = new PublicShapefileServiceContext();
                return instance;
            }
        }


        private PublicShapefileServiceContext() : base()
        {
            Database.SetInitializer<PublicShapefileServiceContext>(new CreateDatabaseIfNotExists<PublicShapefileServiceContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<DownloadLink>()
                .HasOptional(dl => dl.ShapefileRequest)
                .WithOptionalDependent(sr => sr.DownloadLink)
                .Map(m => m.MapKey("ShapefileRequestId"));

            modelBuilder.Entity<OperatorResolution>()

                .HasOptional(or => or.ShapefileRequest)

                .WithOptionalDependent(sr => sr.OperatorResolution)
                .Map(m => m.MapKey("ShapefileRequestId"));

            modelBuilder.Entity<ShapefileRequest>()
                .HasOptional(sr => sr.DownloadLink)
                .WithOptionalPrincipal(p => p.ShapefileRequest);

            modelBuilder.Entity<ShapefileRequest>()
                .HasOptional(sr => sr.OperatorResolution)
                .WithOptionalPrincipal(p => p.ShapefileRequest);

        }

        private DbSet<DownloadLink> _downLoadLinks;
        public DbSet<DownloadLink> DownLoadLinks
        {
            get { return _downLoadLinks; }

            set { _downLoadLinks = value; }
        }

        public DbSet<OperatorResolution> _operatorResolutions;
        public DbSet<OperatorResolution> OperatorResolutions
        {
            get { return _operatorResolutions; }
            set { _operatorResolutions = value; }
        }

        private DbSet<ShapefileRequest> _shapefileRequests;
        public DbSet<ShapefileRequest> ShapefileRequests
        {
            get { return _shapefileRequests; }
            set { _shapefileRequests = value; }
        }

        private DbSet<Layer> _layers;
        public DbSet<Layer> Layers
        {
            get { return _layers; }
            set { _layers = value; }
        }

        private DbSet<Locality> _localities;
        public DbSet<Locality> Localities
        {
            get { return _localities; }
            set { _localities = value; }
        }

   

        public DownloadLink GetDownloadLink(Guid publicId)
        {
            var query = this.DownLoadLinks.Where(dl => dl.PublicId == publicId);
            return query.SingleOrDefault();

        }

        public DownloadLink GetDownloadLinkById(int downloadLinkID)
        {

            var query = this.DownLoadLinks.Where(dl => dl.DownloadLinkId == downloadLinkID);
            return query.SingleOrDefault();

        }

        public ShapefileRequest GetShapefileRequestById(int shapeFileRequestId)
        {

            var query = this.ShapefileRequests.Where(sr => sr.ShapefileRequestId == shapeFileRequestId);
            
            return query.SingleOrDefault();

        }

        public OperatorResolution GetOperatorResolutionById(int operatorResolutionId)
        {

            var query = this.OperatorResolutions.Where(or => or.OperatorResolutionId == operatorResolutionId);
            return query.SingleOrDefault();

        }

        public Layer GetLayerById(int id)
        {
            var query = this.Layers.Where(ia => ia.LayerId == id);
            return query.SingleOrDefault();
        }

        public DownloadLink SaveDownloadLink(DownloadLink downloadLink)
        {

            this.DownLoadLinks.Add(downloadLink);
            this.SaveChanges();
            return downloadLink;

        }

        public OperatorResolution SaveOperatorResolution(OperatorResolution operatorResolution)
        {


            var existingOR = this.OperatorResolutions.FirstOrDefault(or => or.ShapefileRequest.ShapefileRequestId == operatorResolution.ShapefileRequest.ShapefileRequestId);
            if (existingOR == null)
                this.OperatorResolutions.Add(operatorResolution);
            else
            {
                existingOR.Resolution = operatorResolution.Resolution;
                existingOR.ResolutionDetails = operatorResolution.ResolutionDetails;
                existingOR.Timestamp = operatorResolution.Timestamp;
                existingOR.OperatorName = operatorResolution.OperatorName;
            }
            this.SaveChanges();
            return operatorResolution;

        }

        public ShapefileRequest SaveShapefileRequest(ShapefileRequest shapefileRequest)
        {

            this.ShapefileRequests.Add(shapefileRequest);
            this.SaveChanges();
            return shapefileRequest;

        }

        public Layer SaveLayer(Layer layer)
        {
            this.Layers.Add(layer);
            this.SaveChanges();
            return layer;
        }

        public ICollection<Layer> GetAllLayers()
        {
            return Layers.ToList();
        }

        public ICollection<Layer> GetInterestAreasForShapefileRequest(int id)
        {
            var query = this.ShapefileRequests.Where(sr => sr.ShapefileRequestId == id).SingleOrDefault();
            return query.Layers.ToList();
        }

        public ICollection<Locality>GetAllLocalities()
        {
            return Localities.ToList();
        }

        public Locality AddLocality(Locality locality)
        {
            this.Localities.Add(locality);
            try
            {
                this.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Console.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }
            return locality;

        }

    }
}
