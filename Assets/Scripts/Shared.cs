using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Shared 
{
    public static Scenemgr Scenemgr;//싱글턴 패턴 static 정적으로
    public static Table_Manager TableMgr;

    public static Table_Manager InitTableMgr()
    {
        if (TableMgr == null)
        {
            TableMgr = new Table_Manager();
            TableMgr.Init();
        }

        return TableMgr;
    }
}
