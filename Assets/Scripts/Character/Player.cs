using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public StatHandler stat;
    public Equipment equipment;
    public Inventory inventory;

    private void Awake()
    {
        PlayerManager.Instance.Player = this;
        stat = GetComponent<StatHandler>();
        equipment = GetComponent<Equipment>();
        inventory = GetComponent<Inventory>();
    }
}
