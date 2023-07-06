using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Utility.Bases.EntityBases.DefinitionBases;
using Utility.Core.DataLayer;
using Utility.Enum;
using Utility.Extensions;
using Utility;

namespace Scan2Go.Entity.Definitions
{
    [Serializable]
    public class Definition : DefinitionBase
    {
        #region FieldNames

        public class Field : DefinitionFieldEntityBase
        {
            public const string DefaultValueId = "DefaultValueId";
            public const string DefinitionId = "DefinitionId";
            public const string IsActiveFieldName = "IsActive";
            public const string IsDefinable = "IsDefinable";
            public const string IsLanguageIndependent = "IsLanguageIndependent";
            public const string LanguageTableExtention = "Language";
            public const string NameField = "NameField";
            public const string SystemLanguageId = "SystemLanguageId";
            public const string TableName = "TableName";
            public const string Colour = "Colour";
            public const string ArabicName = "ArabicName";
            public const string EnglishName = "EnglishName";
            public const string TurkishName = "TurkishName";

        }

        #endregion FieldNames

        public IList<DefDetailSchema> defDetailSchemas = new List<DefDetailSchema>();

        #region Other Properties

        /// <summary>
        /// Used to determine the user's current language when updating a definition
        /// </summary>
        public LanguageEnum currentInterfaceLanguage { get; set; }

        public int UIElementPropertiesId { get; set; }

        public int DivisionId
        {
            get
            {
                if (IsDivisionSpecific)
                {
                    var fieldValue = this.defDetailSchemas.FirstOrDefault(p => p.FieldName.Equals("DivisionId") || p.FieldName.Equals("DivId")).FieldValue;

                    if (fieldValue is CustomDataHandler)
                    {
                        CustomDataHandler divisionCustomDataHandler = (CustomDataHandler)this.defDetailSchemas.FirstOrDefault(p =>
                            p.FieldName.Equals("DivisionId") || p.FieldName.Equals("DivId")).FieldValue;

                        return divisionCustomDataHandler.ID;
                    }

                    return this.defDetailSchemas.FirstOrDefault(p => p.FieldName.Equals("DivisionId") || p.FieldName.Equals("DivId")).FieldValue.AsInt();
                }

                return 0;
            }
            set
            {
                if (IsDivisionSpecific)
                {
                    this.defDetailSchemas.FirstOrDefault(p => p.FieldName.Equals("DivisionId") || p.FieldName.Equals("DivId")).FieldValue = value;
                }
            }
        }

