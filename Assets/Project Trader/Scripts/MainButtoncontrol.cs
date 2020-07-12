using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainButtoncontrol : MonoBehaviour
{
    [SerializeField]
    GameObject optionwindow;
    [SerializeField]
    GameObject employeewindow;
    [SerializeField]
    GameObject questwindow;
    [SerializeField]
    GameObject sound;

    [SerializeField]
    Button saveButton;

    GameDateTimeManager gameDateTimeManager;

    // Start is called before the first frame update
    void Start()
    {
        gameDateTimeManager = FindObjectOfType<GameDateTimeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // 하루가 멈췄을 때만 저장 가능하도록
        saveButton.interactable = (gameDateTimeManager.IsStopped);
    }
    public void OpenOption()
    {
        sound.GetComponent<SoundControl>().MainButtonSound();
        optionwindow.SetActive(true);

    }


    public void OpenQuest()
    {
        sound.GetComponent<SoundControl>().MainButtonSound();
        questwindow.SetActive(true);
    }

    public void FastSave()
    {
        sound.GetComponent<SoundControl>().MainButtonSound();
        FindObjectOfType<DataSave>().GameSave();
    }
}