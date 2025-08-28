using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField] ItemSlot equipWeaponSlot;
    [SerializeField] ItemSlot equipArmorSlot;
    [SerializeField] ItemSlot equipHelmetSlot;
    [SerializeField] ItemSlot equipSubEquipmentSlot;

    ItemData equipWeapon;
    ItemData equipArmor;
    ItemData equipHelmet;
    ItemData equipSubEquipment;

    StatHandler stat;

    private void Awake()
    {
        stat = GetComponent<StatHandler>();
    }

    public void Equip(ItemSlot item)
    {
        switch (item.Data.equipType)
        {
            case EquipType.Weapon:
                if(equipWeaponSlot.Data != null)
                    UnEquip(equipWeaponSlot);
                equipWeaponSlot = item;
                equipWeapon = item.Data;
                break;
            case EquipType.Armor:
                if(equipArmorSlot.Data != null)
                    UnEquip(equipArmorSlot);
                equipArmorSlot = item;
                equipArmor = item.Data;
                break;
            case EquipType.Helmet:
                if(equipHelmetSlot.Data != null)
                    UnEquip(equipHelmetSlot);
                equipHelmetSlot = item;
                equipHelmet = item.Data;
                break;
            case EquipType.SubEquipment:
                if(equipSubEquipmentSlot.Data != null)
                    UnEquip(equipSubEquipmentSlot);
                equipSubEquipmentSlot = item;
                equipSubEquipment = item.Data;
                break;
        }
        foreach (ItemDataEquip equip in item.Data.equips)
        {
            stat.EquipItemStat(equip.statusType, equip.value);
        }
    }

    public void UnEquip(ItemSlot item)
    {
        switch (item.Data.equipType)
        {
            case EquipType.Weapon:
                equipWeaponSlot.isEquip = false;
                equipWeaponSlot = null;
                equipWeapon = null;
                break;
            case EquipType.Armor:
                equipArmorSlot.isEquip = false;
                equipArmorSlot = null;
                equipArmor = null;
                break;
            case EquipType.Helmet:
                equipHelmetSlot.isEquip = false;
                equipHelmetSlot = null;
                equipHelmet = null;
                break;
            case EquipType.SubEquipment:
                equipSubEquipmentSlot.isEquip = false;
                equipSubEquipmentSlot = null;
                equipSubEquipment = null;
                break;
        }

        foreach (ItemDataEquip equip in item.Data.equips)
        {
            stat.UnequipItemStat(equip.statusType, equip.value);
        }
    }
}
