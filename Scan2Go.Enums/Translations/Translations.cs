using System.Data;
using Utility.Bases.EntityBases;
using Utility.Bases.EntityBases.DefinitionBases;
using Utility.Core.DataLayer;
using Utility.Extensions;

namespace Scan2Go.Enums.Translations
{
    [Serializable]
    public class Translations : EntityStateBase
    {
        #region FieldNames

        public class Field : DefinitionFieldEntityBase
        {
            public const string Arabic = "Arabic";
            public const string Azerbaijani = "Azerbaijani";
            public const string English = "English";
            public const string TranslationId = "TranslationId";
            public const string TranslationKey = "TranslationKey";
            public const string Turkish = "Turkish";
            public const string UsageAreaId = "UsageAreaId";
        }

        #endregion FieldNames

        #region Privates of Translations

        private string _arabic;
        private string _azerbaijani;
        private string _english;
        private TranslationUsageArea _enmTranslationUsageArea;
        private TranslationsState _stateTranslations;
        private int _translationId;
        private string _translationKey;
        private string _turkish;
        protected override IState _state
        {
            get { return _stateTranslations; }
        }

        #endregion Privates of Translations

        #region Constructor of Translations

        public Translations()
        {
            _stateTranslations = new TranslationsState();
        }

        #endregion Constructor of Translations

        #region Properties of Translations

        public string Arabic
        { get { return _arabic; } set { if (_arabic != value) { _arabic = value; _stateTranslations.Arabic = true; } } }
        public string Azerbaijani
        { get { return _azerbaijani; } set { if (_azerbaijani != value) { _azerbaijani = value; _stateTranslations.Azerbaijani = true; } } }
        public string English
        { get { return _english; } set { if (_english != value) { _english = value; _stateTranslations.English = true; } } }
        public TranslationUsageArea EnmTranslationUsageArea
        { get { return _enmTranslationUsageArea; } set { if (_enmTranslationUsageArea.Equals(value) == false) { _enmTranslationUsageArea = value; _stateTranslations.UsageAreaId = true; } } }

        public int TranslationId
        { get { return _translationId; } set { _translationId = value; } }
        public string TranslationKey
        { get { return _translationKey; } set { if (_translationKey != value) { _translationKey = value; _stateTranslations.TranslationKey = true; } } }
        public string Turkish
        { get { return _turkish; } set { if (_turkish != value) { _turkish = value; _stateTranslations.Turkish = true; } } }
        #endregion Properties of Translations

        #region AUTO methods

        public string EnmTranslationUsageAreaName { get; set; }

        public override int PrimaryKeyValue
        { get { return this.TranslationId; } set { this.TranslationId = value; } }
        public override List<DatabaseParameter> GetEntityDbParameters(bool isStateCheck = true)
        {
            this.ClearSqlParameters();

            if (isStateCheck && _stateTranslations.Arabic)
            {
                this.AddParam(Field.Arabic, DbType.String, this.Arabic);
            }

            if (isStateCheck && _stateTranslations.Azerbaijani)
            {
                this.AddParam(Field.Azerbaijani, DbType.String, this.Azerbaijani);
            }

            if (isStateCheck && _stateTranslations.English)
            {
                this.AddParam(Field.English, DbType.String, this.English);
            }

            this.AddParam(Field.TranslationKey, DbType.Int32, this.TranslationKey, ParameterDirection.Input, true);

            if (isStateCheck && _stateTranslations.Turkish)
            {
                this.AddParam(Field.Turkish, DbType.String, this.Turkish);
            }

            if (isStateCheck && _stateTranslations.UsageAreaId)
            {
                this.AddParam(Field.UsageAreaId, DbType.Int32, this.EnmTranslationUsageArea.GetAsDbParam());
            }

            if (isStateCheck && _stateTranslations.TranslationId)
            {
                this.AddParam(Field.TranslationId, DbType.Int32, this.TranslationId);
            }

            if (isStateCheck && _stateTranslations.TranslationKey)
            {
                this.AddParam(Field.TranslationKey, DbType.Int32, this.TranslationKey);
            }

            return this.GetSqlParameters();
        }

        public override DataLayerEnumBase GetPrimaryKeyName()
        {
            return PrimaryKey.TranslationId;
        }

        public override DataLayerEnumBase GetTableName()
        {
            return TableName.Translations;
        }

        #endregion AUTO methods

        public ListItemBase ListItemMember
        {
            get
            {
                return new TranslationsListItem
                {
                    TranslationId = this.TranslationId,
                    TranslationKey = this.TranslationKey,
                    Arabic = this.Arabic,
                    Azerbaijani = this.Azerbaijani,
                    English = this.English,
                    Turkish = this.Turkish,
                    EnmTranslationUsageArea = this.EnmTranslationUsageArea
                };
            }
        }
    }

    #region State

    public class TranslationsState : IState
    {
        public bool Arabic { get; set; }
        public bool Azerbaijani { get; set; }
        public bool English { get; set; }
        public bool TranslationId { get; set; }
        public bool TranslationKey { get; set; }
        public bool Turkish { get; set; }
        public bool UsageAreaId { get; set; }

        public void ChangeState(bool state)
        {
            Arabic = state;
            Azerbaijani = state;
            English = state;
            TranslationId = state;
            TranslationKey = state;
            Turkish = state;
            UsageAreaId = state;
        }

        public bool IsDirty()
        {
            return Arabic || Azerbaijani || English || TranslationId || TranslationKey || Turkish || UsageAreaId;
        }

        #endregion State
    }
}