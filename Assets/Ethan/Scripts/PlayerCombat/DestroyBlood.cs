using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlood : MonoBehaviour
{

    public GameObject blood;

    void Start()
    {
        StartCoroutine(DestroyTheBlood());
         
    }
 
    IEnumerator DestroyTheBlood()
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(blood);
    }
}
