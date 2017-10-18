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
    public class WabaoController : Controller
    {
        // GET: Wabao
        public ActionResult DataSearch(string enddate)
        {
            string shortdate = DateTime.Now.ToString("yyyy-MM-dd");
            if (!enddate.IsNull())
            {
                shortdate = enddate;
            }
         
            DateTime dt = Convert.ToDateTime(shortdate).AddDays(-2);
            DateTime dt2 = Convert.ToDateTime(shortdate).AddDays(+1);
            SQLiteContext dbcontext = new SQLiteContext(new SQLiteConnectionFactory("Data Source=" + System.AppDomain.CurrentDomain.BaseDirectory + "\\Chloe.db;Version=3;Pooling=True;Max Pool Size=100;"));
            IQuery<WowPower_RD> qd = dbcontext.Query<WowPower_RD>();
            List<WowPower_RD> xddlst = qd.Where(x => x.modify > dt && x.modify < dt2).ToList();
            List<WowPower_RD> now = xddlst.Where(x => x.shortdate == shortdate).ToList();
            List<WowPower_RD> yes = xddlst.Where(x => x.shortdate == Convert.ToDateTime(shortdate).AddDays(-1).ToString("yyyy-MM-dd")).ToList();
            List<WowPower_RD> last = xddlst.Where(x => x.shortdate == Convert.ToDateTime(shortdate).AddDays(-2).ToString("yyyy-MM-dd")).ToList();
            List<WabaoVM> vmlst = new List<WabaoVM>();
            foreach (var item in now)
            {
                var iy = yes.Where(x => x.gamename == item.gamename).ToList();
                var il = last.Where(x => x.gamename == item.gamename).ToList();
                double total_now = item.deposit * item.hotscore;
                double total_yes = iy.Count > 0 ? iy.FirstOrDefault().deposit * iy.FirstOrDefault().hotscore : 0;
                double total_last = il.Count > 0 ? il.FirstOrDefault().deposit * il.FirstOrDefault().hotscore : 0;
                double hotscore_yes = iy.Count > 0 ? iy.FirstOrDefault().hotscore : 0;

                vmlst.Add(new WabaoVM()
                {
                    gamename = item.gamename,
                    clicks = item.clicks,
                    deposit = item.deposit,
                    hotscore_now = item.hotscore,
                    hotscore_yes = iy.FirstOrDefault().hotscore,
                    reward = item.reward,
                    wyday = item.reward/item.clicks/item.deposit *10000,
                    total_now = total_now,
                    zje_now = total_now - total_yes,
                    zje_yes = total_yes - total_last

                });
            
            }
            ViewBag.zje_yes_total = vmlst.Sum(x => x.zje_yes);
            ViewBag.zje_now_total = vmlst.Sum(x => x.zje_now);
            ViewBag.now_total = vmlst.Sum(x => x.total_now);
            ViewBag.vmlst = vmlst;
            ViewBag.Date = shortdate;
            return View();
        }

        public ActionResult Pivot()
        {
           
         
            string shortdate = DateTime.Now.ToString("yyyy-MM-dd");
            SQLiteContext dbcontext = new SQLiteContext(new SQLiteConnectionFactory("Data Source=" + System.AppDomain.CurrentDomain.BaseDirectory + "\\Chloe.db;Version=3;Pooling=True;Max Pool Size=100;"));
            IQuery<WowPower_RD> qd = dbcontext.Query<WowPower_RD>();
            List<WowPower_RD> xddlst = qd.Where(x=>true).ToList();
           
            var t = from p in xddlst.AsEnumerable() group p by p.shortdate into g select new { g.Key, total = g.Sum(p => p.deposit * p.hotscore) };
            var t2 = t.ToList();
            List<string> xlst = new List<string>();
            List<LineSeriesData> lst = new List<LineSeriesData>();
            for (int i = 1; i < t2.Count; i++)
            {
                xlst.Add(t2[i].Key);
                lst.Add(new LineSeriesData()
                {
                    Y=(t2[i].total - t2[i-1].total)/100000000
                });
            }
            
         
            ViewData["x"] = xlst;
            ViewData["y"] = lst;
            return View();
        }
    }
}