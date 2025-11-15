using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Pickup : MonoBehaviour
{
    public int value = 1;
    public TMP_Text valueText;
    public ParticleSystem collectParticles;
    void Start()
    {
        valueText.text = value.ToString();
    }

    public void Collect()
    {
        if (collectParticles != null)
        {
            var ps = Instantiate(collectParticles, transform.position, Quaternion.identity);
            ps.Play();
            Destroy(ps.gameObject, 2f);
        }
        gameObject.SetActive(false);
        Destroy(gameObject, 2f); 
        
    }

   
}
