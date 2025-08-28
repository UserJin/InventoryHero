using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemSlot : MonoBehaviour
{
    public ItemSlot slot;
    public Image icon;
    public TextMeshProUGUI equipText;
    Button button;

    public void Init(ItemSlot itemSlot)
    {
        slot = itemSlot;
        button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(ToggleEquip);

        UpdateSlot();
    }

    public void UpdateSlot()
    {
        if (slot == null)
        {
            ClearSlot();
            Destroy(gameObject);
            return;
        }

        icon.sprite = slot.Data.icon;
        equipText.text = slot.isEquip ? "E" : "";
    }

    public void ClearSlot()
    {
        slot = null;
        icon.sprite = null;
        equipText.text = "";
    }

    private void ToggleEquip()
    {
        Debug.Log($"{slot.Data.name}: OnClick");
        if(!slot.isEquip)
        {
            PlayerManager.Instance.Player.inventory.EquipItem(slot);
        }
        else
        {
            PlayerManager.Instance.Player.inventory.UnequipItem(slot);
        }
    }
}
