using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SettingVolumn : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider soundSlider;
    // Start is called before the first frame update
    float temp;
    void Start()
    {
        if (audioMixer == null)
        {
            audioMixer = GetComponent<AudioMixer>();

            if (audioMixer != null)
                audioMixer.FindSnapshot("MasterMixer");
        }

        audioMixer.GetFloat("MusicVol", out temp);
        musicSlider.value = temp;
        audioMixer.GetFloat("SoundVol", out temp);
        soundSlider.value = temp;

    }


    public void ChangeMusic(float volume)
    {
        audioMixer.SetFloat("MusicVol", volume);
    }

    public void ChangeSound(float volume)
    {
        audioMixer.SetFloat("SoundVol", volume);
    }
}
