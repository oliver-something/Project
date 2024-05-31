using Models;

namespace Repository.Contracts;

public interface ITaxAmountRepository<T> where T : BaseTaxAmount
{
    T GetTaxByBikeId(int id);
    void InsertRecordTax(T taxAmount);
    void Save();
}