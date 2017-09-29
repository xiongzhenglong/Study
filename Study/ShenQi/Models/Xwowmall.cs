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
}