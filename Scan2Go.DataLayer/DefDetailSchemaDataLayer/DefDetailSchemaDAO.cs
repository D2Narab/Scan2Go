using System;
using System.Collections.Generic;
using System.Data;
using Scan2Go.DataLayer.BaseClasses.DataLayersBases;
using Scan2Go.Entity.Definitions;
using Scan2Go.Enums;
using DataLayer.Base.GeneralDataLayer;
using DataLayer.Base.SqlGenerator;
using Scan2Go.DataLayer.DefDetailSchemaDataLayer;
using Scan2Go.Entity.Definitions;
using Scan2Go.Enums;
using Utility.Core;
using Utility.Core.DataLayer;
using Utility.Enum;
using Utility.Extensions;

namespace Scan2Go.DataLayer.DefDetailSchemaDataLayer;

public class DefDetailSchemaDAO : Scan2GoDataLayerBase
{
    public DefDetailSchemaDAO()
    {
    }

    public int CheckIfDefinitionFieldNameIsAlreadyDefinedInTheSystem(string fieldName, string defName)
    {
        SqlSelectFactory sqlSelectFactory =
            new DefDetailSchemaSelectOperations(this).CheckIfDefinitionFieldNameIsAlreadyDefinedInTheSystem(fieldName, defName);

        int returnedId = this.ExecuteScalar(sqlSelectFactory).AsInt();

        return returnedId;
    }

    public int CheckIfDefinitionNameIsAlreadyDefinedInTheSystem(string definitionName)
    {
        SqlSelectFactory sqlSelectFactory = new DefDetailSchemaSelectOperations(this).CheckIfDefinitionNameIsAlreadyDefinedInTheSystem(definitionName);

        int returnedId = this.ExecuteScalar(sqlSelectFactory).AsInt();

        return returnedId;
    }

