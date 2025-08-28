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
        if(playerInventory.InventorySlots.Count > uiItemSlots.Count) // 인벤토리 슬롯 수가 더 많으면 그만큼 UI 슬롯 생성 및 추가
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
        else if(playerInventory.InventorySlots.Count < uiItemSlots.Count) // 인벤토리 슬롯 수가 더 적으면 남은 UI 슬롯 제거
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
        else // 인벤토리 슬롯 수와 UI 슬롯 수가 같으면
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
