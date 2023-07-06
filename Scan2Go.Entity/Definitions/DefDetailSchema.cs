using Scan2Go.Enums;
using System.Data;
using Utility;
using Utility.Bases.EntityBases;
using Utility.Bases.EntityBases.DefinitionBases;
using Utility.Core.DataLayer;
using Utility.Enum.GeneralEnums;
using Utility.Extensions;

namespace Scan2Go.Entity.Definitions
{
    [Serializable]
    public class DefDetailSchema : EntityStateBase
    {
        #region FieldNames

        public class Field : DefinitionFieldEntityBase
        {
            public const string DataSourceServicePath = "DataSourceServicePath";
            public const string DefName = "DefName";
            public const string DefSchemaId = "DefSchemaId";
            public const string FieldName = "FieldName";
            public const string FieldTypeId = "FieldTypeId";
            public const string IsLabelVisible = "IsLabelVisible";
            public const string IsMultiLanguage = "IsMultiLanguage";
            public const string IsNameField = "IsNameField";
            public const string IsParent = "IsParent";
            public const string IsPk = "IsPk";
            public const string Length = "Length";
            public const string MandatoryStatusId = "MandatoryStatusId";
            public const string MessageText = "MessageText";
            public const string OrderNumber = "OrderNumber";
            public const string SubFieldSchemaId = "SubFieldSchemaId";
        }

        #endregion FieldNames

        #region Privates of DetailSchema

        private string _dataSourceServicePath;
        private string _defName;
        private int _defSchemaId;
        private InterfaceComponentType _enmInterfaceComponentType;
        private MandatoryStatus _enmMandatoryStatus;
        private string _fieldName = string.Empty;
        private object _fieldValue;
        private bool _isLabelVisible;
        private bool _isMultiLanguage;
        private bool _isNameField;
        private bool _isParent;
        private bool _isPk;
        private int _length;
        private string _messageText;
        private int _orderNumber;
        private DetailSchemaState _stateDetailSchema;
        private int _subFieldSchemaId;

        protected override IState _state
        {
            get { return _stateDetailSchema; }
        }

        #endregion Privates of DetailSchema

        #region Constructor of DetailSchema

        public DefDetailSchema()
        {
            _stateDetailSchema = new DetailSchemaState();
        }

        public DefDetailSchema(string defName, object fieldValue)
        {
            _stateDetailSchema = new DetailSchemaState();
            this.FieldValue = fieldValue;
            this.DefName = defName;
        }

        #endregion Constructor of DetailSchema

        #region Properties of DetailSchema

        public IList<CustomDataHandler> DataSourceList { get; set; } = new List<CustomDataHandler>();
        public string DataSourceServicePath
        { get { return _dataSourceServicePath; } set { if (_dataSourceServicePath != value) { _dataSourceServicePath = value; _stateDetailSchema.DataSourceServicePath = true; } } }
        public string DefName
        { get { return _defName; } set { if (_defName != value) { _defName = value; _stateDetailSchema.DefName = true; } } }
        public int DefSchemaId
        { get { return _defSchemaId; } set { _defSchemaId = value; } }
        public InterfaceComponentType EnmInterfaceComponentType
        { get { return _enmInterfaceComponentType; } set { if (_enmInterfaceComponentType.Equals(value) == false) { _enmInterfaceComponentType = value; _stateDetailSchema.FieldTypeId = true; } } }
        public MandatoryStatus EnmMandatoryStatus
        { get { return _enmMandatoryStatus; } set { if (_enmMandatoryStatus.Equals(value) == false) { _enmMandatoryStatus = value; _stateDetailSchema.MandatoryStatusId = true; } } }
        public string FieldName
        { get { return _fieldName; } set { if (_fieldName != value) { _fieldName = value; _stateDetailSchema.FieldName = true; } } }
        public object FieldValue
        { get { return _fieldValue; } set { if (_fieldValue != value) { _fieldValue = value; _stateDetailSchema.FieldValue = true; } } }
        public bool IsLabelVisible
        { get { return _isLabelVisible; } set { if (_isLabelVisible != value) { _isLabelVisible = value; _stateDetailSchema.IsLabelVisible = true; } } }
        public bool IsMultiLanguage
        { get { return _isMultiLanguage; } set { if (_isMultiLanguage != value) { _isMultiLanguage = value; _stateDetailSchema.IsMultiLanguage = true; } } }
        public bool IsNameField
        { get { return _isNameField; } set { if (_isNameField != value) { _isNameField = value; _stateDetailSchema.IsNameField = true; } } }
        public bool IsParent
        { get { return _isParent; } set { if (_isParent != value) { _isParent = value; _stateDetailSchema.IsParent = true; } } }
        public bool IsPk
        { get { return _isPk; } set { if (_isPk != value) { _isPk = value; _stateDetailSchema.IsPk = true; } } }
        public int Length
        { get { return _length; } set { if (_length != value) { _length = value; _stateDetailSchema.Length = true; } } }
        public string MessageText
        { get { return _messageText; } set { if (_messageText != value) { _messageText = value; _stateDetailSchema.MessageText = true; } } }
        public int OrderNumber
        { get { return _orderNumber; } set { if (_orderNumber != value) { _orderNumber = value; _stateDetailSchema.OrderNumber = true; } } }
        public int SubFieldSchemaId
        { get { return _subFieldSchemaId; } set { if (_subFieldSchemaId != value) { _subFieldSchemaId = value; _stateDetailSchema.SubFieldSchemaId = true; } } }

