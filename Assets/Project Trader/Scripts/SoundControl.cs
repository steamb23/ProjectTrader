using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    //각종 일반 버튼
    [SerializeField] AudioClip buttonDown;
    //각종 일반 버튼2 슬롯 선택
    [SerializeField] AudioClip buttonDown2;
    //각종 일반 버튼3 (알바 해고
    [SerializeField] AudioClip buttonDown3;
    //나가기 버튼
    [SerializeField] AudioClip exitButton;
    //화면 터치
    [SerializeField] AudioClip click;
    //메인화면 버튼(옵션)
    [SerializeField] AudioClip mainButton;
    //계산
    [SerializeField] AudioClip pay;
    //구매
    [SerializeField] AudioClip buy;
    //등급심사 성공
    [SerializeField] AudioClip levelUp;
    //등급심사 실패
    [SerializeField] AudioClip levelFail;
    //게임 시작시
    [SerializeField] AudioClip gameStart;
    //청소
    [SerializeField] AudioClip cleanUp;

    public AudioSource audioSource;
    public AudioSource cleanAudioSource;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickSound();
        }
    }

    public void ClickSound()
    {
        if (audioSource.isPlaying == false)
        {
            audioSource.clip = click;
            audioSource.Play();
        }
    }

    public void ButtonSound()
    {
        audioSource.clip = buttonDown;
        audioSource.Play();
    }

    public void ButtonSound2()
    {
        audioSource.clip = buttonDown2;
        audioSource.Play();
    }

    public void ButtonSound3()
    {
        audioSource.clip = buttonDown3;
        audioSource.Play();
    }

    public void ExitButtonSound()
    {
        audioSource.clip = exitButton;
        audioSource.Play();
    }

    public void MainButtonSound()
    {
        audioSource.clip = mainButton;
        audioSource.Play();
    }

    public void PaySound()
    {
        audioSource.clip = pay;
        audioSource.Play();
    }

    public void BuySound()
    {
        audioSource.clip = buy;
        audioSource.Play();
    }

    public void LevelUpSound()
    {
        audioSource.clip = levelUp;
        audioSource.Play();
    }

    public void LevelFailSound()
    {
        audioSource.clip = levelFail;
        audioSource.Play();
    }

    public void GameStartSound()
    {
        audioSource.clip = gameStart;
        audioSource.Play();
    }

    public void CleanUpSound()
    {
        cleanAudioSource.clip = cleanUp;
        cleanAudioSource.Play();
    }

    public void CleanUpStop()
    {
        cleanAudioSource.Stop();
    }

}
