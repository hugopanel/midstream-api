using Domain.Entities;

namespace Domain.Interfaces;

public interface IWhiteboardHub
{
    // private static readonly List<WhiteboardMessage> Drawing;
    public Task JoinWhiteboard(string whiteboardId);
    public Task LeaveWhiteboard(string whiteboardId);
    public Task SendDrawing(string whiteboardId, WhiteboardMessage message);
    public Task RetrieveDrawing(string whiteboardId);
}