    public OperationResult CreateNewDefinitionTable(DefinitionTableCreation definitionTableCreation)
    {
        OperationResult operationResult = new OperationResult();

        try
        {
            this.BeginTransaction();

            /******************************* CREATING THE DEFINITION TABLE *******************************/
            new GeneralDMLOperations(this).ExecuteNonQuery(DefDetailSchemaSql.CreateNewDefinitionTable(definitionTableCreation.DefinitionName));
            /*******************************************************************************************/

            /******************************* CREATING A NEW DEFINITION TYPE *******************************/
            List<DatabaseParameter> databaseParameters = new List<DatabaseParameter>();

            databaseParameters.Add(new DatabaseParameter
            {
                FieldName = "NameField",
                DbType = DbType.String,
                FieldValue = "Def_" + definitionTableCreation.DefinitionName
            });

            databaseParameters.Add(new DatabaseParameter
            {
                FieldName = Definition.Field.IsActive,
                DbType = DbType.Boolean,
                FieldValue = definitionTableCreation.IsActive
            });

            databaseParameters.Add(new DatabaseParameter
            {
                FieldName = Definition.Field.IsDefinable,
                DbType = DbType.Boolean,
                FieldValue = definitionTableCreation.IsDefinable
            });

            databaseParameters.Add(new DatabaseParameter
            {
                FieldName = Definition.Field.IsLanguageIndependent,
                DbType = DbType.Boolean,
                FieldValue = definitionTableCreation.IsLanguageIndependent
            });

            databaseParameters.Add(new DatabaseParameter
            {
                FieldName = "TableName",
                DbType = DbType.String,
                FieldValue = "Def_" + definitionTableCreation.DefinitionName
            });

            SqlInsertFactory sqlInsertFactory = new SqlInsertFactory(TableName.Def_Definitions, databaseParameters);
            int newDefinitionTypeId = this.ExecuteInsertNonQuery(sqlInsertFactory);
            /*******************************************************************************************/

            /********************* ADDING RECORDS TO THE DEFINITON  LANGUAGE TABLE ***********************/
            databaseParameters.Clear();

            databaseParameters.Add(new DatabaseParameter
            {
                FieldName = Definition.Field.DefinitionId,
                DbType = DbType.Int32,
                FieldValue = newDefinitionTypeId
            });

            //databaseParameters.Add(new DatabaseParameter
            //{
            //    FieldName = Definition.Field.NameField,
            //    DbType = DbType.String,
            //    FieldValue = definitionTableCreation.DefinitionName
            //});

            DatabaseParameter languageTextParameter = new DatabaseParameter
            {
                FieldName = Definition.Field.NameField,
                DbType = DbType.String
            };

            DatabaseParameter languageIdParameter = new DatabaseParameter
            {
                FieldName = Definition.Field.SystemLanguageId,
                DbType = DbType.Int32
            };

            databaseParameters.Add(languageIdParameter);
            databaseParameters.Add(languageTextParameter);

            foreach (LanguageEnum languageEnum in Enum.GetValues(typeof(LanguageEnum)))
            {
                languageIdParameter.FieldValue = languageEnum.AsInt();

                if (languageEnum == LanguageEnum.EN)
                {
                    languageTextParameter.FieldValue = definitionTableCreation.EnglishName;
                }
                else if (languageEnum == LanguageEnum.AR)
                {
                    languageTextParameter.FieldValue = definitionTableCreation.ArabicName;
                }
                else if (languageEnum == LanguageEnum.TR)
                {
                    languageTextParameter.FieldValue = definitionTableCreation.TurkishName;
                }
                else if (languageEnum == LanguageEnum.AZ)
                {
                    languageTextParameter.FieldValue = definitionTableCreation.AzerbaijaniName;
                }

                sqlInsertFactory = new SqlInsertFactory(TableName.Def_Definitions_Language, databaseParameters);
                int returnValue = this.ExecuteInsertNonQuery(sqlInsertFactory);
            }
            /*******************************************************************************************/

            /******************* ADDING PK RECORD TO THE DEF DETAIL SCHEMA TABLE ******************/
            DefDetailSchema defDetailSchema = new DefDetailSchema();

            defDetailSchema.DefName = "Def_" + definitionTableCreation.DefinitionName;
            defDetailSchema.FieldName = definitionTableCreation.DefinitionName + "Id";
            defDetailSchema.EnmInterfaceComponentType = Utility.Enum.GeneralEnums.InterfaceComponentType.Nothing;
            defDetailSchema.OrderNumber = 0;
            defDetailSchema.Length = 0;
            defDetailSchema.EnmMandatoryStatus = Utility.Enum.GeneralEnums.MandatoryStatus.Nothing;
            defDetailSchema.IsLabelVisible = false;
            defDetailSchema.IsPk = true;
            defDetailSchema.IsParent = false;
            defDetailSchema.IsMultiLanguage = false;
            defDetailSchema.IsNameField = false;

            SaveDefDetailSchema(defDetailSchema);

            defDetailSchema = new DefDetailSchema();

            defDetailSchema.DefName = "Def_" + definitionTableCreation.DefinitionName;
            defDetailSchema.FieldName = definitionTableCreation.DefinitionName + "Name";
            defDetailSchema.EnmInterfaceComponentType = Utility.Enum.GeneralEnums.InterfaceComponentType.Textbox;
            defDetailSchema.OrderNumber = 1;
            defDetailSchema.Length = 200;
            defDetailSchema.EnmMandatoryStatus = Utility.Enum.GeneralEnums.MandatoryStatus.Mandatory;
            defDetailSchema.MessageText = "enterName";
            defDetailSchema.IsLabelVisible = true;
            defDetailSchema.IsPk = false;
            defDetailSchema.IsParent = false;
            defDetailSchema.IsMultiLanguage = true;
            defDetailSchema.IsNameField = true;

            SaveDefDetailSchema(defDetailSchema, false);

            defDetailSchema = new DefDetailSchema();

            defDetailSchema.DefName = "Def_" + definitionTableCreation.DefinitionName;
            defDetailSchema.FieldName = "IsActive";
            defDetailSchema.EnmInterfaceComponentType = Utility.Enum.GeneralEnums.InterfaceComponentType.Checkbox;
            defDetailSchema.OrderNumber = 2;
            defDetailSchema.Length = 0;
            defDetailSchema.EnmMandatoryStatus = Utility.Enum.GeneralEnums.MandatoryStatus.NonMandatory;
            defDetailSchema.IsLabelVisible = true;
            defDetailSchema.IsPk = false;
            defDetailSchema.IsParent = false;
            defDetailSchema.IsMultiLanguage = false;
            defDetailSchema.IsNameField = false;

            SaveDefDetailSchema(defDetailSchema, false);
            /*******************************************************************************************/

            operationResult.State = this.CommitTransaction();

            operationResult.ResultObject = newDefinitionTypeId;
        }
        catch (Exception exception)
        {
            operationResult.State = false;
            operationResult.Exception = exception;
            this.RollbackTransaction();
        }

        return operationResult;
    }

