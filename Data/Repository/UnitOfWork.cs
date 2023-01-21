using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PeymanAkhtari.Models;

namespace PeymanAkhtari.Data.Repository
{
    public class UnitOfWork : IDisposable
    {
        private readonly AppDbContext context;
        public UnitOfWork(AppDbContext _appDbContext)
        {
            context = _appDbContext;
        }
        private GenericRepository<Project> _ProjectRepository;
        private GenericRepository<Feature> _FeactureRepository;
        private GenericRepository<SiteSetting> _SiteSettingRepository;
        private GenericRepository<Content> _ContentRepository;


        private bool disposed = false;
        public GenericRepository<Content> ContentRepository
        {
            get
            {
                if (this._ContentRepository == null)
                {
                    this._ContentRepository = new GenericRepository<Content>(context);
                }
                return _ContentRepository;
            }
        }
        public GenericRepository<SiteSetting> SiteSettingRepository
        {
            get
            {
                if (this._SiteSettingRepository == null)
                {
                    this._SiteSettingRepository = new GenericRepository<SiteSetting>(context);
                }
                return _SiteSettingRepository;
            }
        }
        public GenericRepository<Project> ProjectRepository
        {
            get
            {
                if (this._ProjectRepository == null)
                {
                    this._ProjectRepository = new GenericRepository<Project>(context);
                }
                return _ProjectRepository;
            }
        }
        public GenericRepository<Feature> FeactureRepository
        {
            get
            {
                if (this._FeactureRepository == null)
                {
                    this._FeactureRepository = new GenericRepository<Feature>(context);
                }
                return _FeactureRepository;
            }
        }
        public void Save()
        {
            context.SaveChanges();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
