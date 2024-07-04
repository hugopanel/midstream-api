using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Common.Commands;

public record AddPermissionToRoleCommand(Role Role, Permission Permission): IRequest<Unit>;