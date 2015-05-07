using UnityEngine;
using System.Collections;
using Radix.DatabaseManagement;
using Radix.DatabaseManagement.Sqlite;
using Radix.Service;
using System;

public class TestSelectQuery : SQLSelectQuery 
{
    public TestSelectQuery()
    {
        mDBName = "Test1";
        TableName = "Person";
        AddRow("Name");
        AddRow("Age");
    }

    public void Execute()
    {
        ServiceManager.Instance.GetService<SqliteService>().ExecuteQuery(this);
    }

    public string GetName()
    {
        return Result[0][0] as string;
    }

    public int GetAge()
    {
        return Convert.ToInt32(Result[0][1]);
    }

    public int GetResultCount()
    {
        return Result.Count;
    }
}
