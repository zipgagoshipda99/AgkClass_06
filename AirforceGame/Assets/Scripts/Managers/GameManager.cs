using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float worldSpeed = 1.25f;
    public static GameManager gameManager;
    void Awake()
    {
        if(gameManager != null)
        {
            Destroy(gameObject);
        }
        else
        {
            gameManager = this;
        }
    }
}
