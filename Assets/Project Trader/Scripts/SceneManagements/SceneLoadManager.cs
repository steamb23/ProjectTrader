using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public enum ShopScene
    {
        Shop1,
        Shop2,
        Shop3
    }

    public void OpenTownScene()
    {
        var rootObject = GameObject.Find("TownSceneRoot");

        rootObject.SetActive(true);
    }

    public void CloseTownScene()
    {
        var rootObject = GameObject.Find("TownSceneRoot");

        rootObject.SetActive(false);
    }

    public void LoadScene(ShopScene shopScene)
    {
        // 초기화 후 다시 불러오기
        LoadScene("GameScene");
        string shopSceneName;
        switch (shopScene)
        {
            case ShopScene.Shop1:
                shopSceneName = "Shop1Scene";
                break;
            case ShopScene.Shop2:
                shopSceneName = "Shop2Scene";
                break;
            case ShopScene.Shop3:
                shopSceneName = "Shop3Scene";
                break;
            default:
                shopSceneName = "";
                break;
        }
        LoadScene(shopSceneName, LoadSceneMode.Additive);
        LoadScene("TownScene", LoadSceneMode.Additive);
    }

    public void LoadScene(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
    {
        SceneManager.LoadScene(sceneName, loadSceneMode);
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    //public static void UnloadShopScene()
    //{
    //    var sceneCount = SceneManager.sceneCount;
    //    for(int i = 0; i < sceneCount; i++)
    //    {
    //        var scene = SceneManager.GetSceneAt(i);
    //        switch (scene.name)
    //        {
    //            case "Shop1Scene":
    //            case "Shop2Scene":
    //            case "Shop3Scene":
    //                break;
    //        }
    //    }
    //}


    //    private ShopScenes shopScene;

    //    public ShopScenes ShopScene => shopScene;
    //    private void LoadScene(ShopScenes shopScene)
    //    {
    //        switch (ShopScene)
    //        {
    //            case ShopScenes.Shop1:
    //            case ShopScenes.Shop2:
    //            case ShopScenes.Shop3:
    //                break;
    //        }
    //    }
    //    private void UnloadScene()
    //    {
    //        var shop1sceneAsync = SceneManagement.SceneManager.UnloadSceneAsync("Shop1Scene");
    //        var shop2sceneAsync = SceneManagement.SceneManager.UnloadSceneAsync("Shop2Scene");
    //        var shop3sceneAsync = SceneManagement.SceneManager.UnloadSceneAsync("Shop3Scene");
    //    }

    //#if UNITY_EDITOR
    //    private void Update()
    //    {

    //    }
    //#endif
}