    public OperationResult UpdateDefinitionTable(DefinitionTableCreation definitionTableCreation)
    {
        OperationResult operationResult = new OperationResult();

        try
        {
            this.BeginTransaction();

            List<DatabaseParameter> databaseParameters = new List<DatabaseParameter>();


            databaseParameters.Add(new DatabaseParameter
            {
                FieldName = Definition.Field.IsActive,
                DbType = DbType.Boolean,
                FieldValue = definitionTableCreation.IsActive
            });

            databaseParameters.Add(new DatabaseParameter
            {
                FieldName = Definition.Field.IsDefinable,
                DbType = DbType.Boolean,
                FieldValue = definitionTableCreation.IsDefinable
            });

            databaseParameters.Add(new DatabaseParameter
            {
                FieldName = Definition.Field.IsLanguageIndependent,
                DbType = DbType.Boolean,
                FieldValue = definitionTableCreation.IsLanguageIndependent
            });

            databaseParameters.Add(new DatabaseParameter
            {
                FieldName = Definition.Field.DefinitionId,
                DbType = DbType.Boolean,
                FieldValue = definitionTableCreation.DefinitionId,
                IsPrimaryKey = true
            });


            SqlUpdateFactory sqlUpdateFactory = new SqlUpdateFactory(TableName.Def_Definitions, databaseParameters);
            this.ExecuteUpdateNonQuery(sqlUpdateFactory);

            databaseParameters.Clear();

            databaseParameters.Add(new DatabaseParameter
            {
                FieldName = Definition.Field.DefinitionId,
                DbType = DbType.Int32,
                FieldValue = definitionTableCreation.DefinitionId,
                IsPrimaryKey = true
            });


            DatabaseParameter languageTextParameter = new DatabaseParameter
            {
                FieldName = Definition.Field.NameField,
                DbType = DbType.String
            };

            DatabaseParameter languageIdParameter = new DatabaseParameter
            {
                FieldName = Definition.Field.SystemLanguageId,
                DbType = DbType.Int32,
                IsPrimaryKey = true
            };

            databaseParameters.Add(languageIdParameter);
            databaseParameters.Add(languageTextParameter);

            foreach (LanguageEnum languageEnum in Enum.GetValues(typeof(LanguageEnum)))
            {
                languageIdParameter.FieldValue = languageEnum.AsInt();

                if (languageEnum == LanguageEnum.EN)
                {
                    languageTextParameter.FieldValue = definitionTableCreation.EnglishName;
                }
                else if (languageEnum == LanguageEnum.AR)
                {
                    languageTextParameter.FieldValue = definitionTableCreation.ArabicName;
                }
                else if (languageEnum == LanguageEnum.TR)
                {
                    languageTextParameter.FieldValue = definitionTableCreation.TurkishName;
                }
                else if (languageEnum == LanguageEnum.AZ)
                {
                    languageTextParameter.FieldValue = definitionTableCreation.AzerbaijaniName;
                }

                sqlUpdateFactory = new SqlUpdateFactory(TableName.Def_Definitions_Language, databaseParameters);
                int returnValue = this.ExecuteUpdateNonQuery(sqlUpdateFactory);
            }
            operationResult.State = this.CommitTransaction();

        }
        catch (Exception exception)
        {
            operationResult.State = false;
            operationResult.Exception = exception;
            this.RollbackTransaction();
        }

        return operationResult;
    }

