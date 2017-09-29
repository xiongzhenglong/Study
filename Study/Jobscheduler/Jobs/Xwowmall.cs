using Chloe.SQLite;
using Jobscheduler.Helper;
using Jobscheduler.Models;
using Quartz;
using System;
using System.Collections.Generic;
using SqlliteAccess;
using Chloe;
using QD.Framework;
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
        static HttpHelper _helper = new HttpHelper("http://www.xwowmall.com");

        SQLiteContext dbcontext;
        public Xwowmall_Cata_Job()
        {
            dbcontext = new SQLiteContext(new SQLiteConnectionFactory("Data Source=" + "dbpath".ValueOfAppSetting() + "\\Chloe.db;Version=3;Pooling=True;Max Pool Size=100;"));
        }

        public void Execute(IJobExecutionContext context)
        {
            string shortdate = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime dt = DateTime.Now;
           
          
            List<Xwowmall_Cata> mclst = new List<Xwowmall_Cata>();

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
                dbcontext.InsertRange(mclst);
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    [DisallowConcurrentExecution]
    public class Xwowmall_Good_Job : IJob
    {
        SQLiteContext dbcontext;
        public Xwowmall_Good_Job()
        {
            dbcontext = new SQLiteContext(new SQLiteConnectionFactory("Data Source=" + "dbpath".ValueOfAppSetting() + "\\Chloe.db;Version=3;Pooling=True;Max Pool Size=100;"));
        }
       
        public void Execute(IJobExecutionContext context)
        {
            string shortdate = "2017-09-29";

            DateTime dt = DateTime.Now;

            IQuery<Xwowmall_Cata> q = dbcontext.Query<Xwowmall_Cata>();

            List<Xwowmall_Cata> mclst = q.Where(x => x.cateleverl == 3).ToList();

            List<Xwowmall_Good> mglst = new List<Xwowmall_Good>();
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
                dbcontext.InsertRange(mglst);
            }





        }
    }
}