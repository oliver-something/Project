using Models;
using Repository.Contracts;

namespace Repository;

public class CarTaxAmountRepository(Contexts contexts) : ITaxAmountRepository<CarTaxAmount>
{
    public CarTaxAmount GetTaxByBikeId(int id)
    {
        var car = contexts.Cars?.Find(id);
        var tax = contexts.CarsTaxAmounts?.Where(x => x.CarId == car.Id).FirstOrDefault();
        if (tax is null) throw new NullReferenceException("The Bike you are looking for is in the Database");
        return tax;
    }

    public void InsertRecordTax(CarTaxAmount taxAmount)
    {
        contexts.CarsTaxAmounts?.Add(new CarTaxAmount()
        {
            Id = taxAmount.Id,
            CarId = taxAmount.CarId,
            Tax = taxAmount.Tax,
            Dates = taxAmount.Dates
        });
    }

    public void Save()
    {
        contexts.SaveChanges();
    }
}