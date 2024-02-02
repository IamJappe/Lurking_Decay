using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ViewLobbyMenu : MonoBehaviour {
    [SerializeField] private Transform mainMenu;
    [SerializeField] private Transform container;

    private void OnEnable() {
        Debug.Log("Running lobbymenu");
        container.LeanMoveLocalX(-Screen.width, 0.5f);
    }

    public void ShowMenu() {
        Debug.Log("Opening lobbymenu");
        mainMenu.LeanMoveLocalX(-Screen.width, 0.5f);
        container.LeanMoveLocalX(Screen.width / 2, 0.5f);
    }

    public void HideMenu() {
        container.LeanMoveLocalX(-Screen.width, 0.5f).setEaseInExpo();
        mainMenu.LeanMoveLocalX(0, 0.5f).setEaseInExpo();
    }
}
