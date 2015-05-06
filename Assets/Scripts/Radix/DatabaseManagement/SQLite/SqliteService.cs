using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Radix.Service;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;

namespace Radix.DatabaseManagement.Sqlite
{
    public class SqliteService : ServiceBase
    {
        protected override void Init()
        {

        }

        protected override void Dispose()
        {

        }

        void Start()
        {
            string conn = "URI=file:" + Application.dataPath + "/Databases/test.db"; //Path to database.
            IDbConnection dbconn;
            dbconn = (IDbConnection)new SqliteConnection(conn);
            dbconn.Open(); //Open connection to the database.
            IDbCommand dbcmd = dbconn.CreateCommand();

            string sqlQuery = "SELECT lol " + "FROM TestData";
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();

            while (reader.Read())
            {
                int value = reader.GetInt32(0);

                Debug.Log("lol= " + value);
            }

            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            dbconn = null;
        }

        public bool ExecuteQuery(SQLQuery pQuery)
        {
            return true;
        }
    }
}
