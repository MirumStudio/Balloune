using UnityEngine;
using System.Collections;
using Radix.DatabaseManagement;

public class TestDeleteQuery : SQLDeleteQuery
{
    public TestDeleteQuery()
    {
        base.mDBName = "Test1";

        TableName = "Person";

        AddWhereValue("Age", "22");
        
    }
}
