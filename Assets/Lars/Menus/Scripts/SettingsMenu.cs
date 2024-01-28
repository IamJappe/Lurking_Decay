using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.Controls;
using TMPro;

/// <summary>
/// All player prefs are saved in the windows registry at the following location:
/// Computer\HKEY_CURRENT_USER\SOFTWARE\DefaultCompany\Lurking Decay
/// </summary>
public class SettingsMenu : MonoBehaviour {
    [Header("Global Tab Settings")]
    [SerializeField] private int CurrentTab = 0; // Keybinds by default, 0 = keybinds, 1 = audio, 2 = video (see buttons)
    [SerializeField] private List<ScrollRect> views; // Keybinds stored by index (see inspector)

    [Space]
    [Header("Keybinds")]
    [SerializeField] private List<string> KeybindOrder; // The order of the keybinds (use stored name (e.g.: WALK_FORWARD))
    [SerializeField] private List<TextMeshProUGUI> KeybindLabels;
    [Space]
    [SerializeField] private GameObject PleaseEnterKeyScreen;




    private void Start() {
        views.ForEach(view => view.gameObject.SetActive(false));
        ChangeTab(CurrentTab);
        UpdateKeybinds();
    }

    private void OnEnable() {
        GetComponent<RectTransform>().localPosition = new Vector2(5000, 0);
    }

    public void ChangeTab(int tabId) {
        views[CurrentTab].gameObject.SetActive(false);
        views[tabId].gameObject.SetActive(true);
        this.CurrentTab = tabId;
    }

    public void ChangeKeybind(string keybind) { // see inspector of keybinds view (buttons)
        PleaseEnterKeyScreen.SetActive(true);
        InputSystem.onAnyButtonPress.CallOnce((control) => {
            PlayerPrefs.SetString(keybind.ToString(), control.name);
            UpdateKeybinds();
            PleaseEnterKeyScreen.SetActive(false);
        });
    }

    private void OnApplicationQuit() { // Only works if in prod
        PlayerPrefs.Save();
    }


    private void UpdateKeybinds() {
        if (CurrentTab != 0) return;
        int index = 0;
        KeybindLabels.ForEach(label => {
            Debug.Log(label);
            label.SetText(PlayerPrefs.GetString(KeybindOrder[index]));
            index++;
        });
    }

    public void OpenMenu() {
        transform.LeanMoveLocalX(Screen.width / 2, 0.5f);
    }
}
