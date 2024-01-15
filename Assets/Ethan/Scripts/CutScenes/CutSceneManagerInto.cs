using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManagerInto : MonoBehaviour
{
    public float timeToAwake, timeToSpeak;
    public GameObject staminaBar, healthBar, hands, player, playerOld, miniMap;
    public GameObject text1;
    public GameObject contolltext1, contolltext2, contolltext3;
    public PlayerCamera cam;
    void Start()
    {
        StartCoroutine(WaitTime());
        StartCoroutine(TalkTime());
    }  

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(timeToAwake);
        player.SetActive(true);
        playerOld.SetActive(false);
        miniMap.SetActive(true);
        staminaBar.SetActive(true);
        healthBar.SetActive(true);
    }

    IEnumerator TalkTime()
    {
        yield return new WaitForSeconds(timeToSpeak);
        text1.SetActive(true);
        yield return new WaitForSeconds(3f);
        text1.SetActive(false);
        yield return new WaitForSeconds(0.01f);
        cam.enabled = true;
        yield return new WaitForSeconds(2f);
        contolltext1.SetActive(true);
        yield return new WaitForSeconds(3f);
        contolltext1.SetActive(false);
        contolltext2.SetActive(true);
        yield return new WaitForSeconds(3f);
        contolltext1.SetActive(false);
        contolltext2.SetActive(false);
    }
}
