using Dapper;
using DirectoryService.Core.Locations;
using DirectoryService.Domain.Locations.Aggregate;
using DirectoryService.Domain.Locations.Ids;
using DirectoryService.Domain.Locations.ValueObjects;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DirectoryService.Infrastructure.Postgres.Repositories;

public class DapperLocationRepository : ILocationRepository
{
    private readonly string _connectionString;

    public DapperLocationRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
                            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is missing.");
    }

    public async Task<LocationId> AddAsync(Location location, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            INSERT INTO locations (id, name, country, region, city, street, house_number, apartment_number, created_at, updated_at)
            VALUES (@Id, @Name, @Country, @Region, @City, @Street, @HouseNumber, @ApartmentNumber, @CreatedAt, @UpdatedAt)";

        try
        {
            await using var connection = new NpgsqlConnection(_connectionString);

            await connection.ExecuteAsync(sql, new
            {
                Id = location.Id!.Value,
                Name = location.LocationName.Value,
                Country = location.Address.Country,
                Region = location.Address.Region,
                City = location.Address.City,
                Street = location.Address.Street,
                HouseNumber = location.Address.HouseNumber,
                CreatedAt = location.CreatedAt,
                UpdatedAt = location.UpdatedAt
            });
        }
        catch (Exception)
        {
            throw new InvalidOperationException("Location do not save");
        }

        return location.Id!.Value;
    }

    public async Task<bool> ExistsByNameAsync(LocationName locationName, CancellationToken cancellationToken = default)
    {
        const string sql = "SELECT EXISTS(SELECT 1 FROM locations WHERE locationName = @Name)";

        await using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QuerySingleAsync<bool>(sql, new { LocationName = locationName.Value });
    }
}