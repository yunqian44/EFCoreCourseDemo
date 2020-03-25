using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model
{
    public class Blog :IUpdatedable
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdatedTime { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public Category Categorys { get; set; }

        public List<Post> Posts { get; set; }

        public bool boolConvertChar { get; set; }
    }

    /// <summary>
    /// 分类
    /// </summary>
    public enum Category
    {
        /// <summary>
        /// 技术
        /// </summary>
        [Description("技术")]
        Technology=1,

        /// <summary>
        /// 杂谈
        /// </summary>
        [Description("杂谈")]
        Gossip=2

    }
}
