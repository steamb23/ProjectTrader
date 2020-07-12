using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    [SerializeField] Slider bgm;
    [SerializeField] Slider effect;
    [SerializeField] AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        bgm.value = PlayerPrefs.GetFloat("MusicVolume", AudioVolumeManager.DefaultMusicVolume);
        effect.value = PlayerPrefs.GetFloat("EffectVolume", AudioVolumeManager.DefaultEffectVolume);
    }

    // Update is called once per frame
    void Update()
    {
        BgmSlider();
        EffectSlider();
    }

    void BgmSlider()
    {
        var volume = Mathf.Log10(bgm.value) * 20;
        if (float.IsInfinity(volume))
            volume = float.MinValue;
        audioMixer.SetFloat("MusicVolume", volume);
        PlayerPrefs.SetFloat("MusicVolume", bgm.value);
    }

    void EffectSlider()
    {
        var volume = Mathf.Log10(effect.value) * 20;
        if (float.IsInfinity(volume))
            volume = float.MinValue;
        audioMixer.SetFloat("EffectVolume", volume);
        PlayerPrefs.SetFloat("EffectVolume", effect.value);
        //FindObjectOfType<SoundControl>().audioSource.volume = effect.value;
        //FindObjectOfType<SoundControl>().cleanAudioSource.volume = effect.value;
    }

    public void BgmPlusButton()
    {
        if(bgm.value<=1f)
            bgm.value += 0.1f;
    }

    public void BgmMinusButton()
    {
        if(bgm.value>0)
            bgm.value -= 0.1f;
    }

    public void EffectPlusButton()
    {
        if (effect.value <= 1f)
            effect.value += 0.1f;
    }

    public void EffectMinusButton()
    {
        if (effect.value > 0)
            effect.value -= 0.1f;
    }
}
