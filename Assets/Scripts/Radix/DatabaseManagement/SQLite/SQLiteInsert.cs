using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Radix.DatabaseManagement
{
    class SQLiteInsert
    {
        private string _table { get;set; }
        private DatabaseParameter[] _values { get; set; }

        #region Constructors
        public SQLiteInsert()
        {
            this._table = string.Empty;
            this._values = new DatabaseParameter[0];
        }

        public SQLiteInsert(string table, DatabaseParameter[] values)
        {
            this._table = table;
            this._values = values;
        }

        public SQLiteInsert(string table)
        {
            this._table = table;
            this._values = new DatabaseParameter[0];
        }

        #endregion

        public void prepareQuery()
        {
            int nbOfParameter = _values.Length;
            string insertQuestionMarksInQuery = "?";
            
            for (int i = 0; i < nbOfParameter - 1; i++)
            {
                insertQuestionMarksInQuery += ",?";
            }

            /*SQLiteCommand insertCommand = new SQLiteCommand("INSERT INTO " + this._table + " VALUES (" + insertQuestionMarksInQuery + ")");

            for (int i = 0; i < nbOfParameter; i++)
            {
                insertCommand.Parameters.AddWithValue(null, _values[i]._value);
            }*/
        }

        public Boolean executeQuery()
        {
            //TODO EVERYTHING HERE
            //ADD A DatabaseConnectionClass
           // SQLiteConnection cc = new SQLiteConnection();

            return true;
        }


    }
}
