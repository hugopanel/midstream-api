using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Application.Whiteboard;

public class WhiteboardHub : Hub, IWhiteboardHub
{
    private static readonly Dictionary<string, List<WhiteboardMessage>> Drawings = new();
    private static readonly Dictionary<string, List<WhiteboardMessage>> UndoneDrawings = new();

    public async Task JoinWhiteboard(string whiteboardId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, whiteboardId); // Join the group
        
        // Check if the whiteboard already has users in it
        if (!Drawings.ContainsKey(whiteboardId))
            Drawings.Add(whiteboardId, new List<WhiteboardMessage>());
        if (!UndoneDrawings.ContainsKey(whiteboardId))
            UndoneDrawings.Add(whiteboardId, new List<WhiteboardMessage>());

        // TODO: Add persistence, store the drawings somewhere
    }

    public async Task LeaveWhiteboard(string whiteboardId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, whiteboardId); // Leave the group
        // Note: We can't know the number of connections in a group, so we never remove them.
    }

    public async Task SendDrawing(string whiteboardId, WhiteboardMessage message)
    {
        // TODO: Add a way to make sure the drawings appear with the same Z-index for all clients
        Drawings[whiteboardId].Add(message);
        UndoneDrawings[whiteboardId].Clear();
        
        await Clients.OthersInGroup(whiteboardId).SendAsync("ReceiveDrawing", message);
        
        Console.WriteLine(String.Join("\t", "SendDrawing", whiteboardId, Drawings[whiteboardId].Count));
    }

    public async Task RetrieveDrawing(string whiteboardId)
    {
        await Clients.Caller.SendAsync("RetrieveDrawing", Drawings[whiteboardId]);
        Console.WriteLine(String.Join("\t", "RetrieveDrawing", whiteboardId, Drawings[whiteboardId].Count));
    }

    public async Task UndoLastDrawing(string whiteboardId)
    {
        if (Drawings[whiteboardId].Count > 0)
        {
            var lastDrawing = Drawings[whiteboardId].Last();
            Drawings[whiteboardId].Remove(lastDrawing);
            UndoneDrawings[whiteboardId].Add(lastDrawing);
            await Clients.Group(whiteboardId).SendAsync("RemoveDrawing", lastDrawing);
        }
    }

    public async Task RedoLastUndoneDrawing(string whiteboardId)
    {
        if (UndoneDrawings[whiteboardId].Count > 0)
        {
            var lastUndoneDrawing = UndoneDrawings[whiteboardId].Last();
            UndoneDrawings[whiteboardId].Remove(lastUndoneDrawing);
            Drawings[whiteboardId].Add(lastUndoneDrawing);
            await Clients.Group(whiteboardId).SendAsync("ReceiveDrawing", lastUndoneDrawing);
        }
    }

    public async Task ClearWhiteboard(string whiteboardId)
    {
        UndoneDrawings[whiteboardId].Clear();
        
        Drawings[whiteboardId].Clear();
        await Clients.Group(whiteboardId).SendAsync("ClearWhiteboard");
    }
}