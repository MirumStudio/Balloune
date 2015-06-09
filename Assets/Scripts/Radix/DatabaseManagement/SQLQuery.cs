/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.DatabaseManagement.Sqlite;
using Radix.Service;
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


        public virtual void Execute()
        {
            ServiceManager.Instance.GetService<SqliteService>().ExecuteQuery(this);
        }
    }
}
