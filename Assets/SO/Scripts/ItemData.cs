using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipable,
    Consumable,
}

public enum ConsumableType
{
    Health,
    Attack,
    Defence,
    AttackSpeed,
    Exp
}

public enum EquipType
{
    Weapon,
    Helmet,
    Armor,
    SubEquipment
}

public enum StatusType
{
    Health,
    Attack,
    Defence,
    AttackSpeed
}

[Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
    public float time;
}

[Serializable]
public class ItemDataEquip
{
    public StatusType statusType;
    public int value;
}

[CreateAssetMenu(fileName = "Item", menuName = "Data/New Item")]
public class ItemData : ScriptableObject
{
    [Header("info")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackCount;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;

    [Header("Equip")]
    public EquipType equipType;
    public ItemDataEquip[] equips;
}
