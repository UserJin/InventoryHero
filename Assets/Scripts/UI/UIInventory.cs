using System.Collections;
using System.Collections.Generic;
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

    public void Init()
    {

    }

    public void UpdateUI()
    {
        int count = 0;
        foreach (ItemSlot itemSlot in playerInventory.InventorySlots)
        {
            if(count < uiItemSlots.Count)
            {
                uiItemSlots[count].ClearSlot();
                uiItemSlots[count].Init(itemSlot);
            }
            else
            {
                AddItemSlot(itemSlot);
            }
            count++;
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
