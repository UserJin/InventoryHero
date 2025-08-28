using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    private int health;
    private int attack;
    private int defense;
    private int attackSpeed;

    private int equipHealth;
    private int equipAttack;
    private int equipDefence;
    private int equipAttackSpeed;

    public int TotalHealth { get { return health + equipHealth; } }
    public int TotalAttack { get { return attack + equipAttack; } }
    public int TotalDefense { get { return defense + equipDefence; } }
    public int TotalAttackSpeed { get { return attackSpeed + equipAttackSpeed; } }

    private int curLevel;
    public int CurLevel { get { return curLevel; } }
    private int curExp;
    public int CurExp { get { return curExp; } }

    private int curGold;
    public int CurGold { get { return curGold; } }

    public Action OnLevelUp;
    public Action OnStatChange;

    private void Awake()
    {
        curLevel = 1;
        curExp = 0;
        curGold = 1000;

        health = Game.Common.LevelTable.HealthValueTable[curLevel];
        attack = Game.Common.LevelTable.AttackValueTable[curLevel];
        defense = Game.Common.LevelTable.DefenceValueTable[curLevel];
        attackSpeed = Game.Common.LevelTable.AttackSpeedValueTable[curLevel];
    }

    /// <summary>
    /// ����ġ�� ȹ������ ��, �������� �����ϸ� ������ ����
    /// �����ϴٸ� �߰� ���� �� ����
    /// </summary>
    public void CheckLevelUp()
    {
        // ���� �ִ� ������ 10
        if (curLevel >= 10) return;

        while(curExp >= Game.Common.LevelTable.ExpToLevelUp[curLevel+1])
        {
            curLevel++;
            health = Game.Common.LevelTable.HealthValueTable[curLevel];
            attack = Game.Common.LevelTable.AttackValueTable[curLevel];
            defense = Game.Common.LevelTable.DefenceValueTable[curLevel];
            attackSpeed = Game.Common.LevelTable.AttackSpeedValueTable[curLevel];

            curExp -= Game.Common.LevelTable.ExpToLevelUp[curLevel];
        }

        OnStatChange?.Invoke();
    }

    public void GetExp(int amount)
    {
        curExp += amount;
        OnStatChange?.Invoke();
        CheckLevelUp();
    }

    /// <summary>
    /// ������ �������� �ɷ�ġ�� ������
    /// </summary>
    /// <param name="type">����Ǵ� �ɷ�ġ Ÿ��</param>
    /// <param name="amount">����Ǵ� �ɷ�ġ ��</param>
    public void EquipItemStat(StatusType type, int amount)
    {
        switch (type)
        {
            case StatusType.Health:
                equipHealth += amount;
                break;
            case StatusType.Attack:
                equipAttack += amount;
                break;
            case StatusType.Defence:
                equipDefence += amount;
                break;
            case StatusType.AttackSpeed:
                equipAttackSpeed += amount;
                break;
        }
    }

    /// <summary>
    /// ����Ǿ��ִ� ������ �ɷ�ġ�� �����ϰ� ����������
    /// </summary>
    /// <param name="type">�����ϴ� �ɷ�ġ Ÿ��</param>
    /// <param name="amount">�����ϴ� �ɷ�ġ ��</param>
    public void UnequipItemStat(StatusType type, int amount)
    {
        switch (type)
        {
            case StatusType.Health:
                equipHealth -= amount;
                break;
            case StatusType.Attack:
                equipAttack -= amount;
                break;
            case StatusType.Defence:
                equipDefence -= amount;
                break;
            case StatusType.AttackSpeed:
                equipAttackSpeed -= amount;
                break;
        }
    }
}
