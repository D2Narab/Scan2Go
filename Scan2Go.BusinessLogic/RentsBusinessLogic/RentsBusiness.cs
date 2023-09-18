using BusinessLogic.RentsBusinessLogic;
using Scan2Go.BusinessLogic.BaseClasses;
using Scan2Go.Entity.Rents;
using Facade.RentsFacade;
using Utility.Bases;
using Utility.Bases.EntityBases;
using Utility.Core;

namespace Scan2Go.BusinessLogic.RentsBusinessLogic;
public class RentsBusiness : BaseBusiness 
{
	private RentsFacade _rentsFacade;
	private RentsLogic _rentsLogic;
	private RentsValidation _rentsValidation;

	public RentsBusiness(OperationResult operationResult, IUser currentUser) : base(operationResult, currentUser)
	{
	}

	public RentsBusiness(BaseBusiness baseBusiness) : base(baseBusiness)
	{
	}

	private RentsFacade RentsFacade => _rentsFacade ??= new RentsFacade(Language);
	private RentsLogic RentsLogic => _rentsLogic ??= new RentsLogic(this);
	private RentsValidation RentsValidation => _rentsValidation ??= new RentsValidation(this);

	public void DeleteRents(int rentsId)
	{
		Rents rents = RentsFacade.GetRents(rentsId);

		RentsValidation.DeleteRents(rents);

		if (this.OperationState)
		{
			this.AddDetailResult(RentsFacade.DeleteRents(rents));
		}
	}

	public Rents GetRents(int id)
	{
		return RentsFacade.GetRents(id);
	}

	public ListSourceBase GetRentsForList(RentsSearchCriteria rentsSearchCriteria)
	{
		ListSourceBase listSourceBase = RentsFacade.GetRentsForList(rentsSearchCriteria);
		return listSourceBase;
	}

	public void SaveRents(Rents rents)
	{
		RentsValidation.SaveRents(rents);

		if (this.OperationState)
		{
			this.AddDetailResult(RentsFacade.SaveRents(rents));
		}
	}
}
