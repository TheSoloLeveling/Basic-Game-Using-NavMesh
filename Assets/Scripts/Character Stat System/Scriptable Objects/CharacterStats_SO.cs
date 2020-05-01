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
    public ItemPickUp handsArmor { get; private set; }
    public ItemPickUp legsArmor { get; private set; }
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

    public float currentEncumbrance = 0f; // claim property (inventory)
    public float maxEmcumbrance = 0f;

    public int charExperience = 0;
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

    public void EquipArmor(ItemPickUp armorPickUp, CharacterInventory charInventory)
    {
        switch(armorPickUp.itemDefinition.itemArmorSubType) // switch the index of the enum in the item defintion of the itemPickUp enter as parameter
        {
            case ItemArmorSubType.Head:        // if its a head armor 
                headArmor = armorPickUp;        // the current head armor will be the armorPickUp
                currentResistance += armorPickUp.itemDefinition.itemAmount; // increase the resistance based on the item amount of the itemPickUp enter as parameter 
                break;
            case ItemArmorSubType.Chest:    // the same thing for all the type of armor in the enum
                chestArmor = armorPickUp;
                currentResistance += armorPickUp.itemDefinition.itemAmount;
                break;
            case ItemArmorSubType.Hands:    // the same thing for all the type of armor in the enum
                handsArmor = armorPickUp;
                currentResistance += armorPickUp.itemDefinition.itemAmount;
                break;
            case ItemArmorSubType.Legs:    // the same thing for all the type of armor in the enum
                legsArmor = armorPickUp;
                currentResistance += armorPickUp.itemDefinition.itemAmount;
                break;
            case ItemArmorSubType.Boots:    // the same thing for all the type of armor in the enum
                footArmor = armorPickUp;
                currentResistance += armorPickUp.itemDefinition.itemAmount;
                break;
        }
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

    public bool UnEquipWeapon(ItemPickUp weaponPickUp, CharacterInventory charInventory, GameObject weaponSlot)
    {
        bool previousWeaponSame = false;
        if (weapon != null)
        {
            if(weapon == weaponPickUp)
            {
                previousWeaponSame = true;
            }

            DestroyObject(weaponSlot.transform.GetChild(0).gameObject);
            weapon = null;
            currentDamage = baseDamage;
        }

        return previousWeaponSame;
    }

    public bool UnEquipArmor(ItemPickUp armorPickUp, CharacterInventory charInventory)
    {
        bool previousArmorSame = false;

        switch (armorPickUp.itemDefinition.itemArmorSubType)
        {
            case ItemArmorSubType.Head:
                if (headArmor != null)
                {
                    if (headArmor == armorPickUp)
                    {
                        previousArmorSame = true;
                    }

                    currentResistance -= armorPickUp.itemDefinition.itemAmount;
                    headArmor = null;
                }
                break;
            case ItemArmorSubType.Chest:
                if (headArmor != null)
                {
                    if (chestArmor == armorPickUp)
                    {
                        previousArmorSame = true;
                    }

                    currentResistance -= armorPickUp.itemDefinition.itemAmount;
                    chestArmor = null;
                }
                break;    
            case ItemArmorSubType.Hands:
                if (handsArmor != null)
                {
                    if (handsArmor == armorPickUp)
                    {
                        previousArmorSame = true;
                    }

                    currentResistance -= armorPickUp.itemDefinition.itemAmount;
                    handsArmor = null;
                }
                break;
            case ItemArmorSubType.Legs:
                if (legsArmor != null)
                {
                    if (legsArmor == armorPickUp)
                    {
                        previousArmorSame = true;
                    }

                    currentResistance -= armorPickUp.itemDefinition.itemAmount;
                    legsArmor = null;
                }
                break;
            case ItemArmorSubType.Boots:
                if (footArmor != null)
                {
                    if (footArmor == armorPickUp)
                    {
                        previousArmorSame = true;
                    }

                    currentResistance -= armorPickUp.itemDefinition.itemAmount;
                    footArmor = null;
                }
                break;
        }

        return previousArmorSame;
    }

    #endregion

    #region Character Level Up and Death

    private void Death()
    {
        // Death State trigger Respawn by reloading scene
        //Display the Death animation
    }

    private void LevelUp()
    {
        charLevel += 1;
        //display level up vizualization

        maxHealth = charLevelUps[charLevel - 1].maxHealth;
        maxMana = charLevelUps[charLevel - 1].maxMana;
        maxWealth = charLevelUps[charLevel - 1].maxWealth;
        baseDamage = charLevelUps[charLevel - 1].baseDamage;
        baseResistance = charLevelUps[charLevel - 1].baseResistance;
        maxEmcumbrance = charLevelUps[charLevel - 1].maxEncumberance;
    }



    #endregion

    #region SaveCharacterData
    public void saveCharacterData()
    {
        // saveDataOnClose = true;
       // EditorUtility.SetDirty(this);  // a flag that tells unity that the data of this object need to be resaved because its dirty
    }


    #endregion
}

