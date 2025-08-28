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
            Debug.LogError($"{this.name}: 슬롯을 생성할 위치가 존재하지 않습니다.");
            return;
        }

        // 추후 오브젝트 풀링으로 개선 가능
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
