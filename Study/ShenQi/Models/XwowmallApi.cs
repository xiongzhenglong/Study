using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShenQi.Models
{
    public class Mall_Good_Api
    {
        public int resCode { get; set; }
        public string resMsg { get; set; }
        public Data data { get; set; }
        public int pageSize { get; set; }
        public int totalPages { get; set; }
        public int? totalRows { get; set; }
        public int currentPage { get; set; }
        public bool hasNextPage { get; set; }
    }

    public class Data
    {
        public Attributelist[] attributeList { get; set; }
        public Servicelist[] serviceList { get; set; }
        public Brandlist[] brandList { get; set; }
        public List<Productlist> productList { get; set; }
    }

    public class Attributelist
    {
        public string attrId { get; set; }
        public string name { get; set; }
        public Option[] options { get; set; }
    }

    public class Option
    {
        public string opId { get; set; }
        public string options { get; set; }
    }

    public class Servicelist
    {
        public string field { get; set; }
        public string comment { get; set; }
    }

    public class Brandlist
    {
        public string id { get; set; }
        public string brand_name { get; set; }
    }

    public class Productlist
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


    public class Mall_Cata_Api
    {
        public int resCode { get; set; }
        public object resMsg { get; set; }
        public List<Datum> data { get; set; }
        public int pageSize { get; set; }
        public int totalPages { get; set; }
        public object totalRows { get; set; }
        public int currentPage { get; set; }
        public bool hasNextPage { get; set; }
    }

    public class Datum
    {
        public string id { get; set; }
        public string name { get; set; }
        public string parentId { get; set; }
        public string imagePath { get; set; }
        public int cateLevel { get; set; }
        public List<Child> children { get; set; }
        public string canPoint { get; set; }
        public object appApiType { get; set; }
        public object appTitle { get; set; }
        public object appApiParam { get; set; }
        public Webpromotionvolist[] webPromotionVoList { get; set; }
        public object[] appPromotionVoList { get; set; }
    }

    public class Child
    {
        public string id { get; set; }
        public string name { get; set; }
        public string parentId { get; set; }
        public string imagePath { get; set; }
        public int cateLevel { get; set; }
        public List<Child1> children { get; set; }
        public string canPoint { get; set; }
        public object appApiType { get; set; }
        public object appTitle { get; set; }
        public object appApiParam { get; set; }
        public object webPromotionVoList { get; set; }
        public object appPromotionVoList { get; set; }
    }

    public class Child1
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

    public class Webpromotionvolist
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