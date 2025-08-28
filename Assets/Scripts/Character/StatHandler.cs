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
    /// 경험치를 획득했을 때, 레벨업이 가능하면 레벨업 적용
    /// 가능하다면 추가 레벨 업 가능
    /// </summary>
    public void CheckLevelUp()
    {
        // 현재 최대 레벨은 10
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
    /// 장착한 아이템의 능력치를 적용함
    /// </summary>
    /// <param name="type">적용되는 능력치 타입</param>
    /// <param name="amount">적용되는 능력치 값</param>
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
    /// 적용되어있는 아이템 능력치를 제거하고 장착해제함
    /// </summary>
    /// <param name="type">제거하는 능력치 타입</param>
    /// <param name="amount">제거하는 능력치 값</param>
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
