using System;
using System.ComponentModel;

namespace DBDiff.Schema
{
    public static class Enums
    {
        /// <summary>
        /// OriginalStatus = El objeto no sufrio modificaciones.
        /// CreateStatus = El objeto se debe crear.
        /// DropStatus = El objeto se debe eliminar.
        /// AlterStatus = El objeto sufrio modificaciones.
        /// AlterRebuildStatus = El objeto sufrio modificaciones, pero se debe hacer un DROP y ADD.
        /// AlterPropertiesStatus = El objeto sufrio modificaciones en sus propiedades, pero no en su estructura.
        /// </summary>
        [Flags]
        public enum ObjectStatusType
        {
            OriginalStatus = 0,
            AlterStatus = 2,
            AlterBodyStatus = 4,
            RebuildStatus = 8,
            RebuildDependenciesStatus = 16,
            UpdateStatus = 32,
            CreateStatus = 64,
            DropStatus = 128,
            DisabledStatus = 256,
            ChangeOwner = 512,
            DropOlderStatus = 1024,
            BindStatus = 2048,
            PermisionSet = 4096
        }

        public enum ScripActionType
        {
            None = 0,
            PreSets = 1,
            UseDatabase = 2,
            AddFileGroup = 9,
            AddFile = 10,
            AlterFile = 11,
            AlterFileGroup = 12,
            UnbindRuleColumn = 13,
            UnbindRuleType = 14,
            DropRule = 15,
            AddRule = 16,
            AddUser = 17,
            BeginTransaction = 19,
          
            DropFullTextIndex = 20,
            DropConstraintFK = 21,
            DropConstraint = 22,
            DropConstraintPK = 23,
            DropSynonyms = 24,
            DropStoreProcedure = 25,
            DropTrigger = 26,
            DropView = 27,
            DropFunction = 27,
            DropIndex = 28,            
            DropTable = 30,
            AlterColumnFormula = 40,
            AlterColumn = 41,
            AddRole = 42,
            AddSchema = 54,
            AddDefault = 55,                        
            AddAssembly = 56,
            AddAssemblyFile = 57,
            AddUserDataType = 58,
            AddTableType = 59,            
            AlterPartitionFunction = 60,
            AddPartitionFunction = 61,
            AddPartitionScheme = 62,         
            AddFullText = 63,
            AddXMLSchema = 64,
            AlterAssembly = 65,
            UpdateTable = 70,
            AlterTable = 71,
            AlterIndex = 72,
            AlterFullTextIndex = 73,
            AddTable = 74,
            RebuildTable = 75,
            AlterColumnRestore = 76,
            AlterColumnFormulaRestore = 77,
            AlterFunction = 78,
            AlterView = 79,
            AlterProcedure = 80,
            AddIndex = 90,                     
            AddFunction = 91,
            AddView = 91, /*AddFunction and AddView must have the same number!!!*/
            AddTrigger = 92,
            AddConstraint = 93,
            AddConstraintPK = 94,
            AddConstraintFK = 95,
            AlterConstraint = 96,
            AddFullTextIndex = 97,
            EnabledTrigger = 98,
            AddSynonyms = 99,
            AddStoreProcedure = 100,
            DropOptions = 101,
            AddOptions = 102,
            
            AlterTableChangeTracking = 103,

            DropFullText = 110,
            DropTableType = 111,
            DropUserDataType = 112,
            DropXMLSchema = 113,
            DropAssemblyUserDataType = 114,
            DropAssemblyFile = 115,
            DropAssembly = 116,
            DropDefault = 117,

            DropPartitionScheme = 120,
            DropPartitionFunction = 121,
            
            DropSchema = 122,
            DropUser = 123,
            DropRole = 124,
            DropFile = 125,
            DropFileGroup = 126,
            AddExtendedProperty = 127,
            DropExtendedProperty = 128,

            EndTransaction = 140,
            PostSets = 150
        }

        public enum ObjectType
        {
            None = 0,
            Table = 1,
            Column = 2,
            Trigger = 3,
            Constraint = 4,
            ConstraintColumn = 5,
            Index = 6,
            IndexColumn = 7,
            [Description("User Data Type")]
            UserDataType = 8,
            [Description("XML Schema")]
            XMLSchema = 9,
            View = 10,
            Function = 11,
            [Description("Store Procedure")]
            StoreProcedure = 12,
            TableOption = 13,
            Database = 14,
            Schema = 15,
            FileGroup = 16,
            File = 17,
            Default = 18,
            Rule = 19,
            Synonym = 20,
            Assembly = 21,
            User = 22,
            Role = 23,
            FullText = 24,
            AssemblyFile = 25,
            [Description("CLR Store Procedure")]
            CLRStoreProcedure = 26,
            [Description("CLR Trigger")]
            CLRTrigger = 27,
            [Description("CLR Function")]
            CLRFunction = 28,
            [Description("Extended Property")]
            ExtendedProperty = 30,
            Partition = 31,
            [Description("Partition Function")]
            PartitionFunction = 32,
            [Description("Partition Scheme")]
            PartitionScheme = 33,
            [Description("Table Type")]
            TableType = 34,
            FullTextIndex = 35
        }
    }
}
