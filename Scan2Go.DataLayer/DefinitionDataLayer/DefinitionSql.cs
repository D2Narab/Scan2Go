using DataLayer.Base.SqlGenerator;
using Scan2Go.Entity.Definitions;
using Scan2Go.Enums;

namespace Scan2Go.DataLayer.DefinitionDataLayer;

public class DefinitionSql
{
    internal static SqlSelectFactory GetMainPropertiesOfDefinition()
    {
        SqlSelectFactory sqlSelectFactory = new SqlSelectFactory();

        sqlSelectFactory.SelectQuery.Append($" SELECT {TableName.Def_Definitions}.{Definition.Field.IsDefinable}, ");
        sqlSelectFactory.SelectQuery.Append($" {TableName.Def_Definitions}.{Definition.Field.IsLanguageIndependent}, ");
        sqlSelectFactory.SelectQuery.Append($" {TableName.Def_Definitions}.{Definition.Field.DefaultValueId} ");
        sqlSelectFactory.FromQuery.Append($" FROM {TableName.Def_Definitions} ");
        sqlSelectFactory.WhereQuery.Append($" WHERE {TableName.Def_Definitions}.{Definition.Field.TableName}= @{Definition.Field.TableName}");

        return sqlSelectFactory;
    }
}