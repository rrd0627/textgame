using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMManager : MonoBehaviour
{
    static public BGMManager instance;

    public AudioClip[] clips;
    public float BGM_Volume;
    public Slider bgm_slider;

    private AudioSource source;
    private WaitForSeconds waitTime;
    

    private void Awake()
    {
        BGM_Volume = 1;
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }

    }  //--------------인스턴스화를 위함 ----
       // Start is called before the first frame update


    void Start()
    {
        source = this.GetComponent<AudioSource>();

        source.volume = BGM_Volume;

        waitTime = new WaitForSeconds(0.01f);

        Play(0);
    }

    public void SetVolume(Slider vol)
    {
        source.volume = vol.value;
        BGM_Volume = vol.value;
    }
    public float GetVolumn()
    {
        return source.volume;
    }
    public void Play(int _playMusicTrack)
    {
        source.clip = clips[_playMusicTrack];
        source.volume = source.volume;

        source.Play();
    }

    public void Pause()
    {
        source.Pause();
    }
    public void UnPause()
    {
        source.UnPause();
    }

    public void Stop()
    {
        source.Stop();
    }
    public void FadeOutMusic()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutMusicCor());
    }
    IEnumerator FadeOutMusicCor()
    {
        float i = BGM_Volume;
        while (true)
        {
            source.volume = i;

            i -= 0.01f;

            yield return waitTime;

            if (i <= 0)
                break;
        }
    }
    public void FadeInMusic()
    {
        StopAllCoroutines();
        StartCoroutine(FadeInMusicCor());
    }
    IEnumerator FadeInMusicCor()
    {
        float i = 0;

        while (true)
        {
            source.volume = i;

            i += 0.01f;

            yield return waitTime;
            if (i >= BGM_Volume)
               break;
        }
    }
}