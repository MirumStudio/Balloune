using UnityEngine;
using System.Collections;
using Radix.DatabaseManagement;
using System;

public class TestCreateQuery : SQLCreateQuery
{
    public TestCreateQuery() 
    {
        mDBName = "Test1";
    }

    public override string GetQuery()
    {
        return @"CREATE TABLE `Person` (
	                `Name`	TEXT,
	                `Age`	INTEGER
                );";
    }
}
