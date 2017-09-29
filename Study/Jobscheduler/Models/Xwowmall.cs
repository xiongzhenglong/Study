using Chloe.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobscheduler.Models
{
    [Table("xwowmall_cata")]
    public class Xwowmall_Cata
    {
        [Column(IsPrimaryKey = true)]
        [AutoIncrement]
        public int id { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string name { get; set; }

        [Column(DbType = DbType.String)]
        /// <summary>
        /// 分类id
        /// </summary>
        public string cataid { get; set; }

        [Column(DbType = DbType.String)]
        /// <summary>
        /// 父id
        /// </summary>
        public string parentid { get; set; }

        /// <summary>
        /// 分类级别
        /// </summary>
        public int cateleverl { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>       
        public DateTime modify { get; set; }

    }

    [Table("xwowmall_good")]
    public class Xwowmall_Good
    {
        [Column(IsPrimaryKey = true)]
        [AutoIncrement]
        public int id { get; set; }
        /// <summary>
        /// 商品id
        /// </summary>
        public string productid { get; set; }

        /// <summary>
        /// 分类id
        /// </summary>
        public string cataid { get; set; }

        /// <summary>
        /// 商品名
        /// </summary>
        public string productname { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public string price { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public int stock { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public string imgurl { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string desc { get; set; }

        /// <summary>
        /// 短日期
        /// </summary>
        public string shortdate { get; set; }


        /// <summary>
        /// 修改时间
        /// </summary>       
        public DateTime modify { get; set; }
    }
    public class Xwowmall_Good_Api
    {
        public int resCode { get; set; }
        public string resMsg { get; set; }

        public Xwowmall_Data data { get; set; }
        public int pageSize { get; set; }
        public int totalPages { get; set; }
        public int? totalRows { get; set; }
        public int currentPage { get; set; }
        public bool hasNextPage { get; set; }
    }

    public class Xwowmall_Data
    {
        public Xwowmall_Attributelist[] attributeList { get; set; }
        public Xwowmall_Servicelist[] serviceList { get; set; }
        public Xwowmall_Brandlist[] brandList { get; set; }
        public List<Xwowmall_Productlist> productList { get; set; }
    }

    public class Xwowmall_Attributelist
    {
        public string attrId { get; set; }
        public string name { get; set; }
        public Xwowmall_Option[] options { get; set; }
    }

    public class Xwowmall_Option
    {
        public string opId { get; set; }
        public string options { get; set; }
    }

    public class Xwowmall_Servicelist
    {
        public string field { get; set; }
        public string comment { get; set; }
    }

    public class Xwowmall_Brandlist
    {
        public string id { get; set; }
        public string brand_name { get; set; }
    }

    public class Xwowmall_Productlist
    {
        public string id { get; set; }
        public string goodsId { get; set; }
        public string[] productId { get; set; }
        public string title { get; set; }
        public string price { get; set; }
        public string oldPrice { get; set; }
        public object city { get; set; }
        public string storeId { get; set; }
        public string bizType { get; set; }
        public int zy { get; set; }
        public int stock { get; set; }
        public int bonded { get; set; }
        public string imageUrl { get; set; }
        public string[] imagePath { get; set; }
        public string des { get; set; }
        public string promotion { get; set; }
        public string goodRate { get; set; }
    }


    public class Xwowmall_Cata_Api
    {
        public int resCode { get; set; }
        public object resMsg { get; set; }
        public List<Xwowmall_Datum> data { get; set; }
        public int pageSize { get; set; }
        public int totalPages { get; set; }
        public object totalRows { get; set; }
        public int currentPage { get; set; }
        public bool hasNextPage { get; set; }
    }

    public class Xwowmall_Datum
    {
        public string id { get; set; }
        public string name { get; set; }
        public string parentId { get; set; }
        public string imagePath { get; set; }
        public int cateLevel { get; set; }
        public List<Xwowmall_Child> children { get; set; }
        public string canPoint { get; set; }
        public object appApiType { get; set; }
        public object appTitle { get; set; }
        public object appApiParam { get; set; }
        public Xwowmall_Webpromotionvolist[] webPromotionVoList { get; set; }
        public object[] appPromotionVoList { get; set; }
    }

    public class Xwowmall_Child
    {
        public string id { get; set; }
        public string name { get; set; }
        public string parentId { get; set; }
        public string imagePath { get; set; }
        public int cateLevel { get; set; }
        public List<Xwowmall_Child1> children { get; set; }
        public string canPoint { get; set; }
        public object appApiType { get; set; }
        public object appTitle { get; set; }
        public object appApiParam { get; set; }
        public object webPromotionVoList { get; set; }
        public object appPromotionVoList { get; set; }
    }

    public class Xwowmall_Child1
    {
        public string id { get; set; }
        public string name { get; set; }
        public string parentId { get; set; }
        public string imagePath { get; set; }
        public int cateLevel { get; set; }
        public object[] children { get; set; }
        public string canPoint { get; set; }
        public object appApiType { get; set; }
        public object appTitle { get; set; }
        public object appApiParam { get; set; }
        public object webPromotionVoList { get; set; }
        public object appPromotionVoList { get; set; }
    }

    public class Xwowmall_Webpromotionvolist
    {
        public string name { get; set; }
        public object desc { get; set; }
        public string pic { get; set; }
        public string picUrl { get; set; }
        public string goodsUrl { get; set; }
        public int status { get; set; }
        public string appPic { get; set; }
        public string appTitle { get; set; }
        public string appApiType { get; set; }
        public string appApiParam { get; set; }
        public int appStatus { get; set; }
        public string title { get; set; }
        public object appPicUrl { get; set; }
        public int isWebBigPic { get; set; }
        public int isAppBigPic { get; set; }
    }
}
