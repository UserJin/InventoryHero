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

            // Ž���� ������ ���
            ItemSlot slot = new ItemSlot(item, 1, false);
            inventorySlots.Add(slot);
        }
        else
        {
            ItemSlot slot = new ItemSlot(item, 1, false);
            inventorySlots.Add(slot);
        }
        // �κ��丮 UI ����
        OnChangeInventory.Invoke();
    }

    public void RemoveItem(ItemSlot slot)
    {
        inventorySlots.Remove(slot);
        // �κ��丮 UI ����
        OnChangeInventory.Invoke();
    }

    public void EquipItem(ItemSlot slot)
    {
        if(!slot.isEquip)
        {
            Debug.Log($"{slot.Data.name}: EqupItem");
            player.equipment.Equip(slot);
            slot.isEquip = true;
            // �ɷ�ġ UI ����(���� �ɷ�ġ�� ���� �� �� �ִٸ�)
        }
        else
        {
            Debug.LogError("���� ����: �̹� ������ �������Դϴ�.");
        }
        OnChangeInventory.Invoke();
    }

    public void UnequipItem(ItemSlot slot)
    {
        if(slot.isEquip)
        {
            player.equipment.UnEquip(slot);
            slot.isEquip = false;
            // �ɷ�ġ UI ����(���� �ɷ�ġ�� ���� �� �� �ִٸ�)
        }
        else
        {
            Debug.LogError("���� ����: �����ϰ� ���� ���� �������Դϴ�.");
        }
        OnChangeInventory.Invoke();
    }
}
