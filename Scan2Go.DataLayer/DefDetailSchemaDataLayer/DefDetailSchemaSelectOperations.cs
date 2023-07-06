using DataLayer.Base.DatabaseFactory;
using DataLayer.Base.SqlGenerator;
using Scan2Go.DataLayer.BaseClasses.DataLayersBases;
using Scan2Go.Entity.Definitions;
using System.Data;
using Utility.Core.DataLayer;

namespace Scan2Go.DataLayer.DefDetailSchemaDataLayer;

public class DefDetailSchemaSelectOperations : Scan2GoDataLayerBase
{
    public DefDetailSchemaSelectOperations(DataLayerBase dataLayerBase) : base(dataLayerBase)
    {
    }

    public SqlSelectFactory CheckIfDefinitionFieldNameIsAlreadyDefinedInTheSystem(string fieldName, string defName)
    {
        SqlSelectFactory sqlSelectFactory = new DefDetailSchemaSql().CheckIfDefinitionFieldNameIsAlreadyDefinedInTheSystem();

        sqlSelectFactory.AddParam(new DatabaseParameter
        {
            FieldName = DefDetailSchema.Field.FieldName,
            DbType = DbType.String,
            FieldValue = fieldName
        });

        sqlSelectFactory.AddParam(new DatabaseParameter
        {
            FieldName = DefDetailSchema.Field.DefName,
            DbType = DbType.String,
            FieldValue = defName
        });

        return sqlSelectFactory;
    }

    public SqlSelectFactory CheckIfDefinitionNameIsAlreadyDefinedInTheSystem(string definitionName)
    {
        SqlSelectFactory sqlSelectFactory = new DefDetailSchemaSql().CheckIfDefinitionNameIsAlreadyDefinedInTheSystem();

        sqlSelectFactory.AddParam(new DatabaseParameter
        {
            FieldName = Definition.Field.NameField,
            DbType = DbType.String,
            FieldValue = definitionName
        });

        return sqlSelectFactory;
    }
}