    public DataTable GetDefinitionLanguage(int definitionId)
    {
        SqlSelectFactory sqlSelectFactory = DefDetailSchemaSql.GetDefinitionLanguage();

        sqlSelectFactory.AddParam(new DatabaseParameter
        {
            FieldName = Definition.Field.DefinitionId,
            DbType = DbType.Int32,
            ParameterDirection = ParameterDirection.Input,
            FieldValue = definitionId
        });

        return this.ExecuteSQLDataTable(sqlSelectFactory);
    }

    public DataRow GetDefinitionWithoutLanguage(int definationId)
    {
        SqlSelectFactory sqlSelectFactory = DefDetailSchemaSql.GetDefinitionWithoutLanguage();

        sqlSelectFactory.AddParam(new DatabaseParameter
        {
            FieldName = Definition.Field.DefinitionId,
            DbType = DbType.Int32,
            ParameterDirection = ParameterDirection.Input,
            FieldValue = definationId
        });

        return this.ExecuteSQLDataRow(sqlSelectFactory);
    }


    public OperationResult DeleteDefDetailSchema(DefDetailSchema defDetailSchema)
    {
        OperationResult operationResult = new OperationResult();

        try
        {
            this.BeginTransaction();

            new DefDetailSchemaDMLOperations(this).DeleteDefDetailSchema(defDetailSchema);

            if (string.IsNullOrEmpty(defDetailSchema.DataSourceServicePath) == false)
            {
                new GeneralDMLOperations(this).ExecuteNonQuery(DefDetailSchemaSql.DeleteFkOfRowOnDefinitionTable(defDetailSchema));
            }

            new GeneralDMLOperations(this).ExecuteNonQuery(DefDetailSchemaSql.DeleteRowOnDefinitionTable(defDetailSchema));

            operationResult.State = this.CommitTransaction();
        }
        catch (Exception exception)
        {
            operationResult.State = false;
            operationResult.Exception = exception;
            this.RollbackTransaction();
        }

        return operationResult;
    }

    public int GetNextOrderNumberOfDefinitionSchema(string defName)
    {
        DefDetailSchemaSql defDetailSchemaSql = new DefDetailSchemaSql();

        SqlSelectFactory sqlSelectFactory = defDetailSchemaSql.GetNextOrderNumberOfDefinitionSchema();

        sqlSelectFactory.AddParam(new DatabaseParameter
        {
            FieldName = DefDetailSchema.Field.DefName,
            DbType = DbType.String,
            FieldValue = defName
        });

        int returnedOrderNumber = this.ExecuteScalar(sqlSelectFactory).AsInt();

        return returnedOrderNumber + 1;
    }

    public OperationResult SaveDefDetailSchema(DefDetailSchema defDetailSchema, bool addNewRowOnDefinitionTable = true)
    {
        OperationResult operationResult = new OperationResult();

        try
        {
            this.BeginTransaction();

            bool newRecord = defDetailSchema.DefSchemaId == 0;

            new DefDetailSchemaDMLOperations(this).SaveDefDetailSchema(defDetailSchema);

            if (newRecord && defDetailSchema.IsPk == false)
            {
                if (addNewRowOnDefinitionTable)
                {
                    new GeneralDMLOperations(this).ExecuteNonQuery(DefDetailSchemaSql.CreateNewColumnOnDefinitionTable(defDetailSchema));
                }

                if ((defDetailSchema.EnmInterfaceComponentType == Utility.Enum.GeneralEnums.InterfaceComponentType.Combo
                     || defDetailSchema.EnmInterfaceComponentType == Utility.Enum.GeneralEnums.InterfaceComponentType.DefianableCombo)
                    && string.IsNullOrEmpty(defDetailSchema.DataSourceServicePath) == false)
                {
                    string pkColumnName = new GeneralDMLOperations(this).
                        ExecuteScalar(DefDetailSchemaSql.GetPkColumnNameOfTable(defDetailSchema.DataSourceServicePath)).ToString();

                    new GeneralDMLOperations(this).ExecuteNonQuery(DefDetailSchemaSql.AddConstraintForeignKey(defDetailSchema, pkColumnName));
                }
            }

            operationResult.State = this.CommitTransaction();
        }
        catch (Exception exception)
        {
            operationResult.State = false;
            operationResult.Exception = exception;
            this.RollbackTransaction();
        }

        return operationResult;
    }
}