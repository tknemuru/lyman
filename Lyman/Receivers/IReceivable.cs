﻿// using System.Collections.Generic;
// using System.Linq;
using System;
namespace Lyman.Receivers
{
    /// <summary>
    /// 受信機能を提供します。
    /// </summary>
    public interface IReceivable<in TIn, out TOut>
    {
        /// <summary>
        /// 要求を受信し受信結果を返却します。
        /// </summary>
        /// <param name="request">要求</param>
        /// <returns>受信結果</returns>
        TOut Receive(TIn request);
    }
}
