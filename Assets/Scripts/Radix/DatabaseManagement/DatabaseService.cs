using Radix.Service;
using System;
using System.Collections.Generic;
//using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace Radix.DatabaseManagement
{
    public class DatabaseService : ServiceBase
    {
        public static DatabaseService Instance
        {
            get
            {
                return ServiceManager.Instance.GetService<DatabaseService>();
            }
        }

        protected override void Init()
        {
        }

        protected override void Dispose()
        {
        }

        public void test()
        {
            //SQLiteConnection.CreateFile("MyDatabase.sqlite");


        }
    }
}
