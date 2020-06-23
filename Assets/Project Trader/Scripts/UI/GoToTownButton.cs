using UnityEngine;
using System.Collections;

public class GoToTownButton : MonoBehaviour
{
    public void ButtonClick()
    {
        SceneLoadManager.Instance.OpenTownScene();
    }
}
