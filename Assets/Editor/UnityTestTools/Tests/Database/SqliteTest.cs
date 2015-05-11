using UnityEngine;
using System.Collections;
using NUnit.Framework;
using Radix.Service;
using Radix.DatabaseManagement.Sqlite;
using System;
using Radix.DatabaseManagement;
using System.IO;
using UnityEditor;

public class SqliteTest 
{
    [SetUp]
    public void InitService()
    {
        ServiceManager.Instance.Init();
        Assert.Pass();
    }

    [Test]
    public void ExecuteQueryNull()
    {
        //ServiceManager.Instance.GetService<SqliteService>().ExecuteQuery(null);
        Assert.Pass();
    }

    [Test]
    public void DeleteOldDatabase()
    {
        AssetDatabase.DeleteAsset("Assets/Databases/Test1.db");
        Assert.Pass();
    }

    [Test]
    public void Createdatabase()
    {
        var lol = ServiceManager.Instance.GetService<SqliteService>();

        if(lol == null)
        {
            Assert.Fail("WHUT WHUT");
        }

        ServiceManager.Instance.GetService<SqliteService>().ExecuteQuery(new TestCreateQuery());
        Assert.Pass();
    }
    
    [Test]
    public void InsertFormat()
    {
        TestInsertQuery query = new TestInsertQuery();

        query.AddDataTest("Name", "Bob");
        query.AddDataTest("Age", "21");

        string queryWanted = "INSERT INTO Person (Name,Age) VALUES ('Bob','21');";

        Assert.AreEqual(query.GetQuery(), queryWanted);
    }

    [Test]
    public void ExecuteInsert()
    {
        TestInsertQuery query = new TestInsertQuery();

        query.AddDataTest("Name", "Bob");
        query.AddDataTest("Age", "21");

        ServiceManager.Instance.GetService<SqliteService>().ExecuteQuery(query);
        Assert.Pass();
    }

    [Test]
    public void SelectFormat()
    {
        SQLQuery query = new TestSelectQuery();

        string queryWanted = "SELECT Name,Age FROM Person;";

        Assert.AreEqual(query.GetQuery(), queryWanted);
    }

    [Test]
    public void SelectExecute()
    {
        TestSelectQuery query = new TestSelectQuery();

        ServiceManager.Instance.GetService<SqliteService>().ExecuteQuery(query);

        Assert.AreEqual(1, query.GetResultCount());
        Assert.AreEqual(21, query.GetAge());
        Assert.AreEqual("Bob", query.GetName());
    }

    [Test]
    public void UpdateFormat()
    {
        SQLQuery query = new TestUpdateQuery();

        string queryWanted = "UPDATE Person SET Age='22' WHERE Name='Bob';";

        Assert.AreEqual(query.GetQuery(), queryWanted);
    }

    [Test]
    public void UpdateExecute()
    {
        TestUpdateQuery query = new TestUpdateQuery();

        ServiceManager.Instance.GetService<SqliteService>().ExecuteQuery(query);

        TestSelectQuery query2 = new TestSelectQuery();

        ServiceManager.Instance.GetService<SqliteService>().ExecuteQuery(query);

        Assert.AreEqual(1, query2.GetResultCount());
        Assert.AreEqual(22, query2.GetAge());
        Assert.AreEqual("Bob", query2.GetName());
    }

    [Test]
    public void DeleteFormat()
    {
        SQLQuery query = new TestDeleteQuery();

        string queryWanted = "DELETE FROM Person WHERE Age='22';";

        Assert.AreEqual(query.GetQuery(), queryWanted);
    }

    [Test]
    public void DeleteExecute()
    {
        TestDeleteQuery query = new TestDeleteQuery();

        ServiceManager.Instance.GetService<SqliteService>().ExecuteQuery(query);

        TestSelectQuery query2 = new TestSelectQuery();

        ServiceManager.Instance.GetService<SqliteService>().ExecuteQuery(query);

        Assert.AreEqual(0, query2.GetResultCount());
    }

  /*  [Test]
    public void DeleteDB()
    {
        TestDeleteDB delete = new TestDeleteDB();
        delete.Execute();
    }*/

    /*
     * create (file exist)
     * select 
     * ins (select)
     * Update (select)
     * delete (sele
     * cehc query formartion
     * Empty
     */

}
