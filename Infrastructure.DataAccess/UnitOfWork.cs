using System;
using System.Data.Entity;
using Core.DomainServices;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace Infrastructure.DataAccess
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbContext _context;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }

        public int Save()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx) // verbose failure message
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public async Task<int> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbEntityValidationException dbEx) // verbose failure message
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                throw;
            }
        }

        #region IDisposable

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
