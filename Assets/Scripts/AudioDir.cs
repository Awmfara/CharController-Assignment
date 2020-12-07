using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDir : MonoBehaviour
{
    [SerializeField]
    AudioSource deathSoundSource;
    [SerializeField]
    AudioSource backgroundMusic;

    public void DeathSoundPlay()
    {
        deathSoundSource.Play();
    }
    public void DeathSoundStop()
    {
        deathSoundSource.Stop();
    }
    public void MusicPlay()
    {
        backgroundMusic.Play();
    }
    public void MusicStop()
    {
        backgroundMusic.Stop();
    }

}
