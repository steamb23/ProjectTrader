using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Camera camera;

    public void CameraActive(bool isActive)
    {
        camera.gameObject.SetActive(isActive);
    }
}
