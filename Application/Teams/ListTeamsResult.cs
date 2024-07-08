using Domain.Entities;

namespace Application.Teams;

public record ListTeamsResult(
    List<Team> Teams
);