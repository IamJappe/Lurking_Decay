using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFSW.QC;
using Unity.Services.Lobbies.Models;
using Unity.Services.Lobbies;

public class LobbyManager : MonoBehaviour
{

    private Lobby hostLobby;
    public float heartbeatTimerDelay = 20;
    private float heartbeatTimer;

    // lobby heartbeat 
    private async void Update()
    {
        if (hostLobby != null)
        {
            heartbeatTimer -= Time.deltaTime;
            if (heartbeatTimer <= 0)
            {
                heartbeatTimer = heartbeatTimerDelay;
                await LobbyService.Instance.SendHeartbeatPingAsync(hostLobby.Id);
            }
        }
    }

    // creating lobby
    [Command]
    private async void CreateLobby(string lobbyName, int maxPlayers, bool isPrivate)
    {
        Debug.Log("Creating lobby...");
        try
        {
            CreateLobbyOptions options = new CreateLobbyOptions();
            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, options);

            Debug.Log($"Sucesfully created lobby!");
            Debug.Log($"Lobby name: {lobby.Name}");
            Debug.Log($"Lobby code: {lobby.LobbyCode}.");
            Debug.Log($"Lobby ID: {lobby.Id}.");
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    // joining lobbies in different ways
    [Command]
    private async void QuickJoinLobby()
    {
        Debug.Log("Quick joining lobby...");
        try
        {
            QuickJoinLobbyOptions options = new QuickJoinLobbyOptions();
            var lobby = await LobbyService.Instance.QuickJoinLobbyAsync(options);
            Debug.Log("Succesfully joined lobby " + lobby.Name);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    [Command]
    private async void JoinLobbyByCode(string code)
    {
        Debug.Log("Joining lobby with code " + code + "...");
        try
        {
            var lobby = await LobbyService.Instance.JoinLobbyByCodeAsync(code);
            Debug.Log($"sucessfully joined lobby '{lobby.Name}' with ID: {lobby.Id}");
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError(e);
        }
    }
    [Command]
    private async void JoinLobbyById(string id)
    {
        Debug.Log($"Joining lobby with Id: {id}...");
        try
        {
            var lobby = await LobbyService.Instance.JoinLobbyByIdAsync(id);
            Debug.Log($"sucessfully joined lobby '{lobby.Name}' with ID: {lobby.Id}");
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError(e);
        }
    }

    // listing lobbies with filters
    [Command]
    private async void ListLobbies()
    {
        try
        {
            QueryLobbiesOptions options = new QueryLobbiesOptions();
            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync();

            Debug.Log($"Found {queryResponse.Results.Count} lobbies.");
            foreach (Lobby lobby in queryResponse.Results)
            {
                Debug.Log($"{lobby.Name} ({lobby.Players.Count}/{lobby.MaxPlayers})");
            }
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError(e);
        }
    }
}
