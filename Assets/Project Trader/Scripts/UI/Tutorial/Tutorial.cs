using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectTrader;

//튜토리얼을 보면 tutorial스크립트 자체를 꺼버리기
public class Tutorial : MonoBehaviour
{
    [SerializeField]
    GameObject tutorialwindow;
    [SerializeField]
    GameObject[] tutorial;
    [SerializeField]
    int num;
    GameObject town;
    
    // Start is called before the first frame update
    void Start()
    {
        var playData = PlayData.CurrentData;
        if (playData.Tutorial == true)
            tutorialwindow.SetActive(false);
        num = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChangeTutorial();
        }
    }

    void ChangeTutorial()
    {
        num++;

        if (num >= tutorial.Length)
        {
            tutorial[tutorial.Length - 1].SetActive(false);
            var playData = PlayData.CurrentData;
            playData.Tutorial = true;
            tutorialwindow.SetActive(false);
            return;
        }
      

        if (num != 1)
            tutorial[num - 1].SetActive(false);
        tutorial[num].SetActive(true);
        if (num >= 3&&num<9)
        {
            tutorialwindow.GetComponent<Emptutorial>().EmptutorialStart(num);
        }
        if (num >= 9 && num < 14)
        {
            if (num == 9)
            {
                tutorialwindow.GetComponent<TownShopTutorial>().MoveTown(num);
            }
            if (num == 9 || num == 12)
                TownTutorial(num);
            tutorialwindow.GetComponent<TownShopTutorial>().ShopWindowTutorial(num);
        }
        if (num >= 13 && num<17)
        {
            tutorialwindow.GetComponent<TownShopTutorial>().MakerWindowTutorial(num);
            if(num==16)
                TownTutorial(num);
        }
        if (num >=17)
        {
            if(num==17)
                tutorialwindow.GetComponent<TownShopTutorial>().MoveTown(num);
            tutorialwindow.GetComponent<DisplayTutorial>().DisplayTutorialSet(num);
        }
    }

    //town튜토리얼용 카메라 호출
    void TownTutorial(int num)
    {
        town = GameObject.Find("Towntutorial");
        town.GetComponent<TownTutorial>().TownCameraSet(num);
    }
}
