using UnityEngine;

public class ClearCounter : BaseCounter
{

    [SerializeField] private KitchenObjectSO _kitchenObjectSO;


    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //counter has no kitchen object on it
            if (player.HasKitchenObject())
            {
                //player is carrying an object

                //transfer the object from the player to this counter
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //player is not carrying anything
            }
        }
        else
        {
            //counter already has a kitchen object on it
            if (player.HasKitchenObject())
            {
                //player is already carrying an object
            }
            else
            {
                //player is not carrying anything

                //transfer the object from the counter to the player
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
