using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownTutorial : MonoBehaviour
{

    [SerializeField]
    Camera towncamera;

    float x;
    float y;
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
                //x = -3.1f;
                //y = 1.8f;
                towncamera.transform.position = new Vector3(-3.1f, 1.8f,-10);
                break;
            case 12:
                //x = 1.9f;
                //y = -0.3f;
                //towncamera.transform.position = new Vector3(1.9f, -0.3f ,-10);
                towncamera.transform.Translate(5,-2.1f,0);
                UnityEngine.Debug.Log("카메라 위치 변경 12!");
                break;
            case 16:
                //x = 3.2f;
                //y = 1.8f;
                //towncamera.transform.position = new Vector3(3.2f, 1.8f, -10);
                towncamera.transform.Translate(1.3f, 2.1f, 0);
                UnityEngine.Debug.Log("카메라 위치 변경 16!");
                break;
        }
        //towncamera.transform.position =new Vector3(x,y, -10);
    }
}
