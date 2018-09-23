﻿using System.Collections.Generic;
using System.Linq;
using System;
using Lyman.Helpers;

namespace Lyman.Models
{
    /// <summary>
    /// 山/壁牌(ピーパイ)
    /// </summary>
    public static class Wall
    {
        /// <summary>
        /// 長さ
        /// </summary>
        public const int Length = 17;

        /// <summary>
        /// キーの長さ
        /// </summary>
        public static readonly int KeyLength = IEnumerableHelper.GetEnums<Key>().Count();

        /// <summary>
        /// 段の長さ
        /// </summary>
        public static readonly int RankLength = IEnumerableHelper.GetEnums<Rank>().Count();

        /// <summary>
        /// The name of the rank japanese.
        /// </summary>
        public static readonly TwoWayDictionary<Rank, string> RankJapaneseName = new TwoWayDictionary<Rank, string>
        (
            new[]
            {
                Rank.Lower,
                Rank.Upper
            },
            new[]
            {
                "下",
                "上"
            }
        );

        /// <summary>
        /// 段
        /// </summary>
        public enum Rank
        {
            /// <summary>
            /// 下段
            /// </summary>
            Lower,

            /// <summary>
            /// 上段
            /// </summary>
            Upper,
        }

        /// <summary>
        /// キー
        /// </summary>
        public enum Key
        {
            /// <summary>
            /// キー名
            /// </summary>
            Name,

            /// <summary>ank
            /// 風
            /// </summary>
            Wind,

            /// <summary>
            /// 段
            /// </summary>
            Rank,
        }

        /// <summary>
        /// 数値に変換します。
        /// </summary>
        /// <returns>数値化した段</returns>
        /// <param name="rank">段</param>
        public static int ToInt(this Rank rank)
        {
            return (int)rank;
        }

        /// <summary>
        /// 数値に変換します。
        /// </summary>
        /// <returns>数値化したキー</returns>
        /// <param name="key">キー</param>
        public static int ToInt(this Key key)
        {
            return (int)key;
        }

        /// <summary>
        /// 各風・段・牌の壁に対して戻り値を持たないメソッドを繰り返し実行します。
        /// </summary>
        /// <param name="action">戻り値を持たないメソッド</param>
        public static void ForEach(Action<Wind.Index, Rank, int> action)
        {
            for (var wind = Wind.Index.East; wind.ToInt() < Wind.Length; wind++)
            {
                for (var rank = Rank.Lower; (int)rank < RankLength; rank++)
                {
                    for (var i = 0; i < Length; i++)
                    {
                        action(wind, rank, i);
                    }
                }
            }
        }

        /// <summary>
        /// 各段に対して戻り値を持たないメソッドを繰り返し実行します。
        /// </summary>
        /// <param name="action">Action.</param>
        public static void ForEachRank(Action<Rank> action)
        {
            for (var rank = Rank.Lower; (int)rank < RankLength; rank++)
            {
                action(rank);
            }
        }
    }
}
