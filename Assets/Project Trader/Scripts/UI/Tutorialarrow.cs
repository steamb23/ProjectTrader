using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorialarrow : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI explain;
    [SerializeField]
    string exstring;
    // Start is called before the first frame update
    void Start()
    {
        setup();
    }

    void setup()
    {
        explain.text = exstring.Replace("\\n","\n");
    }
}
