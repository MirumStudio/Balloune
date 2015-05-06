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
        //ServiceManager.Instance.GetService<SqliteService>().ExecuteQuery(null);
        Assert.Fail();
    }

    [Test]
    public void Test()
    {
        ServiceManager.Instance.GetService<SqliteService>().ExecuteQuery(new TestCreateQuery());
        Assert.Pass();
    }

    /*
     * create (file exist)
     * select 
     * ins (select)
     * Update (select)
     * delete (sele
     */
}
