using Chloe;
using Chloe.Infrastructure.Interception;
using Chloe.SQLite;
using HtmlAgilityPack;
using Jobscheduler.Helper;
using Jobscheduler.Models;
using QD.Framework;
using Quartz;
using SqlliteAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Jobscheduler.Jobs
{
    class DbCommandInterceptor : IDbCommandInterceptor
    {
        public void ReaderExecuting(IDbCommand command, DbCommandInterceptionContext<IDataReader> interceptionContext)
        {
            //interceptionContext.DataBag.Add("startTime", DateTime.Now);
            Debug.WriteLine(AppendDbCommandInfo(command));
            Console.WriteLine(command.CommandText);
            AmendParameter(command);
        }
        public void ReaderExecuted(IDbCommand command, DbCommandInterceptionContext<IDataReader> interceptionContext)
        {
            //DateTime startTime = (DateTime)(interceptionContext.DataBag["startTime"]);
            //Console.WriteLine(DateTime.Now.Subtract(startTime).TotalMilliseconds);
            if (interceptionContext.Exception == null)
                Console.WriteLine(interceptionContext.Result.FieldCount);
        }

        public void NonQueryExecuting(IDbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            Debug.WriteLine(AppendDbCommandInfo(command));
            Console.WriteLine(command.CommandText);
            AmendParameter(command);
        }
        public void NonQueryExecuted(IDbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            if (interceptionContext.Exception == null)
                Console.WriteLine(interceptionContext.Result);
        }

        public void ScalarExecuting(IDbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            //interceptionContext.DataBag.Add("startTime", DateTime.Now);
            Debug.WriteLine(AppendDbCommandInfo(command));
            Console.WriteLine(command.CommandText);
            AmendParameter(command);
        }
        public void ScalarExecuted(IDbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            //DateTime startTime = (DateTime)(interceptionContext.DataBag["startTime"]);
            //Console.WriteLine(DateTime.Now.Subtract(startTime).TotalMilliseconds);
            if (interceptionContext.Exception == null)
                Console.WriteLine(interceptionContext.Result);
        }


        static void AmendParameter(IDbCommand command)
        {
            //foreach (var parameter in command.Parameters)
            //{
            //    if (parameter is OracleParameter)
            //    {
            //        OracleParameter oracleParameter = (OracleParameter)parameter;
            //        if (oracleParameter.Value is string)
            //        {
            //            /* 针对 oracle 长文本做处理 */
            //            string value = (string)oracleParameter.Value;
            //            if (value != null && value.Length > 4000)
            //            {
            //                oracleParameter.OracleDbType = OracleDbType.NClob;
            //            }
            //        }
            //    }
            //}
        }

        public static string AppendDbCommandInfo(IDbCommand command)
        {
            StringBuilder sb = new StringBuilder();

            foreach (IDbDataParameter param in command.Parameters)
            {
                if (param == null)
                    continue;

                object value = null;
                if (param.Value == null || param.Value == DBNull.Value)
                {
                    value = "NULL";
                }
                else
                {
                    value = param.Value;

                    if (param.DbType == DbType.String || param.DbType == DbType.AnsiString || param.DbType == DbType.DateTime)
                        value = "'" + value + "'";
                }

                sb.AppendFormat("{3} {0} {1} = {2};", Enum.GetName(typeof(DbType), param.DbType), param.ParameterName, value, Enum.GetName(typeof(ParameterDirection), param.Direction));
                sb.AppendLine();
            }

            sb.AppendLine(command.CommandText);

            return sb.ToString();
        }
    }
    /// <summary>
    ///
    /// </summary>
    [DisallowConcurrentExecution]
    public class WowPower_RD_Job : IJob
    {
        private const string indexRecommendGame = "/indexRecommendGame";
        private const string indexGame = "/indexGame";
        private static HttpHelper _helper = new HttpHelper("http://www.wowpower.com");

        private SQLiteContext dbcontext;

        public WowPower_RD_Job()
        {
            dbcontext = new SQLiteContext(new SQLiteConnectionFactory("Data Source=" + "dbpath".ValueOfAppSetting() + "\\Chloe.db;Version=3;Pooling=True;Max Pool Size=100;"));
        }
        public void Execute(IJobExecutionContext context)
        {
            IDbCommandInterceptor interceptor = new DbCommandInterceptor();
            dbcontext.Session.AddInterceptor(interceptor);

            string shortdate = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime dt = DateTime.Now;
            // indexGame
            var t1 = _helper.Get<WowPower_RD_Api>(null, "/indexGame?sort=clicks&sortseq=down");
            var t2 = _helper.Get<WowPower_RD_Api>(null, "/indexGame?sort=deposit&sortseq=down");
            var t3 = _helper.Get<WowPower_RD_Api>(null, "/indexGame?sort=reward&sortseq=down");
            var t4 = _helper.Get<WowPower_RD_Api>(null, "/indexGame?sort=looks&sortseq=down");
            // indexRecommendGame
            var t5 = _helper.Get<WowPower_RD_Api>(null, "/indexRecommendGame?sort=clicks&sortseq=down");
            var t6 = _helper.Get<WowPower_RD_Api>(null, "/indexRecommendGame?sort=deposit&sortseq=down");
            var t7 = _helper.Get<WowPower_RD_Api>(null, "/indexRecommendGame?sort=reward&sortseq=down");
            var t8 = _helper.Get<WowPower_RD_Api>(null, "/indexRecommendGame?sort=looks&sortseq=down");
            List<WowPower_RD> wrlst = new List<WowPower_RD>();
            IQuery<WowPower_RD> qm = dbcontext.Query<WowPower_RD>();
            t1.data.ForEach(x =>
            {
                wrlst.Add(new WowPower_RD()
                {
                    clicks = x.clicks,
                    deposit = x.deposit,
                    gamename = x.gameName,
                    hotscore = x.hotScore,
                    reward = x.reward,
                    modify = dt,
                    shortdate = shortdate
                });
            });
            t2.data.ForEach(x =>
            {
                wrlst.Add(new WowPower_RD()
                {
                    clicks = x.clicks,
                    deposit = x.deposit,
                    gamename = x.gameName,
                    hotscore = x.hotScore,
                    reward = x.reward,
                    modify = dt,
                    shortdate = shortdate
                });
            });
            t3.data.ForEach(x =>
            {
                wrlst.Add(new WowPower_RD()
                {
                    clicks = x.clicks,
                    deposit = x.deposit,
                    gamename = x.gameName,
                    hotscore = x.hotScore,
                    reward = x.reward,
                    modify = dt,
                    shortdate = shortdate
                });
            });
            t4.data.ForEach(x =>
            {
                wrlst.Add(new WowPower_RD()
                {
                    clicks = x.clicks,
                    deposit = x.deposit,
                    gamename = x.gameName,
                    hotscore = x.hotScore,
                    reward = x.reward,
                    modify = dt,
                    shortdate = shortdate
                });
            });
            t5.data.ForEach(x =>
            {
                wrlst.Add(new WowPower_RD()
                {
                    clicks = x.clicks,
                    deposit = x.deposit,
                    gamename = x.gameName,
                    hotscore = x.hotScore,
                    reward = x.reward,
                    modify = dt,
                    shortdate = shortdate
                });
            });
            t6.data.ForEach(x =>
            {
                wrlst.Add(new WowPower_RD()
                {
                    clicks = x.clicks,
                    deposit = x.deposit,
                    gamename = x.gameName,
                    hotscore = x.hotScore,
                    reward = x.reward,
                    modify = dt,
                    shortdate = shortdate
                });
            });
            t7.data.ForEach(x =>
            {
                wrlst.Add(new WowPower_RD()
                {
                    clicks = x.clicks,
                    deposit = x.deposit,
                    gamename = x.gameName,
                    hotscore = x.hotScore,
                    reward = x.reward,
                    modify = dt,
                    shortdate = shortdate
                });
            });
            t8.data.ForEach(x =>
            {
                wrlst.Add(new WowPower_RD()
                {
                    clicks = x.clicks,
                    deposit = x.deposit,
                    gamename = x.gameName,
                    hotscore = x.hotScore,
                    reward = x.reward,
                    modify = dt,
                    shortdate = shortdate
                });
            });
            wrlst = wrlst.GroupBy(o => o.gamename).Select(o => o.FirstOrDefault()).ToList();
         
            if (wrlst.Count > 0)
            {
                List<WowPower_RD> nowmg = qm.Where(x => x.shortdate == shortdate).ToList();
                if (nowmg.Count == 0)
                {
                   
                    dbcontext.InsertRange(wrlst);
                }
                else
                {
                    foreach (var item in wrlst)
                    {
                        var ddd = nowmg.Where(x => x.gamename == item.gamename).FirstOrDefault();
                        if (ddd == null)
                        {
                            dbcontext.Insert(item);
                        }
                        else
                        {
                            ddd.clicks = item.clicks;
                            ddd.deposit = item.deposit;
                            ddd.hotscore = item.hotscore;
                            ddd.reward = item.reward;
                            dbcontext.Update(ddd);
                        }
                    }

                }

            }
        }
    }
}
