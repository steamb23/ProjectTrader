using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

class GameTimeScaleController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] float timeScale = 1;

    public void TouchSpeedButton()
    {
        switch (timeScale)
        {
            case 1:
                timeScale = 2;
                buttonText.text = "(2x) 더 빠르게";
                break;
            case 2:
                timeScale = 3;
                buttonText.text = "(3x) 느리게";
                break;
            default:
                timeScale = 1;
                buttonText.text = "(1x) 빠르게";
                break;
        }

        Time.timeScale = timeScale;
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
    }
}
