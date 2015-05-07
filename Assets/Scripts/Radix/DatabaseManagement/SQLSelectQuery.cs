using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Radix.DatabaseManagement
{
    public class SQLSelectQuery : SQLQuery
    {
        private List<string> mRow = new List<string>(); 

        public override string GetQuery()
        {
            string query = "SELECT ";

            foreach(string row in mRow)
            {
                query += row;
                query += ",";
            }
            query = query.Remove(query.Length - 1);

            query += " FROM ";
            query += TableName;
            query += ";";

            return query;
        }

        protected void AddRow(string pRow)
        {
            mRow.Add(pRow);
        }
    }
}
