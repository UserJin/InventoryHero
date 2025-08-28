using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Common
{
    public static class Constants
    {

    }

    public static class LevelTable
    {
        public static readonly int[] ExpToLevelUp = { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 99999999 };

        public static readonly int[] AttackValueTable = { 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60 };

        public static readonly int[] DefenceValueTable = { 5, 8, 10, 12, 15, 18, 20, 22, 25, 28, 30};

        public static readonly int[] HealthValueTable = { 100, 110, 120, 130, 140, 150, 160, 170, 180, 190, 200 };

        public static readonly int[] AttackSpeedValueTable = { 0, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 };
    }
}

