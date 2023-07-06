using Scan2Go.DataLayer.BaseClasses.SelectOperationBases;
using Scan2Go.DataLayer.DefDetailSchemaDataLayer;
using Scan2Go.Entity.Definitions;
using Scan2Go.Enums;
using System.Data;
using Utility;
using Utility.Bases.EntityBases.Facade;
using Utility.Core;
using Utility.Enum;
using Utility.Enum.GeneralEnums;
using Utility.Extensions;

namespace Scan2Go.Facade;

public class DefDetailSchemaFacade : FacadeBase
{
    public DefDetailSchemaFacade(Utility.Enum.LanguageEnum languageEnum) : base(languageEnum)
    {
    }

    public int CheckIfDefinitionFieldNameIsAlreadyDefinedInTheSystem(string fieldName, string defName)
    {
        return new DefDetailSchemaDAO().CheckIfDefinitionFieldNameIsAlreadyDefinedInTheSystem(fieldName, defName);
    }

    public int CheckIfDefinitionNameIsAlreadyDefinedInTheSystem(string definitionName)
    {
        return new DefDetailSchemaDAO().CheckIfDefinitionNameIsAlreadyDefinedInTheSystem(definitionName);
    }

    public OperationResult CreateNewDefinitionTable(DefinitionTableCreation definitionTableCreation)
    {
        OperationResult operationResult = new DefDetailSchemaDAO().CreateNewDefinitionTable(definitionTableCreation);

        if (operationResult.State)
        {
            new DefinitionFacade(this.languageEnum).ResetDefinitionCache(TableName.Def_Definitions);
        }

        return operationResult;
    }

    public OperationResult DeleteDefDetailSchema(DefDetailSchema defDetailSchema)
    {
        return new DefDetailSchemaDAO().DeleteDefDetailSchema(defDetailSchema);
    }

    public void GetAndSetSubSchemas(DefDetailSchema defDetailSchema)
    {
        //if (defDetailSchema.SubFieldSchemaId != 0)
        //{
        //    defDetailSchema.SubSchemaObject = GetDefDetailSchema(defDetailSchema.SubFieldSchemaId);
        //}
        //else
        //{
        defDetailSchema.SubSchemaObject = new DefDetailSchema();
        //}
    }

    public DefDetailSchema GetDefDetailSchema(int defDetailSchemaId)
    {
        DataRow dataRow = new Scan2GoSelectOperations(null).GetEntityDataRow<DefDetailSchema>(defDetailSchemaId);

        return FillDetailSchema(dataRow);
    }

    public IList<DefDetailSchema> GetDefDetailSchemaList(DefDetailSchema requestedDefDetailSchema)
    {
        DataTable dataTable = new Scan2GoSelectOperations().GetEntityByAnotherColumnDataTable<DefDetailSchema>(DefDetailSchema.Field.DefName,
            requestedDefDetailSchema.DefName, DbType.String);

        IList<DefDetailSchema> defDetailSchemas = new List<DefDetailSchema>();

        foreach (DataRow dataRow in dataTable.Rows)
        {
            defDetailSchemas.Add(FillDetailSchema(dataRow));
        }

        RemovePkSchemaMemberFromSchemas(requestedDefDetailSchema, defDetailSchemas);

        return defDetailSchemas.OrderBy(p => p.OrderNumber).ToList();
    }

    public IList<DefDetailSchema> GetDefDetailSchemaListOfDefinition(Definition definition)
    {
        DataTable dataTable = new Scan2GoSelectOperations().GetEntityByAnotherColumnDataTable<DefDetailSchema>(DefDetailSchema.Field.DefName,
            definition.DefinitionTableName, DbType.String);

        IList<DefDetailSchema> defDetailSchemas = new List<DefDetailSchema>();

        foreach (DataRow dataRow in dataTable.Rows)
        {
            defDetailSchemas.Add(FillDetailSchema(dataRow));
        }

        return defDetailSchemas.OrderBy(p => p.OrderNumber).ToList();
    }

    public DefinitionTableCreation GetDefinitionTable(int definationId)
    {
        DataTable definitionTable = new DefDetailSchemaDAO().GetDefinitionLanguage(definationId);
        DataRow definitionTableWithoutLanguage = new DefDetailSchemaDAO().GetDefinitionWithoutLanguage(definationId);

        return FillDefinitionTable(definitionTable, definitionTableWithoutLanguage);
    }

    public int GetNextOrderNumberOfDefinitionSchema(string defName)
    {
        return new DefDetailSchemaDAO().GetNextOrderNumberOfDefinitionSchema(defName);
    }

    public OperationResult SaveDefDetailSchema(DefDetailSchema defDetailSchema)
    {
        return new DefDetailSchemaDAO().SaveDefDetailSchema(defDetailSchema);
    }

    public OperationResult UpdateDefinitionTable(DefinitionTableCreation definitionTableCreation)
    {
        OperationResult operationResult = new DefDetailSchemaDAO().UpdateDefinitionTable(definitionTableCreation);

        if (operationResult.State)
        {
            new DefinitionFacade(this.languageEnum).ResetDefinitionCache(TableName.Def_Definitions);
        }

        return operationResult;
    }
    private DefinitionTableCreation FillDefinitionTable(DataTable definitionTable, DataRow definitionTableWithoutLanguage)
    {
        if (definitionTable == null)
        {
            return null;
        }

        DefinitionTableCreation definitionTableCreation = new DefinitionTableCreation();

        foreach (DataRow dataRow in definitionTable.Rows)
        {
            if (Convert.ToInt32(dataRow["SystemLanguageId"]) == (int)LanguageEnum.EN)
            {
                definitionTableCreation.EnglishName = dataRow["NameField"].ToString();
            }
            else if (Convert.ToInt32(dataRow["SystemLanguageId"]) == (int)LanguageEnum.AR)
            {
                definitionTableCreation.ArabicName = dataRow["NameField"].ToString();
            }
            else if (Convert.ToInt32(dataRow["SystemLanguageId"]) == (int)LanguageEnum.TR)
            {
                definitionTableCreation.TurkishName = dataRow["NameField"].ToString();
            }
            else if (Convert.ToInt32(dataRow["SystemLanguageId"]) == (int)LanguageEnum.AZ)
            {
                definitionTableCreation.AzerbaijaniName = dataRow["NameField"].ToString();
            }
        }

        definitionTableCreation.IsActive = definitionTableWithoutLanguage["IsActive"].AsBool();
        definitionTableCreation.IsDefinable = definitionTableWithoutLanguage["IsDefinable"].AsBool();
        definitionTableCreation.IsLanguageIndependent = definitionTableWithoutLanguage["IsLanguageIndependent"].AsBool();
        definitionTableCreation.DefinitionId = definitionTableWithoutLanguage["DefinitionId"].AsInt();

        return definitionTableCreation;
    }
    private DefDetailSchema FillDetailSchema(DataRow row)
    {
        if (row == null) { return null; }

        var defDetailSchema = new DefDetailSchema();

        defDetailSchema.DefSchemaId = row.AsInt(DefDetailSchema.Field.DefSchemaId);
        defDetailSchema.DataSourceServicePath = row.AsString(DefDetailSchema.Field.DataSourceServicePath);
        defDetailSchema.DefName = row.AsString(DefDetailSchema.Field.DefName);
        defDetailSchema.FieldName = row.AsString(DefDetailSchema.Field.FieldName);
        defDetailSchema.FieldInterfaceLabel = EnumMethods.GetResourceString(row.AsString(DefDetailSchema.Field.FieldName), this.languageEnum);
        defDetailSchema.EnmInterfaceComponentType = (InterfaceComponentType)row.AsInt(DefDetailSchema.Field.FieldTypeId, true);
        defDetailSchema.InterfaceComponentTypeName = EnumMethods.GetResourceString(defDetailSchema.EnmInterfaceComponentType.ToString(), this.languageEnum);
        defDetailSchema.SubFieldSchemaId = row.AsInt(DefDetailSchema.Field.SubFieldSchemaId);
        defDetailSchema.IsLabelVisible = row.AsBool(DefDetailSchema.Field.IsLabelVisible);
        defDetailSchema.IsPk = row.AsBool(DefDetailSchema.Field.IsPk);
        defDetailSchema.IsParent = row.AsBool(DefDetailSchema.Field.IsParent);
        defDetailSchema.IsMultiLanguage = row.AsBool(DefDetailSchema.Field.IsMultiLanguage);
        defDetailSchema.IsNameField = row.AsBool(DefDetailSchema.Field.IsNameField);
        defDetailSchema.Length = row.AsInt(DefDetailSchema.Field.Length);
        defDetailSchema.EnmMandatoryStatus = (MandatoryStatus)row.AsInt("MandatoryStatusId", true);
        defDetailSchema.MandatoryStatusName = EnumMethods.GetResourceString(defDetailSchema.EnmMandatoryStatus.ToString(), this.languageEnum);
        defDetailSchema.MessageText = row.AsString(DefDetailSchema.Field.MessageText);
        defDetailSchema.OrderNumber = row.AsInt(DefDetailSchema.Field.OrderNumber);

        if (defDetailSchema.FieldName == DefDetailSchema.Field.IsActive)
        {
            defDetailSchema.FieldValue = true;
        }

        if (defDetailSchema.IsParent)
        {
            if (defDetailSchema.DataSourceServicePath.StartsWith("Def_"))
            {
                Definition parentDefinition = new DefinitionFacade(this.languageEnum).GetDefinition(defDetailSchema.DataSourceServicePath);

                if (parentDefinition != null)
                {
                    defDetailSchema.ParentDefinition = new CustomDataHandler
                    {
                        ID = parentDefinition.PrimaryKeyValue,//Buradaki ID önemli değil,sırf Comboya dolsun diye.
                        NameValue = parentDefinition.TableName
                    };
                }
            }
            else if (defDetailSchema.DataSourceServicePath.StartsWith("Enm_"))
            {
                defDetailSchema.ParentDefinition = new CustomDataHandler
                {
                    ID = 1,
                    NameValue = defDetailSchema.DataSourceServicePath.Replace("Enm_", string.Empty)
                };
            }
        }

        new DefinitionFacade(this.languageEnum).FillDataSourcesFromPath(defDetailSchema);
        GetAndSetSubSchemas(defDetailSchema);

        defDetailSchema.ChangeState(false);

        return defDetailSchema;
    }

    private void RemovePkSchemaMemberFromSchemas(DefDetailSchema defDetailSchema, IList<DefDetailSchema> defDetailSchemas)
    {
        if (defDetailSchema.ReturnPkSchemaMember)
        {
            return;
        }

        DefDetailSchema IsPkSchema = defDetailSchemas.FirstOrDefault(p => p.IsPk);

        defDetailSchemas.Remove(IsPkSchema);
    }
}