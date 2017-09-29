using Quartz;
using System;

namespace Jobscheduler.Jobs
{
    /// <summary>
    ///
    /// </summary>
    [DisallowConcurrentExecution]
    public class Xwowmall_Cata_Job : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}