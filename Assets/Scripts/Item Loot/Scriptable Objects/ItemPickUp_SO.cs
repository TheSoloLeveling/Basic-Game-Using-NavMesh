using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTypeDefinitions { HEALTH, WEALTH, MANA, WEAPON, ARMOR, EMPTY}
public enum ItemArmorSubType { None, Head, Chest, Hands, Legs, Boots}

[CreateAssetMenu(fileName = "NewItem", menuName = "Swpawnable Item/New Pick Up", order = 1)]
public class ItemPickUp_SO : ScriptableObject
{
    public string itemName = "New Item";
    public ItemTypeDefinitions itemType = new ItemTypeDefinitions();
    public ItemArmorSubType itemArmorSubType = new ItemArmorSubType();

    public int itemAmount = 0;
    public int spawnChanceWeight = 0;

    public Material itemMaterial = null;
    public Sprite itemIcon = null;

    public Rigidbody itemSpawnObject = null; // the object used in gameplay
    public Rigidbody weaponSlotObject = null; // the object that spawn the weapon

    public bool isEquiped = false;
    public bool isInteractable = false;
    public bool isStorable = false;
    public bool isUnique = false; // is gonna be cloned
    public bool isIndestructable = false;
    public bool isQuestItem = false;
    public bool isStackable = false;
    public bool destroyOnUse = false;
    public float itemWeight = 0f;
}
