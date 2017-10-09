using Chloe.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobscheduler.Models
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
        /// 
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

   


    public class WowPower_RD_Api
    {
        public int resCode { get; set; }
        public object resMsg { get; set; }
        public List<WowPower_Data> data { get; set; }
        public int totalPages { get; set; }
        public int totalRows { get; set; }
        public int currentPage { get; set; }
        public object extra { get; set; }
    }

    public class WowPower_Data
    {
        public string id { get; set; }
        public long createTime { get; set; }
        public string createBy { get; set; }
        public object modifyTime { get; set; }
        public object modifyBy { get; set; }
        public object orderBy { get; set; }
        public string type { get; set; }
        public string language { get; set; }
        public string gameDescrip { get; set; }
        public string gameUrl { get; set; }
        public int playCounts { get; set; }
        public int category { get; set; }
        public object gamePicUrl { get; set; }
        /// <summary>
        /// 游戏名
        /// </summary>
        public string gameName { get; set; }
        public int clickNum { get; set; }
        public int isMember { get; set; }
        /// <summary>
        /// 游戏热度
        /// </summary>
        public double hotScore { get; set; }
        public object square { get; set; }
        public object rectangle { get; set; }
        /// <summary>
        /// 
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
        public int punish { get; set; }
        public int players { get; set; }
        public string typeName { get; set; }
        public string platform { get; set; }
        public string gameSize { get; set; }
        public string gameUpdateTime { get; set; }
        public string gameSource { get; set; }
        public string iosDownloadUrl { get; set; }
        public string androidDownloadUrl { get; set; }
        public object tencentDownloadUrl { get; set; }
        public string gameCoverPic { get; set; }
        public string gameScreenshotsPic { get; set; }
        public string gameQrcodePic { get; set; }
        public string gameCoverPicPath { get; set; }
        public object gameScreenshotsPicPath { get; set; }
        public object gameQrcodePicPath { get; set; }
        public int status { get; set; }
        public string activityType { get; set; }
        public long? startTime { get; set; }
        public long? endTime { get; set; }
    }

}
