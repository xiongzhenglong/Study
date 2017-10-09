using Chloe;
using Chloe.Entity;
using Chloe.SQLite;
using Framework.MongoDB;
using Newtonsoft.Json;
using ShenQi.Excel;
using ShenQi.Models;
using SqlliteAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ShenQi.Controllers
{
   
    public class XwowmallController : Controller
    {
        #region 报表分析
        // GET: Xwowmall
        public ActionResult Pivot()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetPivottable()
        {
            string shortdate = DateTime.Now.ToString("yyyy-MM-dd");
            SQLiteContext dbcontext = new SQLiteContext(new SQLiteConnectionFactory("Data Source=" + System.AppDomain.CurrentDomain.BaseDirectory + "\\Chloe.db;Version=3;Pooling=True;Max Pool Size=100;"));
            IQuery<Xwowmall_Day_Data> qd = dbcontext.Query<Xwowmall_Day_Data>();
            List<Xwowmall_Day_Data> xddlst = qd.Where(x => true).ToList();
            foreach (var item in xddlst)
            {
                string day = item.day;
                var xddy = xddlst.Where(x => x.thirdcataid == item.thirdcataid && x.day == Convert.ToDateTime(day).AddDays(-1).ToString("yyyy-MM-dd")).FirstOrDefault();
                if (xddy != null)
                {
                    item.morethanlast = item.totalsales - xddy.totalsales;
                }
            }


            DataTable tb = new DataTable();
            tb.Columns.Add(new DataColumn("日期", typeof(string)));
            tb.Columns.Add(new DataColumn("一级分类", typeof(string)));
            tb.Columns.Add(new DataColumn("二级分类", typeof(string)));
            tb.Columns.Add(new DataColumn("三级分类", typeof(string)));
            tb.Columns.Add(new DataColumn("上架数量", typeof(int)));
            tb.Columns.Add(new DataColumn("下架数量", typeof(int)));
            tb.Columns.Add(new DataColumn("销售数量", typeof(int)));
            tb.Columns.Add(new DataColumn("对比昨天", typeof(int)));

            foreach (var t in xddlst)
            {


                tb.Rows.Add(t.day, t.firstname, t.secondname, t.thirdname, t.upshelf, t.downshelf, t.totalsales, t.morethanlast);


            }
            StringBuilder sb = new StringBuilder();
            int tmpdataindex = 0;
            sb.Append("[");
            foreach (DataRow dr in tb.Rows)
            {
                sb.Append("{");
                foreach (var c in tb.Columns)
                {
                    sb.Append("\"" + c.ToString() + "\": \"" + dr[tmpdataindex++] + "\",");
                }

                sb.Remove(sb.Length - 1, 1);
                sb.Append("},");
                tmpdataindex = 0;
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");

            return new JsonNetResult()
            {
                Data = JsonConvert.DeserializeObject(sb.ToString()),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }
        #endregion

        public ActionResult DataSearch()
        {
            return View();
        }

        public JsonResult DataSearchAjac()
        {
            SQLiteContext dbcontext = new SQLiteContext(new SQLiteConnectionFactory("Data Source=" + System.AppDomain.CurrentDomain.BaseDirectory + "\\Chloe.db;Version=3;Pooling=True;Max Pool Size=100;"));
            IQuery<Xwowmall_Good> qd = dbcontext.Query<Xwowmall_Good>();
            List<Xwowmall_Good> lst = qd.Where(x => true).ToList();
            IQuery<Xwowmall_Cata> qc = dbcontext.Query<Xwowmall_Cata>();
            List<Xwowmall_Cata> mclst = qc.Where(x => true).ToList();
            XwowmallSearchModel vm = new XwowmallSearchModel();
            vm.code = 0;
            vm.msg = "";
            vm.count = lst.Count;
            vm.data = new List<Xwowmall_Good_Model>();
            foreach (var item in lst)
            {
                var mc3 = mclst.Where(x => x.cataid == item.cataid).FirstOrDefault();
                var mc2 = mclst.Where(x => x.cataid == mc3.parentid).FirstOrDefault();
                var mc1 = mclst.Where(x => x.cataid == mc2.parentid).FirstOrDefault();
                vm.data.Add(new Xwowmall_Good_Model()
                {
                    firstname = mc1.name,
                    secondname =mc2.name,
                    thirdname =mc3.name,
                    imgurl = item.imgurl,
                    price = item.price,
                    productname = item.productname,
                    shortdate = item.shortdate,
                    stock = item.stock
                });
            }
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        public FileResult Export(string start)
        {
            SQLiteContext dbcontext = new SQLiteContext(new SQLiteConnectionFactory("Data Source=" + System.AppDomain.CurrentDomain.BaseDirectory + "\\Chloe.db;Version=3;Pooling=True;Max Pool Size=100;"));
            IQuery<Xwowmall_Good> qd = dbcontext.Query<Xwowmall_Good>();
            List<Xwowmall_Good> lst = new List<Xwowmall_Good>();
            
            if (start!="")
            {
                
                lst = qd.Where(x => x.shortdate==start).ToList();
            }
            else
            {
                lst = qd.Where(x => true).ToList();
            }
            IQuery<Xwowmall_Cata> qc = dbcontext.Query<Xwowmall_Cata>();
            List<Xwowmall_Cata> mclst = qc.Where(x => true).ToList();
           
            var dataex = new List<Xwowmall_Good_Model>();
            foreach (var item in lst)
            {
                var mc3 = mclst.Where(x => x.cataid == item.cataid).FirstOrDefault();
                var mc2 = mclst.Where(x => x.cataid == mc3.parentid).FirstOrDefault();
                var mc1 = mclst.Where(x => x.cataid == mc2.parentid).FirstOrDefault();
                dataex.Add(new Xwowmall_Good_Model()
                {
                    firstname = mc1.name,
                    secondname = mc2.name,
                    thirdname = mc3.name,
                    imgurl = item.imgurl,
                    price = item.price,
                    productname = item.productname,
                    shortdate = item.shortdate,
                    stock = item.stock
                });
            }
            var tt = dataex.GroupBy(o => o.shortdate).Select(o => o.FirstOrDefault()).ToList();
            MemoryStream stream = ExcelHelper.Export(dataex,tt);

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "EPPlusDemo.xlsx");
        }


        public ActionResult ImprotToSqlite()
        {

            //SQLiteContext context = new SQLiteContext(new SQLiteConnectionFactory("Data Source=" + System.AppDomain.CurrentDomain.BaseDirectory + "\\Chloe.db;Version=3;Pooling=True;Max Pool Size=100;"));
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



    public class JsonNetResult : JsonResult
    {
        public JsonNetResult()
        {
            Settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Error
            };
        }

        public JsonSerializerSettings Settings { get; private set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (this.JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("JSON GET is not allowed");

            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(this.ContentType) ? "application/json" : this.ContentType;

            if (this.ContentEncoding != null)
                response.ContentEncoding = this.ContentEncoding;
            if (this.Data == null)
                return;

            var scriptSerializer = JsonSerializer.Create(this.Settings);

            using (var sw = new StringWriter())
            {
                scriptSerializer.Serialize(sw, this.Data);
                response.Write(sw.ToString());
            }
        }
    }


}