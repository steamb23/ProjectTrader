using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

class GameTimeScaleController : MonoBehaviour
{
    [SerializeField] Sprite x1Icon;
    [SerializeField] Sprite x2Icon;
    [SerializeField] Image timeIcon;
    [SerializeField] float timeScale = 1;

    private void Awake()
    {
        timeScale = Time.timeScale;
    }

    private void Update()
    {
        timeScale = Time.timeScale;

        // 현재 타임 스케일에 맞는 아이콘 적용
        switch (timeScale)
        {
            case 1:
                timeIcon.sprite = x1Icon;
                break;
            case 2:
            default:
                timeIcon.sprite = x2Icon;
                break;
        }
    }

    public void TouchSpeedButton()
    {
        switch (timeScale)
        {
            case 1:
                timeScale = 2;
                break;
            case 2:
            default:
                timeScale = 1;
                break;
        }

        Time.timeScale = timeScale;
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
    }
}
