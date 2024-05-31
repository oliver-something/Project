using Models;
using Repository.Contracts;

namespace Repository;

public class MotorbikeTaxAmountRepository(Contexts contexts) : ITaxAmountRepository<MotorbikeTaxAmount>
{
    public MotorbikeTaxAmount GetTaxByBikeId(int id)
    {
        var bike = contexts.Motorbikes?.Find(id);
        if (contexts.MotorbikeTaxAmounts is null)
            throw new Exception("Not found!");
        var tax = contexts.MotorbikeTaxAmounts.FirstOrDefault(x => x.MotorbikeId == bike.Id);
        if (tax is null) throw new NullReferenceException("The Bike you are looking for is in the Database");
        return tax;
    }

    public void InsertRecordTax(MotorbikeTaxAmount taxAmount)
    {
        contexts.MotorbikeTaxAmounts?.Add(new MotorbikeTaxAmount()
        {
            Id = taxAmount.Id,
            MotorbikeId = taxAmount.MotorbikeId,
            Tax = taxAmount.Tax,
            Dates = taxAmount.Dates
        });
    }

    public void Save()
    {
        contexts.SaveChanges();
    }
}