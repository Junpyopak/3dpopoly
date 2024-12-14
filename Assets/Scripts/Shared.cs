using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Shared 
{
    public static Scenemgr Scenemgr;//�̱��� ���� static ��������
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
