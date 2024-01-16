using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;

public class LobbyManager : MonoBehaviour
{
    public Text codeText;
    public InputField codeField;
    string code;

    // create lobby
    async public void CreateLob()
    {
        Debug.Log("Creating lobby...");
        string lobbyName = "new lobby";
        int maxPlayers = 4;
        CreateLobbyOptions options = new CreateLobbyOptions();
        options.IsPrivate = false;

        Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, options);
        Debug.Log(lobby);
        Debug.Log("Created lobby!");
        codeText.text = lobby.LobbyCode;
        (await LobbyService.Instance.GetJoinedLobbiesAsync()).ForEach(lob => Debug.Log(lob));
    }
    // quick join
    async public void QuickJoin()
    {
        try
        {
            await LobbyService.Instance.QuickJoinLobbyAsync();
            Debug.Log("Succesfully quick joined a lobby!");
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }
    // join by code
    async public void JoinLobbyByCode()
    {
        code = codeField.text;
        try
        {
            await LobbyService.Instance.JoinLobbyByCodeAsync(code);
            Debug.Log("Succesfully joined lobby by code!");

        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }
    // quit game
    public void QuitGame()
    {
        Application.Quit();
    }
}
