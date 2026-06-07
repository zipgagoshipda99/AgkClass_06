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
    public AudioSource boom2;
    public AudioSource hitObstacle;
    
    public AudioSource Pew;
    public AudioClip[] PewSoundFiles;

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
    public void PlayModifiedSound(AudioSource soundSource)
    {
        float randomPitch = Random.Range(0.7f, 1.3f);
        soundSource.Stop(); //오디오 스탑 시키고 처음부터 하게. 반복할동안 겹치지 안토록 방지용
        soundSource.Play();
    }
    public void PlayRandomSoundWithNoCutoff(AudioSource soundSource, AudioClip[] audioClips)
     {
        if(audioClips.Length == 0) return;
        int randomIndex = Random.Range(0, audioClips.Length); //length 가 3이면 0~2 에서만 골름... ㅇㅇ
        float RandomVolume = Random.Range(0.1f, 0.9f);
        AudioClip selectedClip = audioClips[randomIndex];
        soundSource.PlayOneShot(selectedClip, RandomVolume);
    }
}
