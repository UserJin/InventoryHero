using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : UIBase
{
    public Button backBtn;

    public UIItemSlot selectedItem;

    public List<UIItemSlot> uiItemSlots;

    public Transform slotsParent;
    public GameObject uiItemSlotPrefab;

    Inventory playerInventory;

    private void Awake()
    {
        type = UIType.Inventory;
        uiItemSlots = new List<UIItemSlot>();

        playerInventory = PlayerManager.Instance.Player.inventory;
        backBtn.onClick.AddListener(BackUI);
    }

    public void UpdateUI()
    {
        if(playerInventory.InventorySlots.Count > uiItemSlots.Count) // �κ��丮 ���� ���� �� ������ �׸�ŭ UI ���� ���� �� �߰�
        {
            for(int count = 0;count < playerInventory.InventorySlots.Count;count++)
            {
                if (count < uiItemSlots.Count)
                {
                    uiItemSlots[count].ClearSlot();
                    uiItemSlots[count].Init(playerInventory.InventorySlots[count]);
                }
                else
                {
                    AddItemSlot(playerInventory.InventorySlots[count]);
                }
            }
        }
        else if(playerInventory.InventorySlots.Count < uiItemSlots.Count) // �κ��丮 ���� ���� �� ������ ���� UI ���� ����
        {
            for (int count = 0; count < uiItemSlots.Count; count++)
            {
                if (count < playerInventory.InventorySlots.Count)
                {
                    uiItemSlots[count].ClearSlot();
                    uiItemSlots[count].Init(playerInventory.InventorySlots[count]);
                }
                else
                {
                    Destroy(uiItemSlots[count].gameObject);
                    uiItemSlots.RemoveAt(count);
                }
            }
        }
        else // �κ��丮 ���� ���� UI ���� ���� ������
        {
            for (int i = 0; i < playerInventory.InventorySlots.Count; i++)
            {
                uiItemSlots[i].ClearSlot();
                uiItemSlots[i].Init(playerInventory.InventorySlots[i]);
            }
        }
    }

    private void Start()
    {
        UpdateUI();
    }

    private void BackUI()
    {
        UIManager.Instance.SetUI(UIType.MainMenu);
    }

    private void AddItemSlot(ItemSlot slot)
    {
        if(slotsParent == null)
        {
            Debug.LogError($"{this.name}: ������ ������ ��ġ�� �������� �ʽ��ϴ�.");
            return;
        }

        // ���� ������Ʈ Ǯ������ ���� ����
        UIItemSlot newSlot = Instantiate(uiItemSlotPrefab, slotsParent).GetComponent<UIItemSlot>();
        uiItemSlots.Add(newSlot);
        newSlot.Init(slot);
    }

    protected override void OnShow()
    {
        base.OnShow();
        playerInventory.OnChangeInventory += UpdateUI;
        UpdateUI();
    }

    protected override void OnHide()
    {
        base.OnHide();
        playerInventory.OnChangeInventory -= UpdateUI;
    }
}
