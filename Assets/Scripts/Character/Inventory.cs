using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemSlot
{
    [SerializeField] ItemData data;
    public int curCount;
    public bool isEquip;

    public ItemData Data {  get { return data; } }

    public ItemSlot(ItemData data, int curCount, bool isEquip)
    {
        this.data = data;
        this.curCount = curCount;
        this.isEquip = isEquip;
    }
}

public class Inventory : MonoBehaviour
{
    [SerializeField] List<ItemSlot> inventorySlots;
    public List<ItemSlot> InventorySlots { get { return inventorySlots; } }

    Player player;

    public Action OnChangeInventory;

    private void Awake()
    {
        //inventorySlots = new List<ItemSlot>();
        player = GetComponent<Player>();
    }

    public void AddItem(ItemData item)
    {
        if(item.canStack)
        {
            foreach(ItemSlot tempSlot in inventorySlots)
            {
                if(tempSlot.Data == item && tempSlot.curCount < tempSlot.Data.maxStackCount)
                {
                    tempSlot.curCount++;
                    return;
                }
            }

            // 탐색에 실패할 경우
            ItemSlot slot = new ItemSlot(item, 1, false);
            inventorySlots.Add(slot);
        }
        else
        {
            ItemSlot slot = new ItemSlot(item, 1, false);
            inventorySlots.Add(slot);
        }
        // 인벤토리 UI 갱신
        OnChangeInventory.Invoke();
    }

    public void RemoveItem(ItemSlot slot)
    {
        inventorySlots.Remove(slot);
        // 인벤토리 UI 갱신
        OnChangeInventory.Invoke();
    }

    public void EquipItem(ItemSlot slot)
    {
        if(!slot.isEquip)
        {
            Debug.Log($"{slot.Data.name}: EqupItem");
            player.equipment.Equip(slot);
            slot.isEquip = true;
            // 능력치 UI 갱신(만약 능력치를 같이 볼 수 있다면)
        }
        else
        {
            Debug.LogError("장착 실패: 이미 장착된 아이템입니다.");
        }
        OnChangeInventory.Invoke();
    }

    public void UnequipItem(ItemSlot slot)
    {
        if(slot.isEquip)
        {
            player.equipment.UnEquip(slot);
            slot.isEquip = false;
            // 능력치 UI 갱신(만약 능력치를 같이 볼 수 있다면)
        }
        else
        {
            Debug.LogError("장착 실패: 장착하고 있지 않은 아이템입니다.");
        }
        OnChangeInventory.Invoke();
    }
}
