using UnityEngine;
using System.Collections;
using Radix.DatabaseManagement;
using System.Collections.Generic;

public class TestUpdateQuery : SQLUpdateQuery
{
    public TestUpdateQuery()
    {
        mDBName = "Test1";
        TableName = "Person";

        AddData("Age", "22");
        AddWhereValue("Name", "Bob");
    }
    
}
