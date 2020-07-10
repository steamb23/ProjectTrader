using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownTutorial : MonoBehaviour
{

    [SerializeField]
    Camera towncamera;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void TownCameraSet(int num)
    {
        switch (num)
        {
            case 9:
                towncamera.transform.position = new Vector3(-3.1f, 1.8f,-10);
                break;

            case 12:
                towncamera.transform.Translate(5,-2.1f,0);
                break;

            case 16:
                towncamera.transform.Translate(1.3f, 2.1f, 0);
                break;
        }
    }
}
