using ClientManager.src.Infrastructure;
using ClientManager.src.Interfaces;
using ClientManager.src.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ClientManager.src.Repositories;

public class ClienteRepository(AplicationDbContext context) : IClienteRepository
{
    private readonly AplicationDbContext _context = context;

    public async Task CreateAsync(Cliente cliente, CancellationToken ct)
    {
        await _context.Clientes.AddAsync(cliente, ct);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Cliente cliente, CancellationToken ct)
    {
        _context.Clientes.Update(cliente);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Cliente cliente, CancellationToken ct)
    {
        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<Cliente?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _context.Clientes.FindAsync(id);
    }
    public async Task<Cliente?> GetAsync(Expression<Func<Cliente, bool>> expression,CancellationToken ct)
    {
        return await _context.Clientes.Where(expression).FirstOrDefaultAsync(ct);
    }

    public async Task<List<Cliente>?> GetAllAsync(
        Expression<Func<Cliente, bool>> expression,
        int offset,
        int limit,
        CancellationToken ct)
    {
        IQueryable<Cliente> query = _context.Clientes;
        
        if (expression != null)
            query = query.Where(expression);
        
        query = query.Skip(offset).Take(limit);

        return await query.OrderBy(c => c.DataCadastro).ToListAsync(ct);
    }

    public async Task<int> CountAsync(Expression<Func<Cliente, bool>> expression, CancellationToken ct)
    {
        return await _context.Clientes.CountAsync(expression, ct);
    }
    public async Task<decimal> GetRendaMediaAsync(Expression<Func<Cliente, bool>> filter, CancellationToken ct)
    {
        return await _context.Set<Cliente>()
            .Where(filter)
            .AverageAsync(c => c.RendaFamiliar, ct);
    }
    
}
