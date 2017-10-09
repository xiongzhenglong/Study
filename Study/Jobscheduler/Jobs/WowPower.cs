using Chloe;
using Chloe.SQLite;
using Jobscheduler.Helper;
using Jobscheduler.Models;
using QD.Framework;
using Quartz;
using SqlliteAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobscheduler.Jobs
{
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
            string shortdate = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime dt = DateTime.Now;

            var t = _helper.Get<WowPower_RD_Api>(null, indexGame);
            var t2 = _helper.Get<WowPower_RD_Api>(null, indexRecommendGame);
            List<WowPower_RD> wrlst = new List<WowPower_RD>();
            IQuery<WowPower_RD> qm = dbcontext.Query<WowPower_RD>();
            t.data.ForEach(x =>
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
                            dbcontext.Update(item);
                        }
                    }

                }

            }
        }
    }
}
