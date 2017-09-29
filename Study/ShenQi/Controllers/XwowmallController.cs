using Chloe;
using Chloe.Entity;
using Chloe.SQLite;
using Framework.MongoDB;
using ShenQi.Models;
using SqlliteAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShenQi.Controllers
{
    public class XwowmallController : Controller
    {
        // GET: Xwowmall
        public ActionResult Pivot()
        {
            return View();
        }

        public ActionResult ImprotToSqlite()
        {
            
            //SQLiteContext context = new SQLiteContext(new SQLiteConnectionFactory("Data Source="+ System.AppDomain.CurrentDomain.BaseDirectory + "\\Chloe.db;Version=3;Pooling=True;Max Pool Size=100;"));
            //MongoDbRepository _rep = new MongoDbRepository();
            //List<Mall_Cata> mclst = _rep.ToList<Mall_Cata>(x => true);
            //List<Xwowmall_Cata> xclst = new List<Xwowmall_Cata>();
            //mclst.ForEach(x =>
            //{
            //    xclst.Add(new Xwowmall_Cata()
            //    {
            //        cataid = x.cataid,
            //        cateleverl = x.cateleverl,
            //        modify = x.modify,
            //        name = x.name,
            //        parentid = x.parentid
            //    });
            //});       

            //context.InsertRange(xclst);

            //List<Mall_Good> mglst = _rep.ToList<Mall_Good>(x => true);
            //List<Xwowmall_Good> xglst = new List<Xwowmall_Good>();
            //mglst.ForEach(x =>
            //{
            //    xglst.Add(new Xwowmall_Good()
            //    {
            //        cataid = x.cataid,
            //        desc = x.desc,
            //        imgurl = x.imgurl,
            //        modify = x.modify,
            //        price = x.price,
            //        productid = x.productid,
            //        productname = x.productname,
            //        shortdate = x.shortdate,
            //        stock = x.stock
            //    });
            //});
            //context.InsertRange(xglst);
            return null;
        }
    }

    
}