﻿// using System.Collections.Generic;
// using System.Linq;
using System;
using System.Diagnostics;
using Lyman.Converters;
using Lyman.Providers;
using Lyman.Receivers;

namespace Lyman.Di
{

    /// <summary>
    /// DIの生成機能を提供します。
    /// </summary>
    public static class DiProvider
    {
        /// <summary>
        /// DIコンテナ
        /// </summary>
        private static SimpleContainer MyContainer { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        static DiProvider()
        {
            MyContainer = new SimpleContainer();
            Register();
        }

        /// <summary>
        /// DIコンテナをセットします。
        /// </summary>
        /// <param name="container">DIコンテナ</param>
        public static void SetContainer(SimpleContainer container)
        {
            MyContainer = container;
        }

        /// <summary>
        /// DIコンテナを取得します。
        /// </summary>
        /// <returns></returns>
        public static SimpleContainer GetContainer()
        {
            if (MyContainer == null)
            {
                throw new InvalidOperationException("コンテナのインスタンスが生成されていません。");
            }
            return MyContainer;
        }

        /// <summary>
        /// 依存性の登録を行います。
        /// </summary>
        private static void Register()
        {
            MyContainer.Register<ContextToTextConverter>(Lifestyle.Singleton);
            MyContainer.Register<TextToContextConverter>(Lifestyle.Singleton);
            MyContainer.Register<DrawReceiver>(Lifestyle.Singleton);
            MyContainer.Register<DiscardReceiver>(Lifestyle.Singleton);
            MyContainer.Register<DealtTilesReceiver>(() => new RandomDealtTilesReceiver(), Lifestyle.Singleton);
            MyContainer.Register<YakuAnalyzerProvider>(Lifestyle.Singleton);
        }
    }
}
