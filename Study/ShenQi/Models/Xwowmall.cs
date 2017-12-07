using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chloe.Entity;


namespace ShenQi.Models
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

        /// <summary>
        /// 分类id
        /// </summary>
        public string cataid { get; set; }

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

    [Table("xwowmall_day_data")]
    public class Xwowmall_Day_Data
    {
        [Column(IsPrimaryKey = true)]
        [AutoIncrement]
        public int id { get; set; }

        /// <summary>
        /// 分类id
        /// </summary>
        public string firstcataid { get; set; }

        public string firstname { get; set; }

        public double totalsalesmoney { get; set; }


        /// <summary>
        /// 分类id
        /// </summary>
        public string secondcataid { get; set; }

        public string secondname { get; set; }
        /// <summary>
        /// 分类id
        /// </summary>
        public string thirdcataid { get; set; }

        public string thirdname { get; set; }


        /// <summary>
        /// 日期 天为单位
        /// </summary>
        public string day { get; set; }

        /// <summary>
        /// 上架
        /// </summary>
        public int upshelf { get; set; }

        /// <summary>
        /// 下架
        /// </summary>
        public int downshelf { get; set; }

        /// <summary>
        /// 销量
        /// </summary>
        public int totalsales { get; set; }

        /// <summary>
        /// 比昨日多出销量
        /// </summary>
        public int morethanlast { get; set; }

        /// <summary>
        /// 短日期
        /// </summary>
        public string shortdate { get; set; }


        /// <summary>
        /// 修改时间
        /// </summary>       
        public DateTime modify { get; set; }
    }

    public class XwowmallSearchModel
    {
        public int code { get; set; }

        public string msg { get; set; }

        public int count { get; set; }

        public List<Xwowmall_Good_Model> data { get; set; }
    }

    public class Xwowmall_Good_Model
    {
        /// <summary>
        /// 一级分类
        /// </summary>
        public string firstname { get; set; }
        /// <summary>
        /// 二级分类
        /// </summary>
        public string secondname { get; set; }

        /// <summary>
        /// 三级分类
        /// </summary>
        public string thirdname { get; set; }

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

        public string shortdate { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public string imgurl { get; set; }
    }
}