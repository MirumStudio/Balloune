using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Radix.DatabaseManagement
{
    public abstract class SQLQuery
    {
        public abstract string GetQuery();//SELECT lol, toto " + "FROM TestData

        public abstract string GetDatabaseName();
    }
}
