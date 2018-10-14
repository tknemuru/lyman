﻿// using System.Collections.Generic;
// using System.Linq;
using System;
namespace Lyman.Models.Requests
{
    /// <summary>
    /// 捨牌要求
    /// </summary>
    public class DiscardRequest : FieldAttachedRequest
    {
        /// <summary>
        /// 捨牌対象の風
        /// </summary>
        /// <value>The wind.</value>
        public Wind.Index Wind { get; set; }

        /// <summary>
        /// 捨てる牌
        /// </summary>
        /// <value>The tile.</value>
        public uint Tile { get; set; }

        /// <summary>
        /// 要求種別の初期化を行います。
        /// </summary>
        protected override void InitRequestType()
        {
            this.RequestType = RequestType.Discard;
        }
    }
}
