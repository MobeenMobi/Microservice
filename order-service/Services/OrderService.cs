using System;
using Microsoft.EntityFrameworkCore;
using order_service.Data;
using order_service.Models;

namespace order_service.Services
{
    

    public interface IOrderService
    {
        Task<IEnumerable<Orders>> GetAllAsync();
        Task<Orders?> GetByIdAsync(int id);
        Task<Orders> CreateAsync(Orders order);
        Task<Orders> UpdateAsync(int id, Orders order);
        Task<bool> DeleteAsync(int id);
    }


    public class OrderService : IOrderService
    {
        private readonly OrderDbContext _context;


        public OrderService(OrderDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Orders>> GetAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }


        public async Task<Orders?> GetByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }


        public async Task<Orders> CreateAsync(Orders order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }


        public async Task<Orders> UpdateAsync(int id, Orders order)
        {
            var existing = await _context.Orders.FindAsync(id);
            if (existing == null) throw new KeyNotFoundException("Order not found");


            _context.Entry(existing).CurrentValues.SetValues(order);
            await _context.SaveChangesAsync();
            return existing;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return false;


            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
