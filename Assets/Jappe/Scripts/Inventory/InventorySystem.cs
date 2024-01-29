using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using System;

public class InventorySystem : MonoBehaviour {

    public static InventorySystem Instance { get; set; }
    [SerializeField] private GameObject inventoryScreenUI;
    [SerializeField] private bool isOpen = false; // TODO: remove serialize field if building for prod

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }

        // TODO: Check if application stopped, if yes stop listener
        // TODO: Cache player prefs instead of getting every time (probably expensive operation)
        IDisposable disposable = InputSystem.onAnyButtonPress.Call((btn) => {
            string btnName = PlayerPrefs.GetString("OPEN_INVENTORY") == ""
                                ? "i"
                                : PlayerPrefs.GetString("OPEN_INVENTORY");
            if (btn.name != btnName) return;
            Debug.Log("opening inventory");
            inventoryScreenUI.SetActive(!isOpen);
            Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isOpen;
            isOpen = !isOpen;

        });
    }
}