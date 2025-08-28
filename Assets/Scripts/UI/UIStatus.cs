using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatus : UIBase
{
    public Button backBtn;

    StatHandler playerStat;

    public TextMeshProUGUI attackValue;
    public TextMeshProUGUI defenceValue;
    public TextMeshProUGUI healthValue;
    public TextMeshProUGUI attackSpeedValue;

    private void Awake()
    {
        type = UIType.Status;
    }

    private void Start()
    {
        backBtn.onClick.AddListener(BackUI);

        playerStat = PlayerManager.Instance.Player.stat;
        PlayerManager.Instance.Player.stat.OnStatChange += UpdateUI;
        UpdateUI();
    }

    private void BackUI()
    {
        UIManager.Instance.SetUI(UIType.MainMenu);
    }

    public void UpdateUI()
    {
        if(playerStat == null)
        {
            Debug.LogError($"{this.name}: ��ϵ� �÷��̾� ���� ������ �����ϴ�.");
            return;
        }

        attackValue.text = playerStat.TotalAttack.ToString();
        defenceValue.text = playerStat.TotalDefense.ToString();
        healthValue.text = playerStat.TotalHealth.ToString();
        attackSpeedValue.text = playerStat.TotalAttackSpeed.ToString();
    }

    protected override void OnShow()
    {
        base.OnShow();

        if(playerStat != null)
            UpdateUI();
    }

    protected override void OnHide()
    {
        base.OnHide();

        PlayerManager.Instance.Player.stat.OnLevelUp -= UpdateUI;
    }
}
