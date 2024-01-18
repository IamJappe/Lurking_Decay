using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitMenu : MonoBehaviour
{
    [SerializeField] private Transform box;
    [SerializeField] private CanvasGroup background;
    [SerializeField] private CanvasGroup mainMenu;

    private void OnEnable() {
        background.alpha = 0;
        box.localPosition = new Vector2(0, -Screen.height);
    }

    public void OpenDialog() { // A function that opens the dialog
        mainMenu.LeanAlpha(0, 0.1f);
        background.LeanAlpha(1.0f, 0.5f);
        box.LeanMoveLocalY(0f, 0.5f).setEaseOutExpo().delay = 0.1f;
    }

    public void CloseDialog() { // A function that closes the dialog
        background.LeanAlpha(0f, 0.5f);
        box.LeanMoveLocalY(-Screen.height, 0.5f).setEaseInExpo();
        mainMenu.LeanAlpha(1, 0.1f).delay = 0.3f;
    }

    public void QuitGame() {
        // Just if testing idk if worked
        Debug.Log("Quiting game...");
        Application.Quit();
    }
}
