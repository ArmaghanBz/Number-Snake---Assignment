using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Pickup : MonoBehaviour
{
    public int value = 1;
    public TMP_Text valueText;
    void Start()
    {
        valueText.text = value.ToString();
    }

    public void Collect()
    {
        gameObject.SetActive(false);
        Destroy(gameObject, 2f); 
    }

   
}
