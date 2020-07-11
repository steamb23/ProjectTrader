using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenOption()
    {
        sound.GetComponent<SoundControl>().MainButtonSound();
        optionwindow.SetActive(true);

    }


    public void OpenQuest()
    {
        sound.GetComponent<SoundControl>().MainButtonSound();
    }

    public void FastSave()
    {
        sound.GetComponent<SoundControl>().MainButtonSound();
    }
}