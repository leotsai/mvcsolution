using System;

namespace MvcSolution
{
    public abstract class DisposableBase : IDisposable
    {
        private bool _disposed = false;
        protected abstract void DisposeInternal();

        public virtual void Dispose()
        {
            if (_disposed == false)
            {
                this.DisposeInternal();
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }
        
        protected void MarkDisposed()
        {
            _disposed = true;
        }
    }
}
