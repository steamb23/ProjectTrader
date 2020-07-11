using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    [SerializeField] Slider bgm;
    [SerializeField] Slider effect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EffectSlider();
    }

    void BgmSlider()
    {

    }

    void EffectSlider()
    {
        FindObjectOfType<SoundControl>().audioSource.volume = effect.value;
        FindObjectOfType<SoundControl>().cleanAudioSource.volume = effect.value;
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
