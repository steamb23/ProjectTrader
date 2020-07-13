using UnityEngine;
using System.Collections;
using JetBrains.Annotations;

public class QuitManager : MonoBehaviour
{
    [SerializeField] GameObject quitMessageBoxWindow;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            quitMessageBoxWindow.SetActive(true);
        }
    }

    public void Quit()
    {
        Debug.Log("종료");
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
