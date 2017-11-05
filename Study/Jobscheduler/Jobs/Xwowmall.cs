using Chloe;
using Chloe.SQLite;
using Common.Logging;
using Jobscheduler.Helper;
using Jobscheduler.Models;
using QD.Framework;
using Quartz;
using SqlliteAccess;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace Jobscheduler.Jobs
{
    /// <summary>
    ///
    /// </summary>
    [DisallowConcurrentExecution]
    public class Xwowmall_Cata_Job : IJob
    {
        private const string catalogurl = "/front/category/all_categories";
        private static HttpHelper _helper = new HttpHelper("http://www.xwowmall.com");

        private SQLiteContext dbcontext;

        public Xwowmall_Cata_Job()
        {
            dbcontext = new SQLiteContext(new SQLiteConnectionFactory("Data Source=" + "dbpath".ValueOfAppSetting() + "\\Chloe.db;Version=3;Pooling=True;Max Pool Size=100;"));
        }

        public void Execute(IJobExecutionContext context)
        {
            string shortdate = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime dt = DateTime.Now;

            List<Xwowmall_Cata> mclst = new List<Xwowmall_Cata>();
            IQuery<Xwowmall_Cata> mcdblst = dbcontext.Query<Xwowmall_Cata>();

            var t = _helper.Get<Xwowmall_Cata_Api>(null, catalogurl);
            t.data.ForEach(x =>
            {
                mclst.Add(new Xwowmall_Cata()
                {
                    cataid = x.id,
                    cateleverl = 1,
                    modify = dt,
                    name = x.name,
                    parentid = "0"
                });
                x.children.ForEach(y =>
                {
                    mclst.Add(new Xwowmall_Cata()
                    {
                        cataid = y.id,
                        cateleverl = 2,
                        modify = dt,
                        name = y.name,
                        parentid = x.id
                    });
                    y.children.ForEach(z =>
                    {
                        mclst.Add(new Xwowmall_Cata()
                        {
                            cataid = z.id,
                            cateleverl = 3,
                            modify = dt,
                            name = z.name,
                            parentid = y.id
                        });
                    });
                });
            });

            if (mclst.Count > 0)
            {
                List<Xwowmall_Cata> dbnow = mcdblst.Where(x => true).ToList();
                if (dbnow.Count==0)
                {
                    dbcontext.InsertRange(mclst);
                }
                else
                {
                    foreach (var item in mclst)
                    {
                        var xc = dbnow.Where(x => x.cataid == item.cataid).FirstOrDefault();
                        if (xc == null)
                        {
                            dbcontext.Insert(item);
                        }
                    }
                }
                
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    [DisallowConcurrentExecution]
    public class Xwowmall_Good_Job : IJob
    {
        private SQLiteContext dbcontext;
   
        public Xwowmall_Good_Job()
        {
            dbcontext = new SQLiteContext(new SQLiteConnectionFactory("Data Source=" + "dbpath".ValueOfAppSetting() + "\\Chloe.db;Version=3;Pooling=True;Max Pool Size=100;"));
        }

        public void Execute(IJobExecutionContext context)
        {
            
            string shortdate = DateTime.Now.ToString("yyyy-MM-dd");

            DateTime dt = DateTime.Now;
          
            IQuery<Xwowmall_Cata> q = dbcontext.Query<Xwowmall_Cata>();

            List<Xwowmall_Cata> mclst = q.Where(x => x.cateleverl == 3).ToList();

            List<Xwowmall_Good> mglst = new List<Xwowmall_Good>();
            IQuery<Xwowmall_Good> qm = dbcontext.Query<Xwowmall_Good>();
           
            HttpWebHelper helper = new HttpWebHelper();
            foreach (var mc in mclst)
            {
                var tt = helper.Post<Xwowmall_Good_Api>("http://www.xwowmall.com/front/solr/search3", string.Format("categoryId={0}&sort=4&currentPage={1}", mc.cataid, 1), Encoding.UTF8, Encoding.UTF8);
                if (tt.totalPages > 0)
                {
                    Console.WriteLine(mc.name);
                    for (int i = 1; i <= tt.totalPages; i++)
                    {
                        Thread.Sleep(500);
                        var gg = helper.Post<Xwowmall_Good_Api>("http://www.xwowmall.com/front/solr/search3", string.Format("categoryId={0}&sort=4&currentPage={1}", mc.cataid, i), Encoding.UTF8, Encoding.UTF8);
                        gg.data.productList.ForEach(x =>
                        {
                            mglst.Add(new Xwowmall_Good()
                            {
                                desc = x.des,
                                imgurl = x.imageUrl,
                                modify = dt,
                                price = x.price,
                                productid = x.id,
                                productname = x.title,
                                stock = x.stock,
                                shortdate = shortdate,
                                cataid = mc.cataid
                            });
                        });
                    }
                }
            }

            if (mglst.Count > 0)
            {
                List<Xwowmall_Good> nowmg = qm.Where(x => x.shortdate == shortdate).ToList();
                if (nowmg.Count == 0)
                {
                    dbcontext.InsertRange(mglst);
                }
                else
                {
                    foreach (var item in mglst)
                    {
                        var ddd = nowmg.Where(x => x.cataid == item.cataid).FirstOrDefault();
                        if (ddd ==null)
                        {
                            dbcontext.Insert(item);
                        }
                        else
                        {
                            int addsails = (ddd.stock - item.stock) > 0 ? (ddd.stock - item.stock) : 0;
                            ddd.price = item.price;
                            ddd.sails = ddd.sails + addsails;
                            ddd.stock = item.stock;
                            
                            dbcontext.Update(ddd);
                        }
                    }
                   
                }
                
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    [DisallowConcurrentExecution]
    public class Xwowmall_Day_Job : IJob
    {
        private SQLiteContext dbcontext;

        public Xwowmall_Day_Job()
        {
            dbcontext = new SQLiteContext(new SQLiteConnectionFactory("Data Source=" + "dbpath".ValueOfAppSetting() + "\\Chloe.db;Version=3;Pooling=True;Max Pool Size=100;"));
        }

        public void Execute(IJobExecutionContext context)
        {
            
            string shortdate = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime dt = DateTime.Now;
     
            //int days = (int)Math.Ceiling((dt - Convert.ToDateTime("2017-09-28")).TotalDays);
            IQuery<Xwowmall_Cata> q = dbcontext.Query<Xwowmall_Cata>();
            List<Xwowmall_Cata> mclst = q.Where(x => true).ToList();
            List<Xwowmall_Cata> thirdmslst = mclst.Where(x => x.cateleverl == 3).ToList();

            IQuery<Xwowmall_Good> qg = dbcontext.Query<Xwowmall_Good>();
            List<Xwowmall_Day_Data> xdlst = new List<Xwowmall_Day_Data>();
            IQuery<Xwowmall_Day_Data> qd = dbcontext.Query<Xwowmall_Day_Data>();

            string dfenxi = dt.ToString("yyyy-MM-dd");
            string dyfenxi = dt.AddDays(-1).ToString("yyyy-MM-dd");

            int isfenxi = qd.Where(x => x.day == dfenxi).Count();

            //Stopwatch stopwatch = new Stopwatch();
            ////第一次计时
            //stopwatch.Start();
            List<Xwowmall_Good> xgdlst = qg.Where(x => x.shortdate == dfenxi).ToList();
            List<Xwowmall_Good> xgydlst = qg.Where(x => x.shortdate == dyfenxi).ToList();
            if (xgdlst.Count > 0 && xgydlst.Count > 0)
            {
                foreach (var item in thirdmslst)
                {
                    List<Xwowmall_Good> xgcatalst = xgdlst.Where(x => x.cataid == item.cataid).ToList();
                    List<Xwowmall_Good> xgydcatalst = xgydlst.Where(x => x.cataid == item.cataid).ToList();
                    Xwowmall_Cata xcsec = mclst.Where(x => x.cataid == item.parentid).FirstOrDefault();
                    Xwowmall_Cata xfirst = mclst.Where(x => x.cataid == xcsec.parentid).FirstOrDefault();
                    int upshelf = xgcatalst.Except(xgydcatalst, new Xwowmall_GoodComparer()).Count();
                    int downshelf = xgydcatalst.Except(xgcatalst, new Xwowmall_GoodComparer()).Count();
                    //var tt1 = xgcatalst.Intersect(xgydcatalst, new Xwowmall_GoodComparer());
                    //var tt2 = xgydcatalst.Intersect(xgcatalst, new Xwowmall_GoodComparer());
                    //int dsails = tt2.Sum(x => x.stock) - tt1.Sum(y => y.stock);
                    //double mongytotal = tt2.Sum(x => x.stock * double.Parse(x.price)) - tt1.Sum(y => y.stock * double.Parse(y.price));
                    int dsails = xgcatalst.Sum(x=>x.sails);
                    double mongytotal = xgcatalst.Sum(x=>x.sails * double.Parse(x.price));
                    xdlst.Add(new Xwowmall_Day_Data()
                    {
                        day = dfenxi,
                        downshelf = downshelf,
                        upshelf = upshelf,
                        firstcataid = xfirst.cataid,
                        firstname = xfirst.name,
                        secondcataid = xcsec.cataid,
                        secondname = xcsec.name,
                        thirdcataid = item.cataid,
                        thirdname = item.name,
                        totalsales = dsails < 0 ? 1 : dsails,
                        totalsalesmoney =mongytotal<0? "0": mongytotal.ToString(),
                        morethanlast = 0,
                        shortdate = shortdate,
                        modify = dt,
                    });
                }
            }
            //stopwatch.Stop();
            //string t1 = stopwatch.ElapsedMilliseconds.ToString();

            if (xdlst.Count > 0)
            {
                List<Xwowmall_Day_Data> xddlst = qd.Where(x => x.day == shortdate).ToList();
                if (xddlst.Count == 0)
                {
                    dbcontext.InsertRange(xdlst);
                }
                else
                {

                    foreach (var item in xddlst)
                    {
                        var ddd = xdlst.Where(x => x.thirdcataid == item.thirdcataid).FirstOrDefault();
                        item.downshelf = ddd.downshelf;
                        item.totalsalesmoney = ddd.totalsalesmoney;
                        item.totalsales = ddd.totalsales;
                        item.upshelf = ddd.upshelf;
                        dbcontext.Update(item);
                    }
                }
            }
        }
    }

    public class Xwowmall_GoodComparer : IEqualityComparer<Xwowmall_Good>
    {
        public bool Equals(Xwowmall_Good x, Xwowmall_Good y)
        {
            if (Object.ReferenceEquals(x, y)) return true;
            return x != null && y != null && x.cataid == y.cataid && x.productid == y.productid;
        }

        public int GetHashCode(Xwowmall_Good obj)
        {
            int hashcataid = obj.cataid.GetHashCode();
            int hashproductid = obj.productid.GetHashCode();
            return hashcataid ^ hashproductid;
        }
    }
}