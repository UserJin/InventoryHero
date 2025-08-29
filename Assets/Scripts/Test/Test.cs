using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public ItemData[] datas;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerManager.Instance.Player.stat.GetExp(15);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerManager.Instance.Player.stat.GetGold(100);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerManager.Instance.Player.inventory.AddItem(datas[Random.Range(0, datas.Length)]);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayerManager.Instance.Player.inventory.TestRemoveItem();
        }
    }
}
