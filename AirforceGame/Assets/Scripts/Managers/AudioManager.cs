using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioManager audioManager;
    public AudioSource ice;
    public AudioSource fire;
    public AudioSource stepGrass;
    public AudioSource hit;
    public AudioSource pause;
    public AudioSource unPause;
    void Awake()
    {
        if (audioManager != null)
        {
            Destroy(gameObject);
        }
        else
        {
            audioManager = this;
        }
    }
    public void PlaySound(AudioSource soundSource)
    {
        soundSource.Stop(); //오디오 스탑 시키고 처음부터 하게. 반복할동안 겹치지 안토록 방지용
        soundSource.Play();
    }
}
