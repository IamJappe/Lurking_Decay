using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonSelection : MonoBehaviour
{
    [SerializeField] private List<string> options;
    [SerializeField] private TMP_Text textField;
    [SerializeField] private string savedAs;
    private string selected;
    private int currentIndex = 0;
    private int nextIndex = 1;

    private void Start() {
        if (this.options.Count == 0) throw new System.Exception("Invalid options: length is 0.");
        this.selected = options[currentIndex];
    }

    public void SetNextOption() {
        currentIndex = options.FindIndex(str => str == selected);
        nextIndex = this.options.Count - 1 == currentIndex ? 0 : currentIndex + 1;
        this.textField.text = this.options[nextIndex];
        this.currentIndex = nextIndex;
        this.selected = this.options[currentIndex];
        PlayerPrefs.SetString(savedAs, selected);
    }
}
