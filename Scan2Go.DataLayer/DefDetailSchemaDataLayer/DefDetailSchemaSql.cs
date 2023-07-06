using DataLayer.Base.SqlGenerator;
using Scan2Go.Entity.Definitions;
using Scan2Go.Enums;
using System.Text;
using Utility.Extensions;

namespace Scan2Go.DataLayer.DefDetailSchemaDataLayer;

public class DefDetailSchemaSql
{
    public static string AddConstraintForeignKey(DefDetailSchema defDetailSchema, string pkColumnName)
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.AppendLine($"ALTER TABLE [{defDetailSchema.DefName}] ");
        stringBuilder.AppendLine($"ADD CONSTRAINT [FK_{defDetailSchema.DefName}_{defDetailSchema.DataSourceServicePath}_{defDetailSchema.FieldName}] ");
        stringBuilder.AppendLine($"FOREIGN KEY ([{defDetailSchema.FieldName}])  REFERENCES {defDetailSchema.DataSourceServicePath} ");
        stringBuilder.AppendLine($"([{pkColumnName}])");

        return stringBuilder.ToString();
    }

    public static string CreateNewColumnOnDefinitionTable(DefDetailSchema defDetailSchema)
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.AppendLine($"ALTER TABLE [{defDetailSchema.DefName}] ");
        stringBuilder.AppendLine($"ADD [{defDetailSchema.FieldName}] ");
        stringBuilder.AppendLine($"[{defDetailSchema.EnmInterfaceComponentType.DbRowDataTypeEqualivant()}] ");

        if (defDetailSchema.EnmInterfaceComponentType == Utility.Enum.GeneralEnums.InterfaceComponentType.Textbox
            || defDetailSchema.EnmInterfaceComponentType == Utility.Enum.GeneralEnums.InterfaceComponentType.Pattern)
        {
            stringBuilder.AppendLine($"({defDetailSchema.Length})");
        }
        else if (defDetailSchema.EnmInterfaceComponentType == Utility.Enum.GeneralEnums.InterfaceComponentType.NumericWithDecimals)
        {
            stringBuilder.AppendLine("(18, 2)");
        }

        return stringBuilder.ToString();
    }

    public static string CreateNewDefinitionTable(string definitionName)
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.AppendLine($" CREATE TABLE [dbo].[Def_{definitionName}] (");
        stringBuilder.AppendLine($" [{definitionName}Id] [int] NOT NULL IDENTITY(1, 1), ");
        stringBuilder.AppendLine($" [{definitionName}Name] [nvarchar] (100) NOT NULL, ");
        stringBuilder.AppendLine(" [IsActive] [bit] NOT NULL) ");
        stringBuilder.AppendLine($" ALTER TABLE [dbo].[Def_{definitionName}] ADD CONSTRAINT [PK_Def_{definitionName}] PRIMARY KEY CLUSTERED  ([{definitionName}Id]) ");
        stringBuilder.AppendLine($" CREATE TABLE [dbo].[Def_{definitionName}_Language] (");
        stringBuilder.AppendLine($" [Language{definitionName}Id] [int] NOT NULL IDENTITY(1, 1),");
        stringBuilder.AppendLine($" [{definitionName}Id] [int] NOT NULL,");
        stringBuilder.AppendLine($" [SystemLanguageId] [int] NOT NULL,");
        stringBuilder.AppendLine($" [{definitionName}Name] [nvarchar] (500) NOT NULL)");
        stringBuilder.AppendLine($" ALTER TABLE [dbo].[Def_{definitionName}_Language] ADD CONSTRAINT [PK_Def_{definitionName}_Language] ");
        stringBuilder.AppendLine($" PRIMARY KEY CLUSTERED  ([{definitionName}Id], [SystemLanguageId])");
        stringBuilder.AppendLine($" ALTER TABLE [dbo].[Def_{definitionName}_Language] ADD CONSTRAINT [FK_Def_{definitionName}_Language_Def_{definitionName}] ");
        stringBuilder.AppendLine($" FOREIGN KEY ([{definitionName}Id]) REFERENCES [dbo].[Def_{definitionName}] ([{definitionName}Id])");
        stringBuilder.AppendLine($" ALTER TABLE [dbo].[Def_{definitionName}_Language] ADD CONSTRAINT [FK_Def_{definitionName}_Language_Enm_SystemLanguage] ");
        stringBuilder.AppendLine($" FOREIGN KEY ([SystemLanguageId]) REFERENCES [dbo].[Enm_SystemLanguage] ([SystemLanguageId])");

        return stringBuilder.ToString();
    }

    public static string DeleteFkOfRowOnDefinitionTable(DefDetailSchema defDetailSchema)
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.AppendLine($"ALTER TABLE [{defDetailSchema.DefName}]");
        stringBuilder.Append($" DROP CONSTRAINT [FK_{defDetailSchema.DefName}_{defDetailSchema.DataSourceServicePath}_{defDetailSchema.FieldName}]");

        return stringBuilder.ToString();
    }

    public static string DeleteRowOnDefinitionTable(DefDetailSchema defDetailSchema)
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.AppendLine($"ALTER TABLE [{defDetailSchema.DefName}]");
        stringBuilder.Append($" DROP COLUMN[{defDetailSchema.FieldName}]");

        return stringBuilder.ToString();
    }

    public static string GetPkColumnNameOfTable(string dataSourceServicePath)
    {
        return $" SELECT dbo.GetPrimaryKeyColumnNameFromTableName('{dataSourceServicePath}') ";
    }

    public SqlSelectFactory CheckIfDefinitionFieldNameIsAlreadyDefinedInTheSystem()
    {
        SqlSelectFactory sqlSelectFactory = new SqlSelectFactory();

        sqlSelectFactory.SelectQuery.Append($"SELECT {TableName.Def_Detail_Schema.InternalValue}.{DefDetailSchema.Field.DefSchemaId}");
        sqlSelectFactory.FromQuery.Append($" FROM {TableName.Def_Detail_Schema.InternalValue}");
        sqlSelectFactory.WhereQuery.Append($" WHERE {TableName.Def_Detail_Schema.InternalValue}.{DefDetailSchema.Field.FieldName} = @{DefDetailSchema.Field.FieldName}");
        sqlSelectFactory.WhereQuery.Append($" AND {TableName.Def_Detail_Schema.InternalValue}.{DefDetailSchema.Field.DefName} = @{DefDetailSchema.Field.DefName}");

        return sqlSelectFactory;
    }

    public SqlSelectFactory CheckIfDefinitionNameIsAlreadyDefinedInTheSystem()
    {
        SqlSelectFactory sqlSelectFactory = new SqlSelectFactory();

        sqlSelectFactory.SelectQuery.Append($"SELECT {TableName.Def_Definitions.InternalValue}.{Definition.Field.DefinitionId}");
        sqlSelectFactory.FromQuery.Append($" FROM {TableName.Def_Definitions.InternalValue}");
        sqlSelectFactory.WhereQuery.Append($" WHERE {TableName.Def_Definitions.InternalValue}.{Definition.Field.NameField} = @{Definition.Field.NameField}");

        return sqlSelectFactory;
    }

    public SqlSelectFactory GetNextOrderNumberOfDefinitionSchema()
    {
        SqlSelectFactory sqlSelectFactory = new SqlSelectFactory();

        sqlSelectFactory.SelectQuery.Append($"SELECT MAX({TableName.Def_Detail_Schema.InternalValue}.{DefDetailSchema.Field.OrderNumber}) ");
        sqlSelectFactory.FromQuery.Append($"FROM {TableName.Def_Detail_Schema.InternalValue}");
        sqlSelectFactory.WhereQuery.Append($"WHERE {TableName.Def_Detail_Schema.InternalValue}.{DefDetailSchema.Field.DefName}");
        sqlSelectFactory.WhereQuery.Append($" = @{DefDetailSchema.Field.DefName} ");

        return sqlSelectFactory;
    }

    internal static SqlSelectFactory GetDefinitionLanguage()
    {
        SqlSelectFactory sqlSelectFactory = new SqlSelectFactory();

        sqlSelectFactory.SelectQuery.Append($"SELECT {TableName.Def_Definitions_Language.InternalValue}.* ");
        sqlSelectFactory.FromQuery.AppendLine($"FROM {TableName.Def_Definitions_Language.InternalValue} ");
        sqlSelectFactory.WhereQuery.AppendLine($"WHERE {TableName.Def_Definitions_Language.InternalValue}.{Definition.Field.DefinitionId}  = @{Definition.Field.DefinitionId} ");

        return sqlSelectFactory;
    }

    internal static SqlSelectFactory GetDefinitionWithoutLanguage()
    {
        SqlSelectFactory sqlSelectFactory = new SqlSelectFactory();

        sqlSelectFactory.SelectQuery.Append($"SELECT {TableName.Def_Definitions.InternalValue}.* ");
        sqlSelectFactory.FromQuery.AppendLine($"FROM {TableName.Def_Definitions.InternalValue} ");
        sqlSelectFactory.WhereQuery.AppendLine($"WHERE {TableName.Def_Definitions.InternalValue}.{Definition.Field.DefinitionId}  = @{Definition.Field.DefinitionId} ");

        return sqlSelectFactory;
    }
}