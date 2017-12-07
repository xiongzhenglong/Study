using Chloe;
using Chloe.SQLite;
using Framework.Common.Extension;
using Highsoft.Web.Mvc.Charts;
using ShenQi.Models;
using SqlliteAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShenQi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string shortdate = DateTime.Now.ToString("yyyy-MM-dd");
            SQLiteContext dbcontext = new SQLiteContext(new SQLiteConnectionFactory("Data Source=" + System.AppDomain.CurrentDomain.BaseDirectory + "\\Chloe.db;Version=3;Pooling=True;Max Pool Size=100;"));
            IQuery<Xwowmall_Day_Data> qd = dbcontext.Query<Xwowmall_Day_Data>();
            IQuery<Xwowmall_Cata> q = dbcontext.Query<Xwowmall_Cata>();
            IQuery<Xwowmall_Good> qdd = dbcontext.Query<Xwowmall_Good>();
            List<Xwowmall_Day_Data> xddlst = qd.Where(x => true).ToList();
            List<Xwowmall_Day_Data> nowd = xddlst.Where(x => x.day == shortdate).ToList();
            List<Xwowmall_Day_Data> yd = xddlst.Where(x => x.day == DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd")).ToList();
            ViewBag.upshelf = nowd.Sum(x => x.upshelf);
            ViewBag.downshelf = nowd.Sum(x => x.downshelf);
            ViewBag.totalsails = nowd.Sum(x => x.totalsales);
            ViewBag.totalsailsmoney = nowd.Sum(x => x.totalsalesmoney);
            int morethanlast = nowd.Sum(x => x.totalsales) - yd.Sum(x => x.totalsales);
            if (morethanlast < 0)
            {
                ViewBag.morethanlast = "比昨日少：" + (0 - morethanlast).ToString();
            }
            else
            {
                ViewBag.morethanlast = "比昨日多：" + morethanlast.ToString();
            }
            ViewBag.totalCata = q.Where(x => x.cateleverl== 3).Count();
            ViewBag.totalGoods = qdd.Where(x => x.shortdate == shortdate).Count();

            DateTime dt = DateTime.Now.AddDays(-11); ;
          
          
            List<Xwowmall_Day_Data> xddlst2 = qd.Where(x => x.modify > dt).ToList();

            var t = from p in xddlst2.AsEnumerable() group p by p.shortdate into g select new { g.Key, total = g.Sum(p => p.totalsalesmoney) };
            var t2 = t.ToList();
            List<string> xlst = new List<string>();
            List<LineSeriesData> lst = new List<LineSeriesData>();
            t2.ForEach(x =>
            {
                xlst.Add(x.Key);
                lst.Add(new LineSeriesData()
                {
                    Y=Math.Round(x.total/10000,2)
            });
            });
       


            ViewData["x"] = xlst;
            ViewData["y"] = lst;
            return View();
        
        }


       

        
      
    }
}