using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Radix.DatabaseManagement
{
    public abstract class SQLInsertQuery : SQLQuery
    {
        private Dictionary<string, string> mData = new Dictionary<string, string>();

        protected void AddData(string pRow, string pValue)
        {
            //Check is exist
            mData.Add(pRow, pValue);
        }

        public override string GetQuery()
        {
            string query = "INSERT INTO ";
            query += TableName;
            query += " (";

            foreach(string key in mData.Keys)
            {
                query += key;
                query += ",";
            }

            query = query.Remove(query.Length - 1);
            query += ") VALUES (";

            foreach (string value in mData.Values)
            {
                query += "'";
                query += value;
                query += "',";
            }
            query = query.Remove(query.Length - 1);
            query += ");";

            return query;
        }
    }
}
