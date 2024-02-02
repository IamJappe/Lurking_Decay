using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Authentication;
using QFSW.QC;
using Unity.Services.Core;

public class Authenticator : MonoBehaviour
{
    private async void Start()
    {
        await UnityServices.InitializeAsync();
    }

    [Command]
    public async void AuthenticateAnon()
    {
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in with Id: " + AuthenticationService.Instance.PlayerId);
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
}