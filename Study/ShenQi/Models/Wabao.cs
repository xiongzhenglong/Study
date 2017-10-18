using Chloe.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShenQi.Models
{
    [Table("wowpower_rd")]
    public class WowPower_RD
    {
        [Column(IsPrimaryKey = true)]
        [AutoIncrement]
        public int id { get; set; }
        /// <summary>
        /// 游戏名称
        /// </summary>
        public string gamename { get; set; }
        /// <summary>
        /// 游戏热度
        /// </summary>
        public double hotscore { get; set; }

        /// <summary>
        /// 押金
        /// </summary>
        public double deposit { get; set; }
        /// <summary>
        /// 周期
        /// </summary>
        public double clicks { get; set; }


        /// <summary>
        /// 奖金
        /// </summary>
        public double reward { get; set; }

        public string shortdate { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>       
        public DateTime modify { get; set; }

    }

    public class WabaoVM
    {
        /// <summary>
        /// 游戏名称
        /// </summary>
        public string gamename { get; set; }
        /// <summary>
        /// 数量 yes
        /// </summary>
        public double hotscore_yes { get; set; }

        /// <summary>
        /// 数量 now
        /// </summary>
        public double hotscore_now { get; set; }

        /// <summary>
        /// 押金
        /// </summary>
        public double deposit { get; set; }
        /// <summary>
        /// 周期
        /// </summary>
        public double clicks { get; set; }

        /// <summary>
        /// 万元日收益
        /// </summary>
        public double wyday { get; set; }


        /// <summary>
        /// 收益
        /// </summary>
        public double reward { get; set; }

        /// <summary>
        /// 增减额 yes
        /// </summary>
        public double zje_yes { get; set; }

        /// <summary>
        /// 增减额 now
        /// </summary>
        public double zje_now { get; set; }

        /// <summary>
        /// 总金额 now
        /// </summary>
        public double total_now { get; set; }


    }

}