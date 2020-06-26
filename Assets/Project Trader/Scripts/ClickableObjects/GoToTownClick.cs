using UnityEngine;
using System.Collections;

public class GoToTownClick : ClickableObject
{
    public override void Click()
    {
        SceneLoadManager.Instance.OpenTownScene();
    }
}
