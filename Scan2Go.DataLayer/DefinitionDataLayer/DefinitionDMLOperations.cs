using DataLayer.Base.DatabaseFactory;
using DataLayer.Base.SqlGenerator;
using Scan2Go.DataLayer.BaseClasses.DataLayersBases;
using Scan2Go.Entity.Definitions;
using System.Data;
using Utility.Core.DataLayer;
using Utility.Enum;

namespace Scan2Go.DataLayer.DefinitionDataLayer;

public class DefinitionDMLOperations : Scan2GoDataLayerBase
{
    public DefinitionDMLOperations(DataLayerBase dataLayerBase) : base(dataLayerBase)
    {
    }

    public void SaveDefinition(Definition definition)
    {
        List<DatabaseParameter> databaseParameters = definition.GetEntityDbParameters();

        if (definition.PrimaryKeyValue == 0)
        {
            SqlInsertFactory sqlInsertFactory = new SqlInsertFactory(definition.GetTableName(), databaseParameters);
            int pkValue = this.ExecuteInsertNonQuery(sqlInsertFactory);

            if (definition.defDetailSchemas.Any(p => p.IsNameField))
            {
                definition.defDetailSchemas.FirstOrDefault(p => p.IsPk).FieldValue = pkValue;
                //filtering the list so that only PK and name field are left as parameters
                databaseParameters = definition.GetEntityPkAndStringDbParameters();

                //setting PK value to insert it later to 1 TO N tables .
                databaseParameters.FirstOrDefault(p => p.IsPrimaryKey).FieldValue = pkValue;

                //So that it got inserted as value to the new table
                databaseParameters.FirstOrDefault(p => p.IsPrimaryKey).IsPrimaryKey = false;

                DatabaseParameter systemLanguageParam = new DatabaseParameter
                {
                    FieldName = Definition.Field.SystemLanguageId,
                    DbType = DbType.Int32,
                };

                databaseParameters.Add(systemLanguageParam);

                //Inserting the same value to all languages
                var languageEnumValues = Enum.GetValues(typeof(LanguageEnum));

                foreach (var language in languageEnumValues)
                {
                    systemLanguageParam.FieldValue = (int)language;

                    sqlInsertFactory = new SqlInsertFactory(definition.GetTableName() + "_" + Definition.Field.LanguageTableExtention, databaseParameters);
                    this.ExecuteInsertNonQuery(sqlInsertFactory);
                }
            }
        }
        else
        {
            SqlUpdateFactory sqlUpdateFactory = new SqlUpdateFactory(definition.GetTableName(), databaseParameters);
            this.ExecuteUpdateNonQuery(sqlUpdateFactory);

            if (definition.defDetailSchemas.Any(p => p.IsNameField))
            {
                //filtering the list so that only PK and name field are left as parameters
                databaseParameters = definition.GetEntityPkAndStringDbParameters();

                if (databaseParameters == null)
                {
                    return;
                }

                if (definition.IsLanguageIndependent == false)
                {
                    DatabaseParameter systemLanguageParam = new DatabaseParameter
                    {
                        FieldName = Definition.Field.SystemLanguageId,
                        DbType = DbType.Int32,
                        FieldValue = (int)definition.currentInterfaceLanguage,
                        IsPrimaryKey = true
                    };

                    databaseParameters.Add(systemLanguageParam);
                }

                sqlUpdateFactory = new SqlUpdateFactory(definition.GetTableName() + "_" + Definition.Field.LanguageTableExtention, databaseParameters);
                this.ExecuteUpdateNonQuery(sqlUpdateFactory);
            }
        }
    }

    internal void DeleteDefinition(Definition definition)
    {
        SqlDeleteFactory sqlDeleteLanguageFactory = new SqlDeleteFactory(definition.GetTableName() + "_Language", definition.GetDatabaseParameterOfPrimaryKey());

        this.ExecuteDeleteNonQuery(sqlDeleteLanguageFactory);

        SqlDeleteFactory sqlDeleteFactory = new SqlDeleteFactory(definition.GetTableName(), definition.GetDatabaseParameterOfPrimaryKey());

        this.ExecuteDeleteNonQuery(sqlDeleteFactory);
    }
}