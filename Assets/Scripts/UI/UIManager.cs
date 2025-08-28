using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIType
{
    Status,
    MainMenu,
    Inventory
}

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Dictionary<UIType, UIBase> uiDictionary = new Dictionary<UIType, UIBase>();

    private UIBase curUI;

    public void RegisterUI(UIBase ui, UIType uIType)
    {
        uiDictionary[uIType] = ui;
    }

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        UIBase[] uIBases = FindObjectsOfType<UIBase>();
        foreach (UIBase uiBase in uIBases)
        {
            RegisterUI(uiBase, uiBase.Type);
            uiBase.gameObject.SetActive(false);
        }

        curUI = uiDictionary[UIType.MainMenu];
        curUI.gameObject.SetActive(true);
    }

    public void SetUI(UIType uIType)
    {
        curUI?.Hide();
        curUI = uiDictionary[uIType];
        curUI?.Show();
    }
}
