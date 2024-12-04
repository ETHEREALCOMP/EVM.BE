using Microsoft.EntityFrameworkCore;

namespace EVM.Data.Models;

public interface IDBConfigurableModel
{
    public abstract static void BuildModel(ModelBuilder builder);
}