using Force.DeepCloner;
using Scan2Go.DataLayer.BaseClasses.SelectOperationBases;
using Scan2Go.DataLayer.DefinitionDataLayer;
using Scan2Go.Entity.Definitions;
using Scan2Go.Enums;
using Scan2Go.Enums.Properties;
using System.Data;
using Utility;
using Utility.Bases.EntityBases.Facade;
using Utility.Core;
using Utility.Enum;
using Utility.Extensions;

namespace Scan2Go.Facade;

public class DefinitionFacade : FacadeBase
{
    public DefinitionFacade(LanguageEnum languageEnum) : base(languageEnum)
    {
    }

    public OperationResult DeleteDefinition(Definition definition)
    {
        OperationResult operationResult = new DefinitionDAO().DeleteDefinition(definition);

        if (operationResult.State)
        {
            var languageEnumValues = Enum.GetValues(typeof(LanguageEnum));
            foreach (var language in languageEnumValues)
            {
                _cacheManager.RemoveAtKeyName(language.ToString() + "_" + definition.GetTableName().ToString().ToLower());
            }
        }
        else
        {
            if (operationResult.MessageStringKey == nameof(MessageStrings.DefinitionIsUsedAnotherRecord))
                operationResult.Message = EnumMethods.GetResourceString(operationResult.MessageStringKey, this.languageEnum);
        }

        return operationResult;
    }

    public void FillDataSourcesFromPath(DefDetailSchema defDetailSchema)
    {
        if (string.IsNullOrEmpty(defDetailSchema.DataSourceServicePath))
        {
            return;
        }

        if (defDetailSchema.DataSourceServicePath.StartsWith("Def"))
        {
            IList<Definition> dataSourceList = GetDefinitionList(defDetailSchema.DataSourceServicePath);

            foreach (Definition definition in dataSourceList)
            {
                if (defDetailSchema.DataSourceList.Any(p => p.ID == definition.PrimaryKeyValue) == false)
                {
                    defDetailSchema.DataSourceList.Add(new CustomDataHandler()
                    {
                        ID = definition.PrimaryKeyValue,
                        NameValue = definition.NameValue,
                        ParentPkId = definition.ParentDefinitionPkValue
                    });
                }
            }
        }
        else if (defDetailSchema.DataSourceServicePath.StartsWith("Enm") && defDetailSchema.DataSourceList.Any() == false)
        {
            string enumName = defDetailSchema.DataSourceServicePath.Split('_')[1];
            Type enumType = enumName.GetEnumType();
            defDetailSchema.DataSourceList = new EnumFacade(this.languageEnum).GetEnum(enumType);
        }
        //else if (defDetailSchema.DataSourceServicePath.StartsWith("Users"))
        //{
        //    IList<UserBrief> userBriefs = new UsersFacade(this.languageEnum).GetUserListByRoles(UserRole.Expert, UserRole.Technician);

        //    foreach (UserBrief userBrief in userBriefs)
        //    {
        //        defDetailSchema.DataSourceList.Add(new CustomDataHandler()
        //        {
        //            ID = userBrief.UserId,
        //            NameValue = userBrief.FullName
        //        });
        //    }
        //}
    }

    public Definition GetDefinition(string defName)
    {
        return GetDefinitionList(defName).FirstOrDefault();
    }

    public Definition GetDefinition(int fieldValue, string defName)
    {
        Definition definition = GetDefinitionList(defName).FirstOrDefault(p => p.PrimaryKeyValue == fieldValue);

        return definition;
    }

    public IList<Definition> GetDefinitionList(string defName, bool ReturnActiveDefsOnly = false, bool overrideLanguageValueAsEnglish = false)
    {
        if (overrideLanguageValueAsEnglish)
        {
            this.languageEnum = LanguageEnum.EN;
        }

        string cacheName = languageEnum.ToString() + "_" + defName.ToLower();

        IList<Definition> definitionList = _cacheManager.GetData(cacheName) as IList<Definition>;

        if (definitionList != null)
        {
            if (ReturnActiveDefsOnly)
            {
                return definitionList.Where(p => p.IsActive).ToList();
            }

            return definitionList;
        }

        Definition getDefinition = new Definition();

        DefDetailSchema defDetailSchemaOfDefinition = new DefDetailSchema { DefName = defName };

        IList<DefDetailSchema> defDetailSchemas = new DefDetailSchemaFacade(this.languageEnum).GetDefDetailSchemaList(defDetailSchemaOfDefinition);

        //foreach (DefDetailSchema defDetailSchema in defDetailSchemas)
        //{
        //    if (defDetailSchema.DataSourceServicePath.StartsWith("Enm"))
        //    {
        //        string enumName = defDetailSchema.DataSourceServicePath.Split('_')[1];
        //        Type enumType = enumName.GetEnumType();
        //        defDetailSchema.DataSourceList = new EnumFacade(this.languageEnum).GetEnum(enumType);
        //    }
        //}

        getDefinition.defDetailSchemas = defDetailSchemas;

        DataTable defDataTable = new Scan2GoSelectOperations(null).GetDefinitionDataTable(getDefinition, languageEnum, getDefinition.DoesHaveANameField);

        definitionList = new List<Definition>();

        foreach (DataRow definitionDetailRow in defDataTable.Rows)
        {
            foreach (DataColumn dc in definitionDetailRow.Table.Columns)
            {
                DefDetailSchema defDetailSchema = new DefDetailSchema();

                if (PrimitiveExtensions.CurrentDataBaseDetection() == "ORACLE")
                {
                    foreach (var item in defDetailSchemas)
                    {
                        item.FieldName = item.FieldName.ToUpperInvariant();
                        defDetailSchema = defDetailSchemas.FirstOrDefault(p => p.FieldName.Equals(dc.ColumnName.ToUpperInvariant()));
                    }
                }
                else
                {
                    defDetailSchema = defDetailSchemas.FirstOrDefault(p => p.FieldName.Equals(dc.ColumnName));
                }

                if (defDetailSchema == null)
                {
                    continue;
                }

                FillDataSourcesFromPath(defDetailSchema);

                defDetailSchema.FieldValue = definitionDetailRow[dc] ?? "";

                if (defDetailSchema.EnmInterfaceComponentType == Utility.Enum.GeneralEnums.InterfaceComponentType.Textbox
                    && string.IsNullOrEmpty(defDetailSchema.FieldValue.ToString()))
                {
                    defDetailSchema.FieldValue = string.Empty;
                }
            }

            Definition definition = new Definition();

            definition.defDetailSchemas = defDetailSchemas.DeepClone();
            definition.TableName = definitionDetailRow.AsString(Definition.Field.TableName);

            DataColumnCollection columns = defDataTable.Columns;

            if (columns.Contains(Definition.Field.IsDefinable))
            {
                definition.IsDefinable = definitionDetailRow.AsBool(Definition.Field.IsDefinable);
            }

            if (columns.Contains(Definition.Field.IsLanguageIndependent))
            {
                definition.IsLanguageIndependent = definitionDetailRow.AsBool(Definition.Field.IsLanguageIndependent);
            }

            //if (defName.ToLower().Equals(TableName.Def_Definitions.ToString().ToLower()))
            //{
            DataRow mainPropertiesOfDefinitionDataRow = new DefinitionDAO().GetMainPropertiesOfDefinition(defName);

            definition.IsDefinable = mainPropertiesOfDefinitionDataRow.AsBool(Definition.Field.IsDefinable);
            definition.IsLanguageIndependent = mainPropertiesOfDefinitionDataRow.AsBool(Definition.Field.IsLanguageIndependent);
            definition.DefaultValueId = mainPropertiesOfDefinitionDataRow.AsInt(Definition.Field.DefaultValueId);
            //}

            if (string.IsNullOrEmpty(definition.TableName))
            {
                definition.TableName = defName;
            }

            foreach (DefDetailSchema defDetailSchema in definition.defDetailSchemas)
            {
                if (string.IsNullOrEmpty(defDetailSchema.DataSourceServicePath) == false && defDetailSchema.DataSourceServicePath.StartsWith("Def_") &&
                    string.IsNullOrEmpty(Convert.ToString(defDetailSchema.FieldValue)) == false)
                {
                    Definition parentDefinition = GetDefinition(defDetailSchema.FieldValue.AsInt(), defDetailSchema.DataSourceServicePath);

                    if (parentDefinition != null)
                    {
                        /*This block is entended to Frontend, the value must be an object of custom data handeler */
                        CustomDataHandler customDataHandler = new CustomDataHandler();
                        customDataHandler.ID = parentDefinition.PrimaryKeyValue;
                        customDataHandler.NameValue = parentDefinition.DefinitionName;
                        defDetailSchema.FieldValue = customDataHandler;
                        /******************************************************************************************/

                        if (defDetailSchema.IsParent)
                        {
                            definition.ParentDefinition = parentDefinition;
                            definition.defDetailSchemas.FirstOrDefault(p => p.IsParent).FieldValue = customDataHandler;
                        }
                    }
                }
                else if (string.IsNullOrEmpty(defDetailSchema.DataSourceServicePath) == false && defDetailSchema.DataSourceServicePath.StartsWith("Enm_") &&
                         string.IsNullOrEmpty(Convert.ToString(defDetailSchema.FieldValue)) == false)
                {
                    defDetailSchema.FieldValue = defDetailSchema.DataSourceList.FirstOrDefault(p => p.ID == defDetailSchema.FieldValue.AsInt());
                }
                else if (string.IsNullOrEmpty(defDetailSchema.DataSourceServicePath) == false && defDetailSchema.DataSourceServicePath.StartsWith("Users") &&
                         string.IsNullOrEmpty(Convert.ToString(defDetailSchema.FieldValue)) == false)
                {
                    defDetailSchema.FieldValue = defDetailSchema.DataSourceList.FirstOrDefault(p => p.ID == defDetailSchema.FieldValue.AsInt());
                }
            }

            definitionList.Add(definition);
        }

        definitionList = definitionList.OrderBy(p => p.NameValue).ToList();

        _cacheManager.SetData(cacheName, definitionList);

        if (ReturnActiveDefsOnly)
        {
            return definitionList.Where(p => p.IsActive).ToList();
        }

        return definitionList;
    }
    public void ResetDefinitionCache(string definitionTableName)
    {
        var languageEnumValues = Enum.GetValues(typeof(LanguageEnum));
        foreach (var language in languageEnumValues)
        {
            _cacheManager.RemoveAtKeyName(language + "_" + definitionTableName.ToLower());
        }
    }

    public OperationResult SaveDefinition(Definition definitionToBeSaved)
    {
        OperationResult operationResult = new DefinitionDAO().SaveDefinition(definitionToBeSaved);

        if (operationResult.State)
        {
            ResetDefinitionCache(definitionToBeSaved.GetTableName());
        }

        return operationResult;
    }
}