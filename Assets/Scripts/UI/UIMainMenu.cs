using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UIBase
{
    public Button statusBtn; 
    public Button inventoryBtn;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI expText;
    public Image expFill;
    public TextMeshProUGUI goldText;

    StatHandler playerStat;

    // ���� �����̴� ���ؾ��ұ�?
    private void Awake()
    {
        type = UIType.MainMenu;
    }

    private void Start()
    {
        statusBtn.onClick.AddListener(ShowStatusUI);
        inventoryBtn.onClick.AddListener(ShowInventoryUI);

        playerStat = PlayerManager.Instance.Player.stat;
        playerStat.OnStatChange += UpdateUI;

        UpdateUI();
    }

    private void ShowButton()
    {
        statusBtn?.gameObject.SetActive(true);
        inventoryBtn?.gameObject.SetActive(true);
    }

    private void HideButton()
    {
        statusBtn?.gameObject.SetActive(false);
        inventoryBtn?.gameObject.SetActive(false);
    }

    private void ShowStatusUI()
    {
        UIManager.Instance.SetUI(UIType.Status);
    }

    private void ShowInventoryUI()
    {
        UIManager.Instance.SetUI(UIType.Inventory);
    }

    public override void Show()
    {
        ShowButton();
    }

    public override void Hide()
    {
        HideButton();
    }


    public void UpdateUI()
    {
        if (playerStat == null)
        {
            Debug.LogError($"{this.name}: ��ϵ� �÷��̾� ���� ������ �����ϴ�.");
            return;
        }

        levelText.text = $"Lv. {playerStat.CurLevel}";
        expText.text = $"{playerStat.CurExp} / {Game.Common.LevelTable.ExpToLevelUp[playerStat.CurLevel + 1]}";
        expFill.fillAmount = (float)playerStat.CurExp / Game.Common.LevelTable.ExpToLevelUp[playerStat.CurLevel + 1];
        goldText.text = playerStat.CurGold.ToString();
    }
}
