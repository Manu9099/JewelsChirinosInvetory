using System.Security.Cryptography;
using System.Text;
using JewelShrinos.Application.DTOs.Request.Customer;
using JewelShrinos.Application.DTOs.Response.Customer;
using JewelShrinos.Application.Interfaces;
using JewelShrinos.Core.Entities;
using JewelShrinos.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JewelShrinos.Infrastructure.Services;

public class CustomerService : ICustomerService
{
    private readonly IRepository<Customer> _customerRepository;

    public CustomerService(IRepository<Customer> customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<CustomerResponse> RegisterAsync(RegisterCustomerRequest request)
    {
        await ValidateRegisterRequestAsync(request);

        var email = request.Email.Trim().ToLowerInvariant();

        var exists = await _customerRepository.AnyAsync(c => c.Email.ToLower() == email);
        if (exists)
            throw new InvalidOperationException("Ya existe un cliente con ese email.");

        var customer = new Customer
        {
            FirstName = request.FirstName.Trim(),
            LastName = NormalizeOptional(request.LastName),
            Email = email,
            PasswordHash = HashPassword(request.Password),
            Phone = NormalizeOptional(request.Phone),
            Address = NormalizeOptional(request.Address),
            RucDni = NormalizeOptional(request.RucDni),
            Status = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _customerRepository.AddAsync(customer);
        await _customerRepository.SaveChangesAsync();

        return MapToResponse(customer);
    }

    public async Task<IEnumerable<CustomerResponse>> GetAllAsync()
    {
        var customers = await _customerRepository.AsQueryable()
            .OrderByDescending(c => c.CustomerId)
            .ToListAsync();

        return customers.Select(MapToResponse);
    }

    public async Task<CustomerResponse?> GetByIdAsync(int id)
    {
        var customer = await _customerRepository.FirstOrDefaultAsync(c => c.CustomerId == id);
        return customer is null ? null : MapToResponse(customer);
    }

    public async Task<CustomerResponse?> GetByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new InvalidOperationException("El email es obligatorio.");

        var normalizedEmail = email.Trim().ToLowerInvariant();

        var customer = await _customerRepository.FirstOrDefaultAsync(
            c => c.Email.ToLower() == normalizedEmail);

        return customer is null ? null : MapToResponse(customer);
    }

    public async Task<CustomerResponse> UpdateAsync(int id, UpdateCustomerRequest request)
    {
        var customer = await _customerRepository.FirstOrDefaultAsync(c => c.CustomerId == id)
                       ?? throw new InvalidOperationException("Cliente no encontrado.");

        if (!string.IsNullOrWhiteSpace(request.FirstName))
            customer.FirstName = request.FirstName.Trim();

        if (request.LastName is not null)
            customer.LastName = NormalizeOptional(request.LastName);

        if (request.Phone is not null)
            customer.Phone = NormalizeOptional(request.Phone);

        if (request.Address is not null)
            customer.Address = NormalizeOptional(request.Address);

        if (request.RucDni is not null)
            customer.RucDni = NormalizeOptional(request.RucDni);

        customer.UpdatedAt = DateTime.UtcNow;

        await _customerRepository.SaveChangesAsync();
        return MapToResponse(customer);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var customer = await _customerRepository.FirstOrDefaultAsync(c => c.CustomerId == id);
        if (customer is null) return false;

        // Soft delete para no romper historial de ventas/devoluciones
        customer.Status = false;
        customer.UpdatedAt = DateTime.UtcNow;

        await _customerRepository.SaveChangesAsync();
        return true;
    }

    private async Task ValidateRegisterRequestAsync(RegisterCustomerRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.FirstName))
            throw new InvalidOperationException("El nombre es obligatorio.");

        if (string.IsNullOrWhiteSpace(request.Email))
            throw new InvalidOperationException("El email es obligatorio.");

        if (string.IsNullOrWhiteSpace(request.Password))
            throw new InvalidOperationException("La contraseña es obligatoria.");
    }

    private static CustomerResponse MapToResponse(Customer customer)
    {
        return new CustomerResponse
        {
            CustomerId = customer.CustomerId,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            Phone = customer.Phone,
            Address = customer.Address,
            RucDni = customer.RucDni,
            Status = customer.Status,
            CreatedAt = customer.CreatedAt
        };
    }

    private static string? NormalizeOptional(string? value)
        => string.IsNullOrWhiteSpace(value) ? null : value.Trim();

    private static string HashPassword(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }
}