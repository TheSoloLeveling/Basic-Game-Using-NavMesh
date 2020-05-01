using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewStats", menuName ="Character/Stats", order = 1)]
public class CharacterStats_SO : ScriptableObject
{

    [System.Serializable] // serialize the class and display it in the inspector
    public class CharLevelUps
    {
        public int maxHealth;
        public int maxMana;
        public int maxWealth;
        public int baseDamage;
        public float baseResistance;
        public float maxEncumberance;
    }
    #region Fields

    public bool setManually = false;  // are the stats set manually or dynamiclly
    public bool saveDataOnClose = false; // save data

    public ItemPickUp weapon { get; private set; } // define the accessiblity of the variable
    public ItemPickUp headArmor { get; private set; }
    public ItemPickUp chestArmor { get; private set; }
    public ItemPickUp handArmor { get; private set; }
    public ItemPickUp legtArmor { get; private set; }
    public ItemPickUp footArmor { get; private set; }
    public ItemPickUp misc1 { get; private set; }  // misc for another type of items
    public ItemPickUp misc2 { get; private set; }  

    public int maxHealth = 0;
    public int currentHealth = 0;

    public int maxWealth = 0;
    public int currentWealth = 0;

    public int maxMana = 0;
    public int currentMana = 0;

    public int baseDamage = 0;
    public int currentDamage = 0;

    public float baseResistance = 0f;
    public float currentResistance = 0f;

    public float currentEmcumbrance = 0f; // claim property (inventory)

    public int chaExperience = 0;
    public int charLevel = 0;

    public CharLevelUps[] charLevelUps;

    #endregion

    #region Stat Increasers

    public void ApplyHealth(int healthAmount)
    {
        if ((currentHealth + healthAmount) > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += healthAmount;
        }
    }

    public void ApplyMana(int manaAmount)
    {
        if ((currentMana + manaAmount) > maxMana)
        {
            currentMana = maxMana;
        }
        else
        {
            currentMana += manaAmount;
        }
    }

    public void GiveWealth(int wealthAmount)
    {
        if ((currentWealth + wealthAmount) > maxWealth)
        {
            currentWealth = maxWealth;
        }
        else
        {
            currentWealth += wealthAmount;
        }
    }

    public void EquipWeapon(ItemPickUp weaponPickUp, CharacterInventory charInventory, GameObject weaponSlot) // equip the itemPickUp and put it in the inventory with setting a slor for it
    {
        weapon = weaponPickUp;
        currentDamage = baseDamage + weapon.itemDefinition.itemAmount;
    }

    #endregion

    #region Stat Reducers

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            //Death();
        }
    }

    public void TakeMana(int amount)
    {
        currentMana -= amount;

        if (currentMana < 0)
        {
            currentMana = 0;
        }
    }

    #endregion

    #region Character Level Up and Death

    private void Death()
    {
        // Death State trigger Respawn by reloading scene
        //Display the Death animation
    }

    

    #endregion
}
