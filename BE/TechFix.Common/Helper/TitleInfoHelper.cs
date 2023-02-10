using System;
using TechFix.Common.Enums;

namespace TechFix.Common.Helper
{
    public static class TitleInfoHelper
    {
        public static string GetDisplayName(this UserTitle userTitle)
        {
            var attr = userTitle.GetAttribute<TitleInfoAttribute>();
            return attr.DisplayName;
        }

        public static int GetMinTotalAp(this UserTitle userTitle)
        {
            var attr = userTitle.GetAttribute<TitleInfoAttribute>();
            return attr.MinTotalAp;
        }

        public static int GetMinPersonalSale(this UserTitle userTitle)
        {
            var attr = userTitle.GetAttribute<TitleInfoAttribute>();
            var result =attr.MinPersonalSale;

            return result;
        }

        public static int GetPoolPercent(this UserTitle userTitle)
        {
            var attr = userTitle.GetAttribute<TitleInfoAttribute>();
            return attr.PoolPercent;
        }

        public static int GetMinDirectApMonthly(this UserTitle userTitle)
        {
            var attr = userTitle.GetAttribute<TitleInfoAttribute>();
            return attr.MinDirectApMonthly;
        }

        public static int GetMaxLayer(this UserTitle userTitle)
        {
            var attr = userTitle.GetAttribute<TitleInfoAttribute>();
            return attr.MaxLayer;
        }

        public static decimal GetBonusNewRanking(this UserTitle userTitle)
        {
            var attr = userTitle.GetAttribute<TitleInfoAttribute>();
            return attr.BonusNewRanking;
        }

    }

    public class TitleInfoAttribute : Attribute
    {
        internal TitleInfoAttribute(string displayName, int minTotalAp, int minPersonalSale, int poolPercent, int maxLayer, int minDirectApMonthly, int bonusNewRanking)
        {
            DisplayName = displayName;
            MinTotalAp = minTotalAp;
            BonusNewRanking = bonusNewRanking;
            PoolPercent = poolPercent;
            MinPersonalSale = minPersonalSale;
            MaxLayer = maxLayer;
            MinDirectApMonthly = minDirectApMonthly;
        }

        public int PoolPercent { get; }
        public string DisplayName { get; }
        public int MinTotalAp { get; }
        public int MinPersonalSale { get; }
        public int MaxLayer { get; }
        public int MinDirectApMonthly { get; }
        public decimal BonusNewRanking { get; }
    }
}
