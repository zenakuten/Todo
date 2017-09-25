using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Data.Context
{
    using Todo.Data.Interfaces;
    public class ContextConfiguration : IContextConfiguration
    {
        public ContextConfiguration()
        {
            _settings = new Dictionary<string, string>();
        }

        private Dictionary<string, string> _settings;
        public Dictionary<string, string> Settings => _settings;
    }
}
