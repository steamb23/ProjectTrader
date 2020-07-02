using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildshopClick : ClickableObject
{
    public override void Click()
    {
        var guildUI = FindObjectOfType<ShopWindow>();
        guildUI.OpenShopWindow();
    }
}
