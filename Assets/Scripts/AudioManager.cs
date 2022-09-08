using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource source1;
    public AudioSource source2;
    public AudioClip clickSound;
    public AudioClip spinButtonSound;
    public AudioClip spinSound;
    public AudioClip spin2Sound;
    public AudioClip spinLastSound;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip wheelResizeSound;
    public AudioClip nextWheelSound;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudio1(AudioClip clip)
    {
        source1.PlayOneShot(clip);
    }
    public void PlayAudio2(AudioClip clip)
    {
        source2.PlayOneShot(clip);
    }

    public async void PlayAudioDelayed(AudioClip clip, int delay)
    {
        await Task.Delay(delay);
        source2.PlayOneShot(clip);
    }
}
