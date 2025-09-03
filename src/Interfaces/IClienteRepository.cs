using ClientManager.src.Models;
using System.Linq.Expressions;
namespace ClientManager.src.Interfaces;

public interface IClienteRepository
{
    Task CreateAsync(Cliente cliente, CancellationToken ct);
    Task UpdateAsync(Cliente cliente, CancellationToken ct);
    Task<Cliente?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<Cliente?> GetAsync(Expression<Func<Cliente, bool>> expression,CancellationToken ct);
    Task<List<Cliente>?> GetAllAsync(Expression<Func<Cliente, bool>> expression, int offset,int limit, CancellationToken ct);
    Task DeleteAsync(Cliente cliente, CancellationToken ct);
    Task<int> CountAsync(Expression<Func<Cliente, bool>> expression, CancellationToken ct);
    Task<decimal> GetRendaMediaAsync(Expression<Func<Cliente, bool>> filter, CancellationToken ct);
}
