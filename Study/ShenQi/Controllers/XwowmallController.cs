using Chloe;
using Chloe.Entity;
using Chloe.SQLite;
using Framework.MongoDB;
using Newtonsoft.Json;
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
        // GET: Xwowmall
        public ActionResult Pivot()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetPivottable()
        {
            var his = _service.List<VoteHis>(x => true);

            var vblst = _service.List<VoteBook>(x => true);

            DataTable tb = new DataTable();
            tb.Columns.Add(new DataColumn("IP", typeof(string)));
            tb.Columns.Add(new DataColumn("日期", typeof(string)));
            tb.Columns.Add(new DataColumn("作品", typeof(string)));
            tb.Columns.Add(new DataColumn("IP位置", typeof(string)));
            tb.Columns.Add(new DataColumn("投票数", typeof(int)));
            tb.Columns.Add(new DataColumn("分钟", typeof(string)));

            foreach (var t in his)
            {
                var t1 = vblst.Where(x => x.bigbookid == t.bigbookid).FirstOrDefault();
                DateTime dt = Convert.ToDateTime(t.voteTime.ToString());
                if (t1 != null)
                {
                    tb.Rows.Add(t.clientIP, t.date, t1.comicname, t.IPLoc, 1, dt.ToString("HH:mm"));
                }

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