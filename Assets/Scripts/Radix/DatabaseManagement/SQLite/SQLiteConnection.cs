using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Radix.DatabaseManagement
{
    class SQLiteConnectiont
    {
        public EDatabaseType _databaseType { get; set; }
        public string _connectionString { get; set; }
        public string _path { get; set; }
        public string _password { get; set; }
        public bool _useUTF16 { get; set; }
        public bool _isLegacy { get; set; }
        public bool _usePooling { get; set; }
        public bool _isReadOnly { get; set; }
        public bool _useTicksAsDateTimeFormat { get; set; }
        public bool _useGUIDasText { get; set; }
        public bool _createOnMissing { get; set; }
        public bool _keepJournal { get; set; }
        public int _maxPageCount { get; set; }
        public int _cacheSize { get; set; }
        public int _pageSize { get; set; }
        public int _maxPoolSize { get; set; }

        public SQLiteConnectiont()
        {
            this._path = "";
            this._password = "";
            this._useUTF16 = false;
            this._isLegacy = false;
            this._usePooling = false;
            this._isReadOnly = false;
            this._useTicksAsDateTimeFormat = false;
            this._useGUIDasText = false;
            this._createOnMissing = false;
            this._keepJournal = true;
            this._maxPageCount = -1;
            this._maxPoolSize = -1;
            this._cacheSize = -1;
            this._pageSize = -1;
            
        }

        public SQLiteConnectiont(string path, string password, bool useUTF16, bool isLegacy, bool usePooling, bool isReadOnly, bool useTickAsDateTimeFormat, bool useGUIDasText, bool createOnMissing, bool keepJournal, int maxPageCount, int maxPoolSize, int cacheSize, int pageSize)
        {
            this._path = path;
            this._password = password;
            this._useUTF16 = useUTF16;
            this._isLegacy = isLegacy;
            this._usePooling = usePooling;
            this._isReadOnly = isReadOnly;
            this._useTicksAsDateTimeFormat = useTickAsDateTimeFormat;
            this._useGUIDasText = useGUIDasText;
            this._createOnMissing = createOnMissing;
            this._keepJournal = keepJournal;
            this._maxPageCount = maxPageCount;
            this._maxPoolSize = maxPoolSize;
            this._cacheSize = cacheSize;
            this._pageSize = pageSize;

        }

        public void buildConnectionString()
        {
            string partialConnectionString = "Data Source=";

            if (this._path != "")
            {
                partialConnectionString += this._path + ";Version=3;";
            }

            if (this._password != "")
            {
                partialConnectionString += "Password=" + this._password + ";";
            }

            if (this._isLegacy)
            {
                partialConnectionString += " UseUTF16Encoding = True;";
            }

            if(this._usePooling && this._maxPoolSize != -1)
            {
                partialConnectionString += "Pooling=True; Max Pool Size=" + this._maxPoolSize + ";";
            }

            if (this._isReadOnly)
            {
                partialConnectionString += "Read Only=True;";
            }

            if (this._useTicksAsDateTimeFormat)
            {
                partialConnectionString += "DateTimeFormat=Ticks;";
            }

            if (this._useGUIDasText)
            {
                partialConnectionString += "BinaryGUID=False;";
            }

            if (this._createOnMissing)
            {
                partialConnectionString += "FailIfMissing=True";
            }

            if (!this._keepJournal)
            {
                partialConnectionString += "Journal Mode=Off";
            }

            if (this._maxPageCount != -1)
            {
                partialConnectionString += "Max Page Count=" + this._maxPageCount + ";";
            }

            if(this._cacheSize != -1)
            {
                partialConnectionString += "Cache Size="+ this._cacheSize + ";";
            }

            if(this._pageSize != -1)
            {
                partialConnectionString += "Page Size=" + this._pageSize + ";";
            }

            this._connectionString = partialConnectionString;
        }

    }
}