        #endregion Properties of DetailSchema

        #region Other Properties

        public string FieldInterfaceLabel { get; set; }

        public object FieldValueRegardlessOfType
        {
            get
            {
                if (FieldValue is CustomDataHandler)
                {
                    return ((CustomDataHandler)FieldValue).ID;
                }

                return FieldValue;
            }
        }

        public string InterfaceComponentTypeName { get; set; }
        public string MandatoryStatusName { get; set; }
        public CustomDataHandler ParentDefinition { get; set; }
        public bool ReturnPkSchemaMember { get; set; } = true;
        public DefDetailSchema SubSchemaObject { get; set; }

        #endregion Other Properties

        #region AUTO methods

        public override int PrimaryKeyValue
        { get { return this.DefSchemaId; } set { this.DefSchemaId = value; } }

        public override List<DatabaseParameter> GetEntityDbParameters(bool isStateCheck = true)
        {
            this.ClearSqlParameters();

            this.AddParam(Field.DefSchemaId, DbType.Int32, this.DefSchemaId, ParameterDirection.Input, true);

            if (isStateCheck && _stateDetailSchema.DefName)
            {
                this.AddParam(Field.DefName, DbType.String, this.DefName);
            }

            if (isStateCheck && _stateDetailSchema.FieldName)
            {
                this.AddParam(Field.FieldName, DbType.String, this.FieldName);
            }

            if (isStateCheck && _stateDetailSchema.FieldTypeId)
            {
                this.AddParam(Field.FieldTypeId, DbType.Int32, this.EnmInterfaceComponentType.GetAsDbParam());
            }

            if (isStateCheck && _stateDetailSchema.OrderNumber)
            {
                this.AddParam(Field.OrderNumber, DbType.Int32, this.OrderNumber);
            }

            if (isStateCheck && _stateDetailSchema.Length)
            {
                this.AddParam(Field.Length, DbType.Int32, this.Length);
            }

            if (isStateCheck && _stateDetailSchema.MandatoryStatusId)
            {
                this.AddParam(Field.MandatoryStatusId, DbType.Int32, this.EnmMandatoryStatus.GetAsDbParam());
            }

            if (isStateCheck && _stateDetailSchema.MessageText)
            {
                this.AddParam(Field.MessageText, DbType.String, this.MessageText);
            }

            if (isStateCheck && _stateDetailSchema.DataSourceServicePath)
            {
                this.AddParam(Field.DataSourceServicePath, DbType.String, this.DataSourceServicePath);
            }

            this.AddParam(Field.IsLabelVisible, DbType.Boolean, this.IsLabelVisible);

            this.AddParam(Field.IsPk, DbType.Boolean, this.IsPk);

            this.AddParam(Field.IsParent, DbType.Boolean, this.IsParent);

            this.AddParam(Field.IsMultiLanguage, DbType.Boolean, this.IsMultiLanguage);

            if (isStateCheck && _stateDetailSchema.SubFieldSchemaId)
            {
                this.AddParam(Field.SubFieldSchemaId, DbType.Int32, this.SubFieldSchemaId);
            }

            this.AddParam(Field.IsNameField, DbType.Boolean, this.IsNameField);

            return this.GetSqlParameters();
        }

        public override DataLayerEnumBase GetPrimaryKeyName()
        {
            return PrimaryKey.DefSchemaId;
        }

        public override DataLayerEnumBase GetTableName()
        {
            return TableName.Def_Detail_Schema;
        }

        #endregion AUTO methods
    }

    #region State

    [Serializable]
    public class DetailSchemaState : IState
    {
        public bool DataSourceServicePath { get; set; }
        public bool DefName { get; set; }
        public bool DefSchemaId { get; set; }
        public bool FieldName { get; set; }
        public bool FieldTypeId { get; set; }
        public bool FieldValue { get; set; }
        public bool IsLabelVisible { get; set; }
        public bool IsMultiLanguage { get; set; }
        public bool IsNameField { get; set; }
        public bool IsParent { get; set; }
        public bool IsPk { get; set; }
        public bool Length { get; set; }
        public bool MandatoryStatusId { get; set; }
        public bool MessageText { get; set; }
        public bool OrderNumber { get; set; }
        public bool SubFieldSchemaId { get; set; }

        public void ChangeState(bool state)
        {
            DefSchemaId = state;
            DataSourceServicePath = state;
            DefName = state;
            FieldValue = state;
            FieldName = state;
            FieldTypeId = state;
            IsLabelVisible = state;
            IsPk = state;
            Length = state;
            MandatoryStatusId = state;
            MessageText = state;
            OrderNumber = state;
            IsParent = state;
            IsMultiLanguage = state;
            SubFieldSchemaId = state;
            IsNameField = state;
        }

        public bool IsDirty()
        {
            return DefSchemaId || DataSourceServicePath || DefName || FieldName || FieldTypeId || IsLabelVisible || IsPk || Length || MandatoryStatusId ||
                   FieldValue || MessageText || OrderNumber || IsParent || IsMultiLanguage || SubFieldSchemaId || IsNameField;
        }

        #endregion State
    }
}