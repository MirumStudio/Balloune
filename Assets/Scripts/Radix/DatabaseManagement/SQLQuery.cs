using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Radix.DatabaseManagement
{
    public abstract class SQLQuery
    {
        public List<List<object>> Result { get; set; }
        protected string TableName { get; set; }
        protected string mDBName = string.Empty;

        public virtual string GetDatabaseName()
        {
            return mDBName;
        }

        public abstract string GetQuery();
    }
}
