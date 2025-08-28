using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public ItemData data1;
    public ItemData data2;
    public ItemData data3;
    public ItemData data4;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerManager.Instance.Player.inventory.AddItem(data1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerManager.Instance.Player.inventory.AddItem(data2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerManager.Instance.Player.inventory.AddItem(data3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayerManager.Instance.Player.inventory.AddItem(data4);
        }
        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            PlayerManager.Instance.Player.stat.GetExp(15);
        }
    }
}
