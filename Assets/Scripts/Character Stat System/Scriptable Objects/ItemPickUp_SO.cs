using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTypeDefinitions { HEALTH, WEALTH, MANA, WEAPON, ARMOR, EMPTY}
public enum ItemArmorSubType { None, Head, Chest, Hands, Legs, Boots}

[CreateAssetMenu(fileName = "NewItem", menuName = "Swpawnable Item/New Pick Up", order = 1)]
public class ItemPickUp_SO : ScriptableObject
{
    public ItemTypeDefinitions itemType = new ItemTypeDefinitions();
    public ItemArmorSubType itemArmorSubType = new ItemArmorSubType();

    public int itemAmount = 0;
}
