using Scan2Go.Mapper.Models.DefinitionModels;
using System;

namespace Scan2Go.Mapper.Models.RentsModels;

public class RentsModel
{
	public int CarId { get; set; }
	public int CustomerId { get; set; }
	public bool HasInsurance { get; set; }
	public DateTime RentEndDate { get; set; }
	public int RentId { get; set; }
	public DateTime RentStartDate { get; set; }
	public double TotalCharge { get; set; }
}
