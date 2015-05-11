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
    [Test]
    public void BigDatabasetest()
    {
        ServiceManager.Instance.Init();
        AssetDatabase.DeleteAsset("Assets/Databases/Test1.db");

        ServiceManager.Instance.GetService<SqliteService>();

        var query = new TestCreateQuery();
        query.Execute();

        TestInsertQuery insertQuery = new TestInsertQuery();

        insertQuery.AddDataTest("Name", "Bob");
        insertQuery.AddDataTest("Age", "21");

        string insertQueryWanted = "INSERT INTO Person (Name,Age) VALUES ('Bob','21');";

        Assert.AreEqual(insertQuery.GetQuery(), insertQueryWanted);
        insertQuery.Execute();

        TestSelectQuery selectQuery = new TestSelectQuery();

        string selectQueryWanted = "SELECT Name,Age FROM Person;";

        Assert.AreEqual(selectQuery.GetQuery(), selectQueryWanted);

        selectQuery.Execute();

        Assert.AreEqual(1, selectQuery.GetResultCount());
        Assert.AreEqual(21, selectQuery.GetAge());
        Assert.AreEqual("Bob", selectQuery.GetName());

        TestUpdateQuery updateQuery = new TestUpdateQuery();

        string updateQueryWanted = "UPDATE Person SET Age='22' WHERE Name='Bob';";

        Assert.AreEqual(updateQuery.GetQuery(), updateQueryWanted);

        updateQuery.Execute();
        selectQuery.Execute();

        Assert.AreEqual(1, selectQuery.GetResultCount());
        Assert.AreEqual(22, selectQuery.GetAge());
        Assert.AreEqual("Bob", selectQuery.GetName());

        TestDeleteQuery deleteQuery = new TestDeleteQuery();

        string deleteQueryWanted = "DELETE FROM Person WHERE Age='22';";

        Assert.AreEqual(deleteQuery.GetQuery(), deleteQueryWanted);

        deleteQuery.Execute();

        selectQuery.Execute();
        Assert.AreEqual(0, selectQuery.GetResultCount());
    }


    /*[SetUp]
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
        ServiceManager.Instance.GetService<SqliteService>();

        var query =  new TestCreateQuery();
        query.Execute();

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

        query.Execute();
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

        query.Execute();

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

        query.Execute();

        TestSelectQuery query2 = new TestSelectQuery();

        query2.Execute();

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

        query.Execute();

        TestSelectQuery query2 = new TestSelectQuery();

        query2.Execute();

        Assert.AreEqual(0, query2.GetResultCount());
    }*/
}
