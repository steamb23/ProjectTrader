using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Uiup : MonoBehaviour
{
    [SerializeField]
    GameObject moneyplus;
    [SerializeField]
    TextMeshProUGUI money;
    [SerializeField]
    GameObject staminaplus;
    [SerializeField]
    TextMeshProUGUI stamina;
    [SerializeField]
    GameObject awarenessplus;
    [SerializeField]
    TextMeshProUGUI awareness;
    [SerializeField]
    float speed;

    float[] up=new float[3];
    float y;
    // Start is called before the first frame update
    void Start()
    {
        y = moneyplus.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Upmoney(100);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Upstamina(10);
        }
    }

    public void Upmoney(int num)
    {
        moneyplus.SetActive(true);
        up[0] = 0.0f;
        money.text = "+" + num.ToString();
        StartCoroutine(uptext(moneyplus,0));
    }

    public void Upstamina(int num)
    {
        staminaplus.SetActive(true);
        up[1] = 0.0f;
        stamina.text = "+" + num.ToString();
        StartCoroutine(uptext(staminaplus,1));
    }

    public void Upawareness(int num)
    {
        awarenessplus.SetActive(true);
        up[2] = 0.0f;
        awareness.text = "+" + num.ToString();
        StartCoroutine(uptext(awarenessplus, 1));
        
    }

    IEnumerator uptext(GameObject game,int i)
    {
        yield return new WaitForSeconds(0.01f);
        var canvasGroup = game.GetComponent<CanvasGroup>();

        game.transform.position = new Vector2(game.transform.position.x, game.transform.position.y + speed*Time.deltaTime);
        up[i] += speed;
        canvasGroup.alpha -= 0.1f;
        if (up[i] > 1f)
        {
            up[i] = 0.0f;
            canvasGroup.alpha = 1.0f;
            game.transform.position = new Vector2(game.transform.position.x, y);
            game.SetActive(false);
        }
        else
            StartCoroutine(uptext(game,i));

    }
}
