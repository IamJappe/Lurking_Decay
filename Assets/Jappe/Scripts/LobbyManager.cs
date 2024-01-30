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
        CreateLobbyOptions options = new CreateLobbyOptions();
        Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, options);
    }

    [Command]
    public async void QuickJoinLobby()
    {
        Debug.Log("Quick joining lobby...");
        try
        {
            QuickJoinLobbyOptions options = new QuickJoinLobbyOptions();
            var lobby = await LobbyService.Instance.QuickJoinLobbyAsync(options);
            Debug.Log("succesfully joined lobby " + lobby.Id);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
}
