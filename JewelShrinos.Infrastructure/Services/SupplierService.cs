using JewelShrinos.Application.DTOs.Request.Supplier;
using JewelShrinos.Application.DTOs.Response.Supplier;
using JewelShrinos.Application.Interfaces;
using JewelShrinos.Core.Entities;
using JewelShrinos.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JewelShrinos.Infrastructure.Services;

public class SupplierService : ISupplierService
{
    private readonly IRepository<Supplier> _supplierRepository;

    public SupplierService(IRepository<Supplier> supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public async Task<IEnumerable<SupplierResponse>> GetAllAsync()
    {
        var suppliers = await _supplierRepository.AsQueryable()
            .OrderByDescending(x => x.SupplierId)
            .ToListAsync();

        return suppliers.Select(MapToResponse);
    }

    public async Task<SupplierResponse?> GetByIdAsync(int id)
    {
        var supplier = await _supplierRepository.FirstOrDefaultAsync(x => x.SupplierId == id);
        return supplier is null ? null : MapToResponse(supplier);
    }

    public async Task<SupplierResponse> CreateAsync(CreateSupplierRequest request)
    {
        await ValidateCreateRequestAsync(request);

        var normalizedName = request.Name.Trim();
        var normalizedRucDni = NormalizeOptional(request.RucDni);
        var normalizedEmail = NormalizeOptional(request.Email)?.ToLowerInvariant();

        var nameExists = await _supplierRepository.AnyAsync(x => x.Name.ToLower() == normalizedName.ToLower());
        if (nameExists)
            throw new InvalidOperationException("Ya existe un proveedor con ese nombre.");

        if (!string.IsNullOrWhiteSpace(normalizedRucDni))
        {
            var rucExists = await _supplierRepository.AnyAsync(x => x.RucDni == normalizedRucDni);
            if (rucExists)
                throw new InvalidOperationException("Ya existe un proveedor con ese RUC/DNI.");
        }

        if (!string.IsNullOrWhiteSpace(normalizedEmail))
        {
            var emailExists = await _supplierRepository.AnyAsync(x => x.Email != null && x.Email.ToLower() == normalizedEmail);
            if (emailExists)
                throw new InvalidOperationException("Ya existe un proveedor con ese email.");
        }

        var supplier = new Supplier
        {
            Name = normalizedName,
            RucDni = normalizedRucDni,
            ContactName = NormalizeOptional(request.ContactName),
            Email = normalizedEmail,
            Phone = NormalizeOptional(request.Phone),
            Address = NormalizeOptional(request.Address),
            Status = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _supplierRepository.AddAsync(supplier);
        await _supplierRepository.SaveChangesAsync();

        return MapToResponse(supplier);
    }

    public async Task<SupplierResponse> UpdateAsync(int id, UpdateSupplierRequest request)
    {
        var supplier = await _supplierRepository.FirstOrDefaultAsync(x => x.SupplierId == id)
            ?? throw new InvalidOperationException("Proveedor no encontrado.");

        if (request.Name is not null)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new InvalidOperationException("El nombre no puede estar vacío.");

            var normalizedName = request.Name.Trim();

            var duplicatedName = await _supplierRepository.AnyAsync(x =>
                x.SupplierId != id &&
                x.Name.ToLower() == normalizedName.ToLower());

            if (duplicatedName)
                throw new InvalidOperationException("Ya existe otro proveedor con ese nombre.");

            supplier.Name = normalizedName;
        }

        if (request.RucDni is not null)
        {
            var normalizedRucDni = NormalizeOptional(request.RucDni);

            if (!string.IsNullOrWhiteSpace(normalizedRucDni))
            {
                var duplicatedRuc = await _supplierRepository.AnyAsync(x =>
                    x.SupplierId != id &&
                    x.RucDni == normalizedRucDni);

                if (duplicatedRuc)
                    throw new InvalidOperationException("Ya existe otro proveedor con ese RUC/DNI.");
            }

            supplier.RucDni = normalizedRucDni;
        }

        if (request.Email is not null)
        {
            var normalizedEmail = NormalizeOptional(request.Email)?.ToLowerInvariant();

            if (!string.IsNullOrWhiteSpace(normalizedEmail))
            {
                var duplicatedEmail = await _supplierRepository.AnyAsync(x =>
                    x.SupplierId != id &&
                    x.Email != null &&
                    x.Email.ToLower() == normalizedEmail);

                if (duplicatedEmail)
                    throw new InvalidOperationException("Ya existe otro proveedor con ese email.");
            }

            supplier.Email = normalizedEmail;
        }

        if (request.ContactName is not null)
            supplier.ContactName = NormalizeOptional(request.ContactName);

        if (request.Phone is not null)
            supplier.Phone = NormalizeOptional(request.Phone);

        if (request.Address is not null)
            supplier.Address = NormalizeOptional(request.Address);

        if (request.Status.HasValue)
            supplier.Status = request.Status.Value;

        supplier.UpdatedAt = DateTime.UtcNow;

        await _supplierRepository.SaveChangesAsync();

        return MapToResponse(supplier);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var supplier = await _supplierRepository.FirstOrDefaultAsync(x => x.SupplierId == id);
        if (supplier is null) return false;

        supplier.Status = false;
        supplier.UpdatedAt = DateTime.UtcNow;

        await _supplierRepository.SaveChangesAsync();
        return true;
    }

    private async Task ValidateCreateRequestAsync(CreateSupplierRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new InvalidOperationException("El nombre es obligatorio.");
    }

    private static SupplierResponse MapToResponse(Supplier supplier)
    {
        return new SupplierResponse
        {
            SupplierId = supplier.SupplierId,
            Name = supplier.Name,
            RucDni = supplier.RucDni,
            ContactName = supplier.ContactName,
            Email = supplier.Email,
            Phone = supplier.Phone,
            Address = supplier.Address,
            Status = supplier.Status,
            CreatedAt = supplier.CreatedAt
        };
    }

    private static string? NormalizeOptional(string? value)
        => string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}