using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioVolumeManager : MonoBehaviour
{
    public const float DefaultMusicVolume = 0.7f;
    public const float DefaultEffectVolume = 1f;

    [SerializeField] AudioMixer audioMixer;

    // Use this for initialization
    void Start()
    {
        // 볼륨 가져오기

        // 음악 볼륨
        var musicVolume = Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume", DefaultMusicVolume)) * 20;
        if (float.IsInfinity(musicVolume))
            musicVolume = float.MinValue;
        audioMixer.SetFloat("MusicVolume", musicVolume);

        // 효과 볼륨
        var effectVolume = Mathf.Log10(PlayerPrefs.GetFloat("EffectVolume", DefaultEffectVolume)) * 20;
        if (float.IsInfinity(effectVolume))
            effectVolume = float.MinValue;
        audioMixer.SetFloat("EffectVolume", effectVolume);
    }
}
