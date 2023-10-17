using Scan2Go.BusinessLogic.RentsBusinessLogic;
using Scan2Go.Entity.Rents;
using Scan2Go.Enums;
using Scan2Go.Mapper.BaseClasses;
using Scan2Go.Mapper.Models.RentsModels;
using Utility.Bases;
using Utility.Bases.EntityBases;
using Utility.Core;
using Utility.Extensions;

namespace Scan2Go.Mapper.RentsMappings;

public class RentsManager : BaseManager
{
    public RentsManager(IUser user) : base(user)
    {
    }

    public OperationResult CreateRent(RentsModel rentsModel)
    {
        Rents rent = Mapper.Map<RentsModel, Rents>(rentsModel);

        OperationResult operationResult = new OperationResult(Modules.Rents.AsInt(), Operations.CreateRent.AsInt());

        new RentsBusiness(operationResult, this.user).SaveRents(rent);
        rentsModel.RentId = rent.CarId;

        return operationResult;
    }

    public OperationResult DeleteRents(int rentsId)
    {
        OperationResult operationResult = new OperationResult((int)Modules.Rents, (int)Operations.DeleteRents);

        new RentsBusiness(operationResult, this.user).DeleteRents(rentsId);

        return operationResult;
    }

    public OperationResult GetRents(int rentsId)
    {
        OperationResult operationResult = new OperationResult();

        Rents rents = new RentsBusiness(operationResult, this.user).GetRents(rentsId);

        RentsModel rentsModel = Mapper.Map<Rents, RentsModel>(rents);

        operationResult.ResultObject = rentsModel;
        return operationResult;
    }

    public OperationResult GetRentsForList(RentsSearchCriteriaModel rentsSearchCriteriaModel)
    {
        OperationResult operationResult = new OperationResult();

        RentsSearchCriteria rentsSearchCriteria = Mapper.Map<RentsSearchCriteriaModel, RentsSearchCriteria>(rentsSearchCriteriaModel);

        ListSourceBase rentsListItems = new RentsBusiness(operationResult, this.user).GetRentsForList(rentsSearchCriteria);

        operationResult.ResultObject = Mapper.Map<ListSourceBase, ListSourceModel<RentsListItemModel>>(rentsListItems);
        return operationResult;
    }

    public OperationResult SaveRents(RentsModel rentsModel)
    {
        var rents = Mapper.Map<RentsModel, Rents>(rentsModel);

        OperationResult operationResult = new OperationResult((int)Modules.Rents, (int)Operations.SaveRents);

        new RentsBusiness(operationResult, this.user).SaveRents(rents);
        operationResult.ResultObject = rents;
        return operationResult;
    }
}