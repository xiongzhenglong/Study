using Chloe;
using Chloe.SQLite;
using SqlliteAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShenQi.Models;

namespace ShenQi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string shortdate = DateTime.Now.ToString("yyyy-MM-dd");
            SQLiteContext dbcontext = new SQLiteContext(new SQLiteConnectionFactory("Data Source=" + System.AppDomain.CurrentDomain.BaseDirectory + "\\Chloe.db;Version=3;Pooling=True;Max Pool Size=100;"));
            IQuery<Xwowmall_Day_Data> qd = dbcontext.Query<Xwowmall_Day_Data>();
            List<Xwowmall_Day_Data> xddlst = qd.Where(x => true).ToList();
            List<Xwowmall_Day_Data> nowd = xddlst.Where(x => x.day == shortdate).ToList();
            List<Xwowmall_Day_Data> yd = xddlst.Where(x => x.day == DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd")).ToList();
            ViewBag.upshelf = nowd.Sum(x => x.upshelf);
            ViewBag.downshelf = nowd.Sum(x => x.downshelf);
            ViewBag.totalsails = nowd.Sum(x => x.totalsales);
            ViewBag.totalsailsmoney = nowd.Sum(x => double.Parse(x.totalsalesmoney));
            int morethanlast = nowd.Sum(x => x.totalsales) - yd.Sum(x => x.totalsales);
            if (morethanlast < 0)
            {
                ViewBag.morethanlast = "比昨日少：" + (0 - morethanlast).ToString();
            }
            else
            {
                ViewBag.morethanlast = "比昨日多：" + morethanlast.ToString();
            }
            return View();
        }


       

        
      
    }
}