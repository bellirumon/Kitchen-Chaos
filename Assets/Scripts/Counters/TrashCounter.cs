using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            //player has a kitchen object in hand
            player.GetKitchenObject().DestroySelf();
        }
    }

}
