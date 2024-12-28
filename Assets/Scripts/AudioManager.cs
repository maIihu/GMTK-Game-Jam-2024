using UnityEngine;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sFXSource;


    public AudioClip background;
    public AudioClip powerUp;
    public AudioClip eating;
    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySfx(AudioClip clip)
    {
        sFXSource.PlayOneShot(clip);
    }
}