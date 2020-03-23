using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public interface IUpdatedable
    {
        DateTime UpdatedTime { get; set; }
    }
}
