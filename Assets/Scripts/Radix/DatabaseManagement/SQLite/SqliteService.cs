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
        private System.Object mLock = new System.Object();
        private IDbConnection mDBConnection;

        protected override void Init()
        {

        }

        protected override void Dispose()
        {

        }

        public void ExecuteQuery(SQLQuery pQuery)
        {
            lock (mLock)
            {
                ExecuteQueryThreadSafe(pQuery);
            }
        }

        public void ExecuteQueryThreadSafe(SQLQuery pQuery)
        {
            try
            {
                OpenConnection(pQuery.GetDatabaseName());
                var data = ExecuteRequest(pQuery.GetQuery());
                pQuery.Result = ReadResult(data);
                CloseConnection();
            }
            catch(Exception exception)
            {
                int y = 6;
                //error
            }
        }

        private void OpenConnection(string pDBName)
        {
            string conn = "URI=file:" + Application.dataPath + "/Databases/" + pDBName + ".db";
            mDBConnection = (IDbConnection)new SqliteConnection(conn);
            mDBConnection.Open();
        }

        private IDataReader ExecuteRequest(string pSQLQuery)
        {
            IDbCommand command = mDBConnection.CreateCommand();

            command.CommandText = pSQLQuery;
            IDataReader reader = command.ExecuteReader();

            command.Dispose();
            command = null;

            return reader;
        }

        private List<List<object>> ReadResult(IDataReader pData)
        {
            List<List<object>> result = new List<List<object>>();

            while (pData.Read())
            {
                List<object> currentResult = new List<object>();
                int count = pData.FieldCount;
                for(int i = 0; i < count;i++)
                {
                    currentResult.Add(pData.GetValue(i));
                }

                result.Add(currentResult);
            }

            pData.Close();
            pData = null;

            return result;
        }

        private void CloseConnection()
        {
            mDBConnection.Close();
            mDBConnection = null;
        }

        public void Test()
        {
            string conn = "URI=file:" + Application.dataPath + "/Databases/lol.db";
            mDBConnection = (IDbConnection)new SqliteConnection(conn);
            mDBConnection.Open();

            mDBConnection.Close();
            mDBConnection = null;
        }
    }
}
