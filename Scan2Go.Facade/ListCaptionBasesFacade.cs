using Scan2Go.DataLayer.BaseClasses.SelectOperationBases;
using Scan2Go.DataLayer.ListCaptionBaseDataLater;
using Scan2Go.Enums;
using System.Data;
using Utility.Bases;
using Utility.Bases.EntityBases;
using Utility.Bases.EntityBases.Facade;
using Utility.Bases.ListBases;
using Utility.Core;
using Utility.Core.DataLayer;
using Utility.Enum;
using Utility.Enum.GeneralEnums;
using Utility.Extensions;

namespace Scan2Go.Facade;

public class ListCaptionBasesFacade : FacadeBase
{
    public ListCaptionBasesFacade(LanguageEnum languageEnum) : base(languageEnum)
    {
    }

    public List<ListCaptionBase> GetCaptions(ListSourceBase listSourceBase, string prmlistCaptionTypeName = null)
    {
        List<ListCaptionBase> entityCollectionBase = new List<ListCaptionBase>();
        string listCaptionTypeName = listSourceBase.ListItemType.Name;

        if (string.IsNullOrEmpty(prmlistCaptionTypeName) == false)
        {
            listCaptionTypeName = prmlistCaptionTypeName;
        }

        DataTable dataTable = new Scan2GoSelectOperations().GetEntityByAnotherColumnDataTable<ListCaptionBase>(ListCaptionBase.Field.Type, listCaptionTypeName, DbType.String);

        foreach (DataRow dataRow in dataTable.Rows)
        {
            entityCollectionBase.Add(FillListCaption(dataRow));
        }

        return entityCollectionBase.OrderBy(t => t.Order).ToList();
    }

    public ListSourceBase GetCaptionsofList(int listItemTypeId)
    {
        List<DatabaseParameter> whereParameters = new List<DatabaseParameter>();
        List<DatabaseParameter> orderByParameters = new List<DatabaseParameter>();

        whereParameters.Add(new DatabaseParameter { FieldName = ListCaptionBase.Field.ListItemTypeId, DbType = DbType.Int32, FieldValue = listItemTypeId });
        orderByParameters.Add(new DatabaseParameter { FieldName = ListCaptionBase.Field.order, DbType = DbType.Int32 });

        DataTable dataTable = new Scan2GoSelectOperations().GetEntityByAnotherColumnDataTable<ListCaptionBase>(whereParameters, orderByParameters);

        ListSourceBase listSourceBase = new ListSourceBase();

        listSourceBase.ListItemBases = new List<ListItemBase>();
        listSourceBase.TotalRecordCount = dataTable.Rows.Count;
        listSourceBase.ListItemType = typeof(ListCaptionBaseListItem);
        //listSourceBase.RecordInfo = EnumMethods.GetResourceString(caseSearchCriteria.CaseListType.ToString(), this.languageEnum);

        foreach (DataRow dataRow in dataTable.Rows)
        {
            ListCaptionBaseListItem listCaptionBaseListItem = FillListCaptionBaseListItem(dataRow);

            if (listCaptionBaseListItem != null)
            {
                listSourceBase.ListItemBases.Add(listCaptionBaseListItem);
            }
        }

        listSourceBase.ListCaptionBases = new ListCaptionBasesFacade(this.languageEnum).GetCaptions(listSourceBase);

        return listSourceBase;
    }

    public ListCaptionBase GetListCaptionBase(int listCaptionBaseId)
    {
        DataRow dataRow = new Scan2GoSelectOperations().GetEntityDataRow<ListCaptionBase>(listCaptionBaseId);

        ListCaptionBase listCaptionBase = FillListCaption(dataRow);

        return listCaptionBase;
    }

    public OperationResult SaveListCaptionBase(ListCaptionBase listCaptionBase)
    {
        return new ListCaptionBaseDAO().SaveListCaptionBase(listCaptionBase);
    }

    private ListCaptionBase FillListCaption(DataRow dataRow)
    {
        ListCaptionBase listCaptionBase = new ListCaptionBase();

        listCaptionBase.AlignmentIsMiddle = dataRow.AsBool(ListCaptionBase.Field.AlignmentIsMiddle);
        listCaptionBase.ColumnCaption = EnumMethods.GetResourceString(dataRow.AsString(ListCaptionBase.Field.columnCaption), this.languageEnum);
        listCaptionBase.ColumnFieldTypeId = dataRow.AsInt(ListCaptionBase.Field.ColumnFieldTypeId);
        listCaptionBase.CorrespondingField = dataRow.AsString(ListCaptionBase.Field.correspondingField).ToLowerFirstLetter();//intended for the frontend
        listCaptionBase.IsPk = dataRow.AsBool(ListCaptionBase.Field.isPK);
        listCaptionBase.IsVisible = dataRow.AsBool(ListCaptionBase.Field.isVisible);
        listCaptionBase.ListCaptionId = dataRow.AsInt(ListCaptionBase.Field.ListCaptionId);
        listCaptionBase.ListRenderType = (ListRenderTypes)dataRow.AsInt(ListCaptionBase.Field.ListRenderType, true);
        listCaptionBase.Order = dataRow.AsInt(ListCaptionBase.Field.order);
        listCaptionBase.Width = dataRow.AsInt(ListCaptionBase.Field.width);

        return listCaptionBase;
    }

    private ListCaptionBaseListItem FillListCaptionBaseListItem(DataRow dataRow)
    {
        if (dataRow == null)
        {
            return null;
        }

        ListCaptionBaseListItem listCaptionBaseListItem = new ListCaptionBaseListItem();

        listCaptionBaseListItem.ListCaptionId = dataRow.AsInt(ListCaptionBase.Field.ListCaptionId);
        listCaptionBaseListItem.columnCaption = EnumMethods.GetResourceString(dataRow.AsString(ListCaptionBase.Field.columnCaption), this.languageEnum);
        listCaptionBaseListItem.isVisible = dataRow.AsBool(ListCaptionBase.Field.isVisible);
        listCaptionBaseListItem.order = dataRow.AsInt(ListCaptionBase.Field.order);
        listCaptionBaseListItem.width = dataRow.AsInt(ListCaptionBase.Field.width);

        return listCaptionBaseListItem;
    }
}