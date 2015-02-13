using System;
using System.Collections.Generic;
using System.Text;
using DBDiff.Schema.Model;

namespace DBDiff.Schema
{
    public class SQLScriptList
    {
        private List<SQLScript> list;

        public void Sort()
        {
            if (list != null) list.Sort();
        }

        public void Add(SQLScript item, int deep)
        {
            if (list == null) list = new List<SQLScript>();
            if (item != null)
            {
                item.Deep = deep;
                list.Add(item);
            }
        }

        public void Add(SQLScript item)
        {
            if (list == null) list = new List<SQLScript>();
            if (item != null) list.Add(item);
        }

        public void Add(string SQL, int dependencies, Enums.ScripActionType type)
        {
            if (list == null) list = new List<SQLScript>();
            list.Add(new SQLScript(SQL, dependencies, type));
        }

        public void AddRange(SQLScriptList items)
        {
            for (int j = 0; j < items.Count; j++)
            {
                if (list == null) list = new List<SQLScript>();
                list.Add(items[j]);
            }
        }

        public int Count
        {
            get { return (list==null)?0:list.Count; }
        }

        public SQLScript this[int index]
        {
            get { return list[index]; }
        }

        public string ToSQL()
        {
            StringBuilder sql = new StringBuilder();
            this.Sort(); /*Ordena la lista antes de generar el script*/
            if (list != null)
            {                
                for (int j = 0; j < list.Count; j++)
                {
                    if (!String.IsNullOrEmpty(list[j].dbObject))
                        sql.Append(String.Format("Print '{0}'\r\nGO\r\n", list[j].Status + " " + list[j].dbObject));
                    sql.Append(list[j].SQL); //ToSqlDown(list[j]);
                    if (list[j].Status > Enums.ScripActionType.BeginTransaction && list[j].Status < Enums.ScripActionType.EndTransaction)
                        sql.Append("IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END\r\nGO\r\n\r\n");
                }
                
            }
            return sql.ToString();
        }

        public SQLScriptList FindAlter()
        {
            SQLScriptList alter = new SQLScriptList();
            list.ForEach(item => { if ((item.Status == Enums.ScripActionType.AlterView) || (item.Status == Enums.ScripActionType.AlterFunction) || (item.Status == Enums.ScripActionType.AlterProcedure)) alter.Add(item); });
            return alter;
        }
    }

    public static class SQLScriptListExtensionMethod
    {
        public static SQLScriptList WarnMissingScript(this SQLScriptList scriptList, ISchemaBase scriptSource)
        {
            if (scriptList == null || scriptSource == null || scriptSource.Status == Enums.ObjectStatusType.OriginalStatus)
            {
                return scriptList;
            }

            for (int i = 0; i < scriptList.Count; ++i)
            {
                if (!String.IsNullOrEmpty(scriptList[i].SQL))
                {
                    return scriptList;
                }
            }

            scriptList.Add(String.Format("\r\n--\r\n-- DIFF-ERROR 0x{0:x8}.{1:d3}: Missing {2} script for {3} '{4}'\r\n--\r\n\r\n", (int)scriptSource.Status, (int)scriptSource.ObjectType, scriptSource.Status, scriptSource.ObjectType, scriptSource.Name), 0, Enums.ScripActionType.None);
            return scriptList;
        }
    }
}
