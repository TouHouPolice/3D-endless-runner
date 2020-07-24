using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class  AudioController :MonoBehaviour {
    private GameObject player;
    public  AudioSource click;
    public  AudioSource hit;
    public  AudioSource powerup;
    public  AudioSource coin;
    public  AudioSource die;
    public  AudioSource jump;
    public AudioSource magic;
    
    
    public AudioSource sliding;
    public AudioSource bossFightBGM;
    public AudioSource mainBGM;
    // Use this for initialization
    public Slider volumeSlider;

    public static bool keepFadingIn;
    public static bool keepFadingOut;

    float masterVolume = 0.5f;

    private void Start()
    {
        player=GameObject.Find("Player");
    }
    private void Update()
    {
        if (player)
        {
            transform.position = player.transform.position;
        }
    }

    public void SetMasterVolume()
    {
        masterVolume = volumeSlider.value;
        AudioListener.volume = masterVolume;
    }

   public static IEnumerator MusicFadeIn(AudioSource music, float speed,float maxVolume)
    {
       /* keepFadingIn = true;
        keepFadingOut = false;*/

        music.volume = 0;

        float audioVolume = music.volume;

        while (music.volume < maxVolume)
        {
            audioVolume += speed;
            music.volume = audioVolume;
            yield return new WaitForSeconds(0.1f);
        }
    }

   public static IEnumerator MusicFadeOut(AudioSource music,float speed)
    {
        /*keepFadingIn = false;
        keepFadingOut = true;*/

        music.volume = 0;

        float audioVolume = music.volume;

        while (music.volume >= speed )
        {
            audioVolume -= speed;
            music.volume = audioVolume;
            yield return new WaitForSeconds(0.1f);
        }
    }


    public void MusicTransition(AudioSource musicIn,AudioSource musicOut)
    {
        musicIn.Play();
        StartCoroutine(MusicFadeOut(musicOut, 0.01f));
        StartCoroutine(MusicFadeIn(musicIn, 0.005f, 0.12f));
        
    }

}
