using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace QueueHandler.Collections
{
    class AsyncQueue<T> : IDisposable
    {
        #region -  Fields  -

        private readonly SemaphoreSlim _semaphore;
        private readonly ConcurrentQueue<T> _queue;

        #endregion

        #region -  Constructors  -

        public AsyncQueue()
        {
            _semaphore = new SemaphoreSlim(0);
            _queue = new ConcurrentQueue<T>();
        }

        #endregion

        #region -  Methods  -

        public void Enqueue(T item)
        {
            _queue.Enqueue(item);
            _semaphore.Release();
        }

        public void EnqueueRange(IEnumerable<T> source)
        {
            var n = 0;
            foreach (var item in source)
            {
                _queue.Enqueue(item);
                n++;
            }
            _semaphore.Release(n);
        }

        public async Task<T> DequeueAsync(int millisecondsTimeout, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entered = await _semaphore.WaitAsync(millisecondsTimeout, cancellationToken);
            if (!entered) { throw new TimeoutException(); }
            if (!_queue.TryDequeue(out T item)) { throw new InvalidOperationException("Entered semaphore with empty queue."); }
            return item;
        }

        #endregion

        #region -  IDisposable Support  -

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _semaphore.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
