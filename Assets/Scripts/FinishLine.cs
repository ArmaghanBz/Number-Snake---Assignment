using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public void ReachFinish()
    {
        Debug.Log("Finished! Level end logic here.");
        //Time.timeScale = 0f; 
        UIManager.Instance.gameOverPanel.SetActive(true);
    }
}
