using System;
using System.Collections.Generic;

namespace Model
{
    public class Blog 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Post> Posts { get; set; }
    }
}
