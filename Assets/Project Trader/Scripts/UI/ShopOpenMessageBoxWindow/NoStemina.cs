using ProjectTrader;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoStemina : MonoBehaviour
{
    [SerializeField]
    GameObject openshopwindow;
    [SerializeField]
    GameObject gameover;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Click()
    {
        var playData = PlayData.CurrentData;
        if (playData !=null)
        {
            if (playData.Money >= 1000)
            {
                playData.Money -= 1000;
                FindObjectOfType<Uiup>().Upstamina(playData.MaxStamina - playData.Stamina);
                playData.Stamina = playData.MaxStamina;
                openshopwindow.GetComponent<ShopOpenMessageBoxWindow>().OpenButtonClick();
                gameObject.SetActive(false);
            }
            else
            {
                gameover.SetActive(true);
            }
        }
    }
}
