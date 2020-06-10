using ProjectTrader;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIData : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI moneyText;
    [SerializeField] TMPro.TextMeshProUGUI staminaText;
    [SerializeField] TMPro.TextMeshProUGUI awarenessText;
    [SerializeField] TMPro.TextMeshProUGUI calendarTimeText;
    [SerializeField] TMPro.TextMeshProUGUI calendarDateText;

    [SerializeField] PlayData playData;

    int money;
    int stamina;
    int awareness;
    (int hour, int minute, int date) calendar;

    private int Money
    {
        get => this.money;
        set
        {
            this.money = value;
            this.moneyText.text = this.money < 999_999_999 ?
                $"{money:n0}" :
                $"999,999,999+";
        }
    }
    private int Stamina
    {
        get => this.stamina;
        set
        {
            this.stamina = value;

            var staminaText = value <= 999 ? $"{value}" : $"999+";
            var maxStaminaText = this.playData.MaxStamina <= 999 ? $"{this.playData.MaxStamina}" : $"999+";

            this.staminaText.text = $"{staminaText}/{maxStaminaText}";
        }
    }
    private int Awareness
    {
        get => this.awareness;
        set
        {
            this.awareness = value;

            var awarenessText = value <= 999 ? $"{value}" : $"999+";

            // TODO: 최대 인기도를 구하는 기능이 구현되면 여기에도 수정
            const string maxAwarenessText = "999";

            this.awarenessText.text = $"{awarenessText}/{maxAwarenessText}";
        }
    }
    private (int hour, int minute, int date) Calendar
    {
        get => this.calendar;
        set
        {
            this.calendar = value;

            // 튜플 분해
            (int hour, int minute, int date) = value;
            this.calendarTimeText.text = $"{hour:00}:{minute:00}";
            this.calendarDateText.text = $"{date}D";
        }
    }

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    private void Start()
    {
        playData = PlayData.CurrentData;
        ResetText();
    }

    void Update()
    {
        UpdateText();
    }

    void UpdateText()
    {
        if (Money != playData.Money)
        {
            Money = playData.Money;
        }
        if (Stamina != playData.Stamina)
        {
            Stamina = playData.Stamina;
        }
        if (Awareness != playData.Awareness)
        {
            Awareness = (int)playData.Awareness;
        }
        var cal = (playData.Date.Hour, playData.Date.Minute, playData.Date.Day);
        if (Calendar != cal)
        {
            Calendar = cal;
        }
    }

    void ResetText()
    {
        Money = playData.Money;
        Stamina = playData.Stamina;
        Awareness = (int)playData.Awareness;
        var cal = (playData.Date.Hour, playData.Date.Minute, playData.Date.Day);
        Calendar = cal;
    }
}
