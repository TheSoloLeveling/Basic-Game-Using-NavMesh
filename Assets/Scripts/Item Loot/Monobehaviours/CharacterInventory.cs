using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{

    #region Variable Declarations
    public static CharacterInventory instance;
    #endregion

    #region Initializations

    private void Start()
    {
        instance = this;
    }
    #endregion

    private void Update()
    {
        
    }

    public void StoreItem(ItemPickUp itemToStore)
    {

    }

    void TryPickUp()
    {

    }

    bool AddItemToInv(bool finishedAdding)
    {
        return true;
    }

  //  private void AddItemToHotBar(InventoryEntry itemForHotBar)
   // {

   // }

    void Displayinventory()
    {

    }

    void FillInvetoryDisplay()
    {

    }

    public void TriggerItemUse(int itemToUseID)
    {

    }


}
