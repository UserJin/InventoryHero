using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBase : MonoBehaviour
{
    // ���� UI�� Ȱ��ȭ ����
    protected bool isActive = false;

    protected UIType type;
    public UIType Type { get { return type; } }

    public virtual void Show()
    {
        gameObject.SetActive(true);
        isActive = true;
        OnShow();
    }

    public virtual void Hide()
    {
        OnHide();
        isActive = false;
        gameObject.SetActive(false);
    }

    protected virtual void OnShow() { }
    protected virtual void OnHide() { }
}
