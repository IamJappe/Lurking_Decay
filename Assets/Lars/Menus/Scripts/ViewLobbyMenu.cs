using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ViewLobbyMenu : MonoBehaviour {
    [SerializeField] private Transform mainMenu;

    private void OnEnable() {
        Debug.Log("Running lobbymenu");
        transform.LeanMoveLocalX(-Screen.width, 0.5f);
        transform.gameObject.SetActive(false);
        //mainMenu.gameObject.SetActive(true);
    }

    public void ShowMenu() {
        Debug.Log("Opening lobbymenu");
        transform.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
        //mainMenu.gameObject.LeanMoveLocalX(-Screen.width, 0.5f).setEaseOutExpo().delay = 0.5f;
        //transform.LeanMoveLocalX(Screen.width / 2, 0.5f).setEaseInExpo().delay = 0.5f;
    }

    public void HideMenu() {
        //transform.LeanMoveLocalX(-Screen.width, 0.5f).setEaseInExpo();
        //mainMenu.LeanMoveLocalX(0, 0.5f).setEaseInExpo();
        mainMenu.gameObject.SetActive(true); // Debug cuz anim not working :( :( :(
        transform.gameObject.SetActive(false); // Debug cuz anim not working :( :( :(
    }
}
