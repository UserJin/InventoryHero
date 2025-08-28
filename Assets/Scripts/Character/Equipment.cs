using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField] ItemSlot equipWeapon;
    [SerializeField] ItemSlot equipArmor;
    [SerializeField] ItemSlot equipHelmet;
    [SerializeField] ItemSlot equipSubEquipment;

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
                if(equipWeapon.Data != null)
                    UnEquip(equipWeapon);
                equipWeapon = item;
                break;
            case EquipType.Armor:
                if(equipArmor.Data != null)
                    UnEquip(equipArmor);
                equipArmor = item;
                break;
            case EquipType.Helmet:
                if(equipHelmet.Data != null)
                    UnEquip(equipHelmet);
                equipHelmet = item;
                break;
            case EquipType.SubEquipment:
                if(equipSubEquipment.Data != null)
                    UnEquip(equipSubEquipment);
                equipSubEquipment = item;
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
                equipWeapon.isEquip = false;
                equipWeapon = null;
                break;
            case EquipType.Armor:
                equipArmor.isEquip = false;
                equipArmor = null;
                break;
            case EquipType.Helmet:
                equipHelmet.isEquip = false;
                equipHelmet = null;
                break;
            case EquipType.SubEquipment:
                equipSubEquipment.isEquip = false;
                equipSubEquipment = null;
                break;
        }

        foreach (ItemDataEquip equip in item.Data.equips)
        {
            stat.UnequipItemStat(equip.statusType, equip.value);
        }
    }
}
