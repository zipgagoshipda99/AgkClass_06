using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool isPaused;
    // Start is called before the first frame update
    
    private IEnumerator waitPause()
    {
        yield return new WaitForSecondsRealtime(0.5f);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
