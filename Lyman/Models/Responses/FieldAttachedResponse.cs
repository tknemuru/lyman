﻿// using System.Collections.Generic;
// using System.Linq;
using System;
using Lyman.Managers;

namespace Lyman.Models.Responses
{
    /// <summary>
    /// フィールド状態が付随した応答
    /// </summary>
    public class FieldAttachedResponse : Response
    {
        /// <summary>
        /// フィールド状態
        /// </summary>
        public FieldContext Context { get; set; }

        /// <summary>
        /// フィールド状態を引き離します。
        /// </summary>
        public void Detach(Guid roomKey)
        {
            var room = RoomManager.Get(roomKey);
            room.Context = this.Context;
            this.Context = null;
        }
    }
}
