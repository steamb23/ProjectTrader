using UnityEngine;
using System.Collections;

public class GoToShopObject : ClickableObject
{
    public override void Click()
    {
        SceneLoadManager.Instance.CloseTownScene();
    }
}
