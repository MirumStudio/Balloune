/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Radix.DatabaseManagement
{
    public class SQLUpdateQuery : SQLQuery
    {
        private Dictionary<string, string> mData = new Dictionary<string, string>();
        private Dictionary<string, string> mWhereValue = new Dictionary<string, string>();

        protected void AddData(string pRow, string pValue)
        {
            //Check is exist
            mData.Add(pRow, pValue);
        }

        protected void AddWhereValue(string pRow, string pValue)
        {
            //Check is exist
            mWhereValue.Add(pRow, pValue);
        }

        public override string GetQuery()
        {
            string query = "UPDATE ";
            query += TableName;
            query += " SET ";

            foreach (KeyValuePair<string, string> pair in mData)
            {
                query += pair.Key;
                query += "='";
                query += pair.Value;
                query += "',";
            }

            query = query.Remove(query.Length - 1);
            query += " WHERE ";

            int index = 0;
            foreach (KeyValuePair<string, string> pair in mWhereValue)
            {
                query += pair.Key;
                query += "='";
                query += pair.Value;
                query += "'";

                if(index != mWhereValue.Count - 1)
                {
                    query += " AND ";
                }

                index++;
            }

            query += ";";

            return query;
        }
    }
}
