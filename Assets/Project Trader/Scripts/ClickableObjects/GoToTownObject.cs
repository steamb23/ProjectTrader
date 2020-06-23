using UnityEngine;
using System.Collections;

public class GoToTownObject : ClickableObject
{
    public override void Click()
    {
        SceneLoadManager.Instance.OpenTownScene();
    }
}
