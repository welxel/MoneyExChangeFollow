using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyExChangeFollowAPI.Extentions
{
    public static class Utils
    {
        public static Task<T> Retry<T>(Func<T> func, int retryCount, int delay, TaskCompletionSource<T> tcs = null)
        {
            if (tcs == null)
                tcs = new TaskCompletionSource<T>();
            Task.Factory.StartNew(func).ContinueWith(_original =>
            {
            if (_original.IsFaulted)
            {
                    if (retryCount == 0)
                        tcs.SetException(_original.Exception.InnerExceptions);
                    else
                        Task.Factory.StartNew(() => { Thread.Sleep(delay); Retry(func, retryCount - 1, delay, tcs); });
                }
                else
                    tcs.SetResult(_original.Result);
            });
            return tcs.Task;
        }
    }
}
