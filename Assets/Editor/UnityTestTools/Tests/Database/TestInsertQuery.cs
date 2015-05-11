using UnityEngine;
using System.Collections;
using Radix.DatabaseManagement;

public class TestInsertQuery : SQLInsertQuery
{
    public TestInsertQuery()
    {
        base.mDBName = "Test1";

        TableName = "Person";
        
    }

    //NOT to do in real query
    public void AddDataTest(string pRow, string pValue)
    {
        AddData(pRow, pValue);
    }
}
