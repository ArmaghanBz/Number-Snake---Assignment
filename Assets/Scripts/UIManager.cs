using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public GameObject gameOverPanel;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

}
