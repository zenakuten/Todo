using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Data.Interfaces
{
    public interface IContextConfiguration
    {
        Dictionary<string, string> Settings { get; }
    }
}
