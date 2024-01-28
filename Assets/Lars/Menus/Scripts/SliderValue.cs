using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SliderValue : MonoBehaviour
{
    [SerializeField] private bool isPerentage;
    [SerializeField] private float minValue;
    [SerializeField] private float maxValue;

    public void UpdateText(float value) {
        if (!isPerentage) transform.GetComponent<TMP_Text>().SetText(((int)(maxValue * value)).ToString());
        else transform.GetComponent<TMP_Text>().SetText(((int) (value * 100)) + "%"); //TODO: Remove casting and use Mathf.RoundToInt
    }
}
