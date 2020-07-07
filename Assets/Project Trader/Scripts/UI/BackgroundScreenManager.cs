using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BackgroundScreenManager : MonoBehaviour
{
    [SerializeField] Image backgroundImage;
    [SerializeField] GameObject[] targetObjects;

    // 매 프레임마다 오브젝트 목록을 확인하고 켜진 오브젝트가 있으면 백그라운드도 같이 켭니다.
    void LateUpdate()
    {
        bool isActive = false;

        foreach (var targetObject in targetObjects)
        {
            if (targetObject.activeInHierarchy)
            {
                isActive = true;
                break;
            }
        }

        backgroundImage.gameObject.SetActive(isActive);
    }
}
