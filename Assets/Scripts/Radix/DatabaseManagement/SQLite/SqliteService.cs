using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Radix.Service;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using Radix.ErrorMangement;

namespace Radix.DatabaseManagement.Sqlite
{
    public class SqliteService : ServiceBase
    {
        private System.Object mLock = new System.Object();
        private IDbConnection mDBConnection;

        protected override void Init()
        {}

        protected override void Dispose()
        {
            CloseConnection();
        }

        internal void ExecuteQuery(SQLQuery pQuery)
        {
            lock (mLock)
            {
                ExecuteQueryThreadSafe(pQuery);
            }
        }

        private void ExecuteQueryThreadSafe(SQLQuery pQuery)
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
                Error.Create(exception.Message, EErrorSeverity.MAJOR);
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

            DisposeDbCommand(command);

            return reader;
        }

        private void DisposeDbCommand(IDbCommand pCommand)
        {
            pCommand.Dispose();
            pCommand = null;
        }

        private List<List<object>> ReadResult(IDataReader pData)
        {
            List<List<object>> result = new List<List<object>>();

            while (pData.Read())
            {
                result.Add(ReadRow(pData));
            }

            DisposeDataReader(pData);

            return result;
        }

        private List<object> ReadRow(IDataReader pData)
        {
            List<object> currentResult = new List<object>();
            int count = pData.FieldCount;
            for (int i = 0; i < count; i++)
            {
                currentResult.Add(pData.GetValue(i));
            }

            return currentResult;
        }

        private void DisposeDataReader(IDataReader pData)
        {
            pData.Close();
            pData = null;
        }

        private void CloseConnection()
        {
            if (mDBConnection != null)
            {
                mDBConnection.Close();
                mDBConnection = null;
            }
        }
    }
}
