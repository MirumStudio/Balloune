using UnityEngine;
using System.Collections;
using NUnit.Framework;
using Radix.Service;
using Radix.DatabaseManagement.Sqlite;
using System;

public class SqliteTest 
{
    [Test]
    public void InitService()
    {
        ServiceManager.Instance.Init();
        Assert.Pass();
    }

    [Test]
    public void ServiceInit()
    { 
        ServiceManager.Instance.GetService<SqliteService>();
        Assert.Pass();
    }

    [Test]
    public void ExecuteQueryNull()
    {
        bool test = ServiceManager.Instance.GetService<SqliteService>().ExecuteQuery(null);
        Assert.False(test);
    }
}
