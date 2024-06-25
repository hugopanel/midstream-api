using Domain.Entities;

namespace Api.Models;

public record UpdateTasksRequest(List<Tache> tasks);