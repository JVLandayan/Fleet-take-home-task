using Fleet.context;

namespace Fleet.uow
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();
    }
    public class UnitOfWork: IUnitOfWork
    {
        
        private FleetContext Context { get; }
        public UnitOfWork(FleetContext context)
        {
            Context = context;
        }

        public int Commit()
        {
            return Context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Context != null)
                {
                    Context.Dispose();
                }
            }
        }
    }
}