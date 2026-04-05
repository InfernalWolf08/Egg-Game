using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Sources")]
    public AudioSource bgSource;
    
    [Space]
    
    [Header("BG Clips")]
    public AudioClip start;
    public AudioClip game;
    public AudioClip shop;
    public AudioClip end;
    
    [Header("SFX")]
    public AudioClip click;
    public AudioClip eggSplat;
    public AudioClip eggSplatRotten;
    public AudioClip eggCollect;
    public AudioClip eggCollectRotten;

    // Change background music to selected track
    public void changeBG(AudioClip clip)
    {
        bgSource.Stop();
        bgSource.clip = clip;
        bgSource.Play();
    }
}