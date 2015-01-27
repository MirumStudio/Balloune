using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Radix.DatabaseManagement
{
    class DatabaseParameter
    {
        public EDatabaseType _databaseType { get; set; }
        public string _value { get; set; }
        public EDataType _valueType { get; set; }

        #region Constructors
        public DatabaseParameter()
        {
            this._value = string.Empty;
            this._databaseType = EDatabaseType.SQLite;
            this._valueType = EDataType.SQLITE_TEXT;
        }
        public DatabaseParameter(string value)
        {
            this._value = value;
            this._valueType = EDataType.SQLITE_TEXT;
            this._databaseType = EDatabaseType.SQLite;
        }
        public DatabaseParameter(string value, EDataType dataType)
        {
            this._value = value;
            this._valueType = dataType;
            this._databaseType = EDatabaseType.SQLite;
        }

        public DatabaseParameter(string value, EDataType dataType, EDatabaseType databaseType)
        {
            this._value = value;
            this._valueType = dataType;
            this._databaseType = databaseType;
        }
        #endregion

        public void testFunction()
        {

        }
    }
}
