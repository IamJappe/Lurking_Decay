using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFSW.QC;
using Unity.Services.Lobbies.Models;
using Unity.Services.Lobbies;
using Unity.Netcode;

public class LobbyManager : MonoBehaviour
{
    [Command]
    public async void CreateLobby(string lobbyName, int maxPlayers, bool isPrivate)
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

    [Command]
    public async void QuickJoinLobby()
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
    public async void JoinLobbyByCode(string code)
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
    public async void JoinLobbyById(string id)
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
}
