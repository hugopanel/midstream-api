using System.Linq.Expressions;
using Domain.Interfaces;
using Domain.Permissions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.ValueConversion;

namespace Infrastructure.Persistence;

public class PermissionsValueConverter : ValueConverter<List<Permission>, List<string>>, INpgsqlArrayConverter
{
    public PermissionsValueConverter() : base(
        permissions => permissions.Select(p => p.Code).ToList(),
        codes => codes.Select(
            code => PermissionMapper.Permissions.FirstOrDefault(
                p => p.Code == code)).ToList()!)
    {
    }

    public ValueConverter ElementConverter => new ValueConverter<Permission, string>(x => x.Code, x => PermissionMapper.Permissions.FirstOrDefault(p => p.Code == x)!);
}