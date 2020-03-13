using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Link10
{
    internal static class TaskExtensionMethods
    {
        /// <summary>
        /// Snippet to get Task result from task without knowing the type.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        /// <remarks>
        /// From : https://stackoverflow.com/questions/22109246/get-result-of-taskt-without-knowing-typeof-t
        /// </remarks>
        public static async Task<object> AsObjectTaskResult(this Task task)
        {
            await task;
            var voidTaskType = typeof(Task<>).MakeGenericType(Type.GetType("System.Threading.Tasks.VoidTaskResult"));
            if (voidTaskType.IsAssignableFrom(task.GetType()))
                throw new InvalidOperationException("Task does not have a return value (" + task.GetType().ToString() + ")");
            var property = task.GetType().GetProperty("Result", BindingFlags.Public | BindingFlags.Instance);
            if (property == null)
                throw new InvalidOperationException("Task does not have a return value (" + task.GetType().ToString() + ")");
            return property.GetValue(task);
        }
    }
}