        public bool DoesBelongToParent { get { return defDetailSchemas.Any(p => p.IsParent); } }
        public bool DoesHaveANameField { get { return this.defDetailSchemas.Any(p => p.IsNameField); } }
        public bool IsDefinable { get; set; } = true;
        public bool IsDivisionSpecific { get { return this.defDetailSchemas.Any(p => p.FieldName.Equals("DivisionId") || p.FieldName.Equals("DivId")); } }
        public bool IsLabSpecific { get { return this.defDetailSchemas.Any(p => p.FieldName.Equals("LaboratoryId") || p.FieldName.Equals("LabId")); } }
        public bool IsLanguageIndependent { get; set; }
        public int DefaultValueId { get; set; }
        public string Colour { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public string TurkishName { get; set; }

        public int LabId
        {
            get
            {
                if (IsLabSpecific)
                {
                    var fieldValue = this.defDetailSchemas.FirstOrDefault(p => p.FieldName.Equals("LaboratoryId") || p.FieldName.Equals("LabId")).FieldValue;

                    if (fieldValue is CustomDataHandler)
                    {
                        CustomDataHandler labCustomDataHandler = (CustomDataHandler)this.defDetailSchemas.FirstOrDefault(p =>
                            p.FieldName.Equals("LaboratoryId") || p.FieldName.Equals("LabId")).FieldValue;

                        return labCustomDataHandler.ID;
                    }

                    return this.defDetailSchemas.FirstOrDefault(p => p.FieldName.Equals("LaboratoryId") || p.FieldName.Equals("LabId")).FieldValue.AsInt();
                }

                return 0;
            }
            set
            {
                if (IsLabSpecific)
                {
                    this.defDetailSchemas.FirstOrDefault(p => p.FieldName.Equals("LaboratoryId") || p.FieldName.Equals("LabId")).FieldValue = value;
                }
            }
        }

        public string TableName { get; set; }

        #endregion Other Properties

        #region Parent Definition

        public Definition ParentDefinition { get; set; }

        public string ParentDefinitionName
        {
            get
            {
                if (ParentDefinition == null)
                {
                    return string.Empty;
                }

                var nameField = ParentDefinition.defDetailSchemas.FirstOrDefault(p => p.FieldName.Equals(Field.NameField));
                if (nameField != null)
                {
                    return nameField.FieldValue.ToString();
                }

                nameField = ParentDefinition.defDetailSchemas.FirstOrDefault(p => p.FieldName.ToLower().EndsWith("name"));
                if (nameField != null)
                {
                    return nameField.FieldValue.ToString();
                }

                return ParentDefinition.defDetailSchemas.FirstOrDefault().DefName;
            }
        }

        public int ParentDefinitionPkValue
        {
            get
            {
                if (ParentDefinition == null)
                {
                    return 0;
                }

                return ParentDefinition.defDetailSchemas.FirstOrDefault(p => p.IsPk).FieldValue.AsInt();
            }
        }

        #endregion Parent Definition

        #region Definition Base Members

        public new bool IsActive
        {
            get
            {
                var isActiveField = defDetailSchemas.FirstOrDefault(p => p.FieldName.Equals(Field.IsActiveFieldName));

                if (isActiveField != null && string.IsNullOrEmpty(Convert.ToString(isActiveField.FieldValue)) == false && isActiveField.FieldValue != DBNull.Value)
                {
                    return Convert.ToBoolean(isActiveField.FieldValue);
                }

                return true;
            }
        }

        public string ListNameValue
        {
            get
            {
                string nameValue = string.Empty;

                DefDetailSchema item = defDetailSchemas.FirstOrDefault(p => p.IsNameField);

                if (item != null)
                {
                    nameValue = Convert.ToString(item.FieldValue);
                }

                return nameValue;
            }
        }

        public override string NameValue
        {
            get
            {
                string nameValue = string.Empty;

                foreach (DefDetailSchema item in defDetailSchemas.Where(p => p.IsNameField).OrderBy(p => p.OrderNumber))
                {
                    if (Convert.ToString(item.FieldValue).Length > 0)
                    {
                        nameValue += Convert.ToString(item.FieldValue) + " - ";
                    }
                }

                return nameValue.Trim().TrimEnd('-').Trim();
            }
            set { }
        }

        public override int PrimaryKeyValue
        {
            get
            {
                if (PkValue == 0)
                {
                    if (defDetailSchemas.Count > 0)
                    {
                        PkValue = defDetailSchemas.FirstOrDefault(p => p.IsPk).FieldValue.AsInt();
                    }
                }

                return PkValue;
            }
            set
            {
                PkValue = value;
            }
        }

        public string TableNameWithoutAnalysis
        {
            get
            {
                return TableName?.Replace("Analysis_", string.Empty);
            }
        }

        public string TableNameWithoutEvd
        {
            get
            {
                return TableName?.Replace("Evd_", string.Empty);
            }
        }

        public override DataLayerEnumBase GetCacheKey()
        {
            return new DataLayerEnumBase(defDetailSchemas.FirstOrDefault(p => p.IsPk).DefName);
        }

        public override List<DatabaseParameter> GetEntityDbParameters(bool isStateCheck = true)
        {
            List<DatabaseParameter> databaseParameters = new List<DatabaseParameter>();

            foreach (DefDetailSchema defDetailSchema in defDetailSchemas)
            {
                //if (defDetailSchema.FieldValue is Definition)
                //{
                //    databaseParameters.Add(new DatabaseParameter
                //    {
                //        IsPrimaryKey = defDetailSchema.IsPk,
                //        FieldName = defDetailSchema.FieldName,
                //        DbType = new DbType().DecideOnDbType(defDetailSchema.FieldTypeId),
                //        FieldValue = ((Definition)defDetailSchema.FieldValue).PrimaryKeyValue
                //    });
                //}
                //else
                //{

                databaseParameters.Add(new DatabaseParameter
                {
                    IsPrimaryKey = defDetailSchema.IsPk,
                    FieldName = defDetailSchema.FieldName,
                    DbType = new DbType().DecideOnDbType(defDetailSchema.EnmInterfaceComponentType.AsInt()),
                    FieldValue = defDetailSchema.FieldValue
                });
                //}
            }

            return databaseParameters;
        }

        public List<DatabaseParameter> GetEntityPkAndStringDbParameters(bool isStateCheck = true)
        {
            if (defDetailSchemas.Any(p => p.IsNameField) == false)
            {
                return null;
            }

            List<DatabaseParameter> databaseParameters = new List<DatabaseParameter>();

            foreach (DefDetailSchema defDetailSchema in defDetailSchemas.Where(p => p.IsPk || p.IsNameField || p.IsMultiLanguage))
            {
                databaseParameters.Add(new DatabaseParameter
                {
                    IsPrimaryKey = defDetailSchema.IsPk,
                    FieldName = defDetailSchema.FieldName,
                    DbType = new DbType().DecideOnDbType(defDetailSchema.EnmInterfaceComponentType.AsInt()),
                    FieldValue = defDetailSchema.FieldValue
                });
            }

            return databaseParameters;
        }

        public override DataLayerEnumBase GetPrimaryKeyName()
        {
            return new DataLayerEnumBase(defDetailSchemas.FirstOrDefault(p => p.IsPk).FieldName, defDetailSchemas.FirstOrDefault(p => p.IsPk).FieldValue.AsInt());
        }

        public override DataLayerEnumBase GetTableName()
        {
            return new DataLayerEnumBase(defDetailSchemas.FirstOrDefault(p => p.IsPk).DefName);
        }

        #endregion Definition Base Members

        #region Mapping Methods ,for mapping with model , better naming

        //public Hashtable ColumnList { get { return GetColumList(); } }
        public string DefinitionName { get { return NameValue; } }

        public string DefinitionTableName { get { return GetTableName(); } }

        public string NotificationMessage
        {
            get
            {
                var message = defDetailSchemas.FirstOrDefault(t => t.FieldName == "NotificationText"); //notificationMessage text olmalı

                if (message != null && message.FieldValue != null)
                {
                    return message.FieldValue.ToString();
                }

                return string.Empty;
            }
        }

        public bool ShowNotification
        {
            get
            {
                var messageType = defDetailSchemas.FirstOrDefault(t => t.FieldName == "ShowNotification");

                if (messageType == null || messageType.FieldValue == null)
                {
                    return false;
                }

                return messageType.FieldValue.AsBool();
            }
        }

        public DateTime? ExpiyrDate
        {
            get
            {
                var expiyrDate = defDetailSchemas.FirstOrDefault(t => t.FieldName == "ExpiyrDate");

                if (expiyrDate == null || expiyrDate.FieldValue == null)
                {
                    return null;
                }

                return expiyrDate.FieldValue.AsDateTime();
            }
        }

        public object GetDetailValueByContainingFieldName(string fieldName)
        {
            DefDetailSchema defDetailSchema = defDetailSchemas.FirstOrDefault(p => p.FieldName.ToLower().Contains(fieldName));

            if (defDetailSchema == null)
            {
                return null;
            }

            if (defDetailSchema.FieldValue is CustomDataHandler customDataHandler)
            {
                return customDataHandler.ID;
            }

            return defDetailSchema.FieldValue;
        }

        public object GetDetailValueByExactFieldName(string fieldName)
        {

            if (PrimitiveExtensions.CurrentDataBaseDetection() == "ORACLE")
            {
                DefDetailSchema defDetailSchema = defDetailSchemas.FirstOrDefault(p => p.FieldName.Equals(fieldName.ToUpperInvariant()));

                if (defDetailSchema == null)
                {
                    return null;
                }

                if (defDetailSchema.FieldValue is CustomDataHandler customDataHandler)
                {
                    return customDataHandler.ID;
                }

                if (defDetailSchema.FieldValue is JObject jObject)
                {
                    return jObject.GetValue("id");
                }

                return defDetailSchema.FieldValue;
            }
            else
            {
                DefDetailSchema defDetailSchema = defDetailSchemas.FirstOrDefault(p => p.FieldName.Equals(fieldName));

                if (defDetailSchema == null)
                {
                    return null;
                }

                if (defDetailSchema.FieldValue is CustomDataHandler customDataHandler)
                {
                    return customDataHandler.ID;
                }

                if (defDetailSchema.FieldValue is JObject jObject)
                {
                    return jObject.GetValue("id");
                }

                return defDetailSchema.FieldValue;
            }

        }

        #endregion Mapping Methods ,for mapping with model , better naming

        private int PkValue { get; set; }
    }
}
