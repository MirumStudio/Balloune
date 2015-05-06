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
        private IDbConnection mDBConnection;

        protected override void Init()
        {

        }

        protected override void Dispose()
        {

        }

        public bool ExecuteQuery(SQLQuery pQuery)
        {
            lock (mDBConnection)
            {
                return ExecuteQueryThreadSafe(pQuery);
            }
        }

        public bool ExecuteQueryThreadSafe(SQLQuery pQuery)
        {
            try
            {
                OpenConnection(pQuery.GetDatabaseName());
                var data = ExecuteRequest(pQuery.GetQuery());
                ReadResult(data);
                CloseConnection();
            }
            catch(Exception exception)
            {

            }
            return true;
        }

        private void OpenConnection(string pDBName)
        {
            string conn = "URI=file:" + Application.dataPath + "/Databases/" + pDBName + ".db";
            mDBConnection = (IDbConnection)new SqliteConnection(conn);
            mDBConnection.Open();
        }

        private IDataReader ExecuteRequest(string pSQLQuery)
        {
            IDbCommand dbcmd = mDBConnection.CreateCommand();

            string sqlQuery = "SELECT lol, toto " + "FROM TestData";
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();

            dbcmd.Dispose();
            dbcmd = null;

            return reader;
        }

        private void ReadResult(IDataReader pData)
        {
            while (pData.Read())
            {
                int lol = pData.GetInt32(0);
                string toto = pData.GetString(1);
                //Debug.Log("lol = " + value);
            }

            pData.Close();
            pData = null;
        }

        private void CloseConnection()
        {
            mDBConnection.Close();
            mDBConnection = null;
        }
    }
}
