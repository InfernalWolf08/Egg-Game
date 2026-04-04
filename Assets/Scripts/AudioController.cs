using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Sources")]
    public AudioSource bgSource;

    [Header("BG Clips")]
    public AudioClip start;
    public AudioClip game;
    public AudioClip shop;
    public AudioClip end;
    
    [Header("SFX")]
    public AudioClip click;
}