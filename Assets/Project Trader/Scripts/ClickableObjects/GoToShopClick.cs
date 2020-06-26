using UnityEngine;
using System.Collections;

public class GoToShopClick : ClickableObject
{
    public override void Click()
    {
        SceneLoadManager.Instance.CloseTownScene();
    }
}
