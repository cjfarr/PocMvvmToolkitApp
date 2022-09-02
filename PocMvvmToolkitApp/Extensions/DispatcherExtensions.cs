using System;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace PocMvvmToolkitApp.Extensions
{
    /// <summary>
    /// https://stackoverflow.com/a/38135118 
    /// This fixes not being able to await disptacher methods
    /// </summary>
    public static class DispatcherTaskExtensions
    {
        public static async Task<T> RunTaskAsync<T>(this CoreDispatcher dispatcher, CoreDispatcherPriority priority,
            Func<Task<T>> func)
        {
            var taskCompletionSource = new TaskCompletionSource<T>();
            await dispatcher.RunAsync(priority, async () =>
            {
                try
                {
                    taskCompletionSource.SetResult(await func());
                }
                catch (Exception ex)
                {
                    taskCompletionSource.SetException(ex);
                }
            });
            return await taskCompletionSource.Task;
        }

        // There is no TaskCompletionSource<void> so we use a bool that we throw away.
        public static async Task RunTaskAsync(this CoreDispatcher dispatcher, CoreDispatcherPriority priority,
            Func<Task> func) =>
            await RunTaskAsync(dispatcher, priority, async () => { await func(); return false; });
    }
}
