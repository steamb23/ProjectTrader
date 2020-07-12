using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MakeSlot : MonoBehaviour
{
    public int code;
    GameObject go;
    GameObject sound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSlotInData(int cod)
    {
        code = cod;
        //UnityEngine.Debug.Log("세팅완료");
    }

    public void MakerslotPushButton()
    {
        FindObjectOfType<SoundControl>().ButtonSound2();
        FindObjectOfType<MakerUI>().SetMakerBg(code);

        //GameObject go = GameObject.Find("makeroom");
        //go.GetComponent<MakerUI>().SetMakerBg(count, code);

    }

}
