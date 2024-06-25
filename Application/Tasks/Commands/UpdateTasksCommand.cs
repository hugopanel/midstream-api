using MediatR;
using Domain.Entities;

namespace Application.Tasks.Commands;

public record UpdateTasksCommand(List<Tache> tasks) : IRequest<MessageResult>;