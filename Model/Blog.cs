using System;
using System.Collections.Generic;

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

        public List<Post> Posts { get; set; }
    }
}
