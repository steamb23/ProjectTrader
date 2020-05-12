using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InCoinScript : MonoBehaviour
{
    GameObject go;
    // Start is called before the first frame update
    void Start()
    {
        go = GameObject.Find("DropItem");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClick()
    {
        go.GetComponent<DropItem>().GetCoin();
    }
}