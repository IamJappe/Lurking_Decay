using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFSW.QC;
using Unity.Services.Lobbies.Models;
using Unity.Services.Lobbies;

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
            Debug.Log("Sucesfully created lobby " + lobby.Name);
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
        try
        {
            await LobbyService.Instance.JoinLobbyByCodeAsync(code);
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError(e);
        }
    }

    [Command]
    public async void JoinLobbyById(string id)
    {
        try
        {
            await LobbyService.Instance.JoinLobbyByIdAsync(id);
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError(e);
        }
    }
}
