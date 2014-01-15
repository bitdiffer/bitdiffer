using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BitDiffer.Common.Utility
{
	/// <summary>
	/// ThreadPoolWait: http://msdn.microsoft.com/msdnmag/issues/04/10/NETMatters/
	/// </summary>
	public class ThreadPoolWait : IDisposable
	{
		private int _remainingWorkItems = 1;
		private ManualResetEvent _done = new ManualResetEvent(false);

		public void QueueUserWorkItem(WaitCallback callback)
		{
			QueueUserWorkItem(callback, null);
		}

		public void QueueUserWorkItem(WaitCallback callback, object state)
		{
			ThrowIfDisposed();
			QueuedCallback qc = new QueuedCallback();
			qc.Callback = callback;
			qc.State = state;
			Interlocked.Increment(ref _remainingWorkItems);
			ThreadPool.QueueUserWorkItem(
				new WaitCallback(HandleWorkItem), qc);
		}

		public bool WaitOne() { return WaitOne(-1, false); }

		public bool WaitOne(TimeSpan timeout, bool exitContext)
		{
			return WaitOne((int)timeout.TotalMilliseconds, exitContext);
		}

		public bool WaitOne(int millisecondsTimeout, bool exitContext)
		{
			ThrowIfDisposed();
			DoneWorkItem(); // to offset initial value of _remainingWorkItems
			bool rv = _done.WaitOne(millisecondsTimeout, exitContext);
			if (rv)
			{
				_remainingWorkItems = 1;
				_done.Reset();
			}
			else Interlocked.Increment(ref _remainingWorkItems);
			return rv;
		}

		private void HandleWorkItem(object state)
		{
			QueuedCallback qc = (QueuedCallback)state;
			try { qc.Callback(qc.State); }
			finally { DoneWorkItem(); }
		}

		private void DoneWorkItem()
		{
			if (Interlocked.Decrement(ref _remainingWorkItems) == 0)
				_done.Set();
		}

		private class QueuedCallback
		{
			public WaitCallback Callback;
			public object State;
		}

		private void ThrowIfDisposed()
		{
			if (_done == null)
				throw new ObjectDisposedException(GetType().Name);
		}

		public void Dispose()
		{
			if (_done != null)
			{
				((IDisposable)_done).Dispose();
				_done = null;
			}
		}
	}

}
