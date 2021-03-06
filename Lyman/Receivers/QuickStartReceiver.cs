﻿using System.Collections.Generic;
using System.Linq;
using System;
using Lyman.Di;
using Lyman.Models;
using Lyman.Models.Requests;
using Lyman.Models.Responses;
using Lyman.Managers;

namespace Lyman.Receivers
{
    /// <summary>
    /// 即時ゲーム開始要求の受信機能を提供します。
    /// </summary>
    public class QuickStartReceiver : IReceivable<QuickStartRequest, QuickStartResponse>
    {
        /// <summary>
        /// 即時ゲーム開始要求の受信処理を実行します。
        /// </summary>
        /// <returns>即時ゲーム開始応答</returns>
        /// <param name="request">即時ゲーム開始要求</param>
        public QuickStartResponse Receive(QuickStartRequest request)
        {
            // 部屋作成
            var response = DiProvider.GetContainer().GetInstance<QuickStartResponse>();
            var createRoomResponse = DiProvider.GetContainer().GetInstance<CreateRoomReceiver>().
                Receive(request.CreateRoomRequest);
            var roomKey = createRoomResponse.RoomKey;
            response.RoomKey = roomKey;
            response.RoomName = request.CreateRoomRequest.RoomName;

            // 入室
            var enterRoomResponses = request.EnterRoomRequests.Select(req =>
            {
                req.RoomKey = roomKey;
                return DiProvider.GetContainer().GetInstance<EnterRoomReceiver>().Receive(req);
            });
            var first = enterRoomResponses.ToList().First();
            var room = RoomManager.Get(roomKey);
            response.Players = room.Players.ToDictionary(p => p.Key.ToInt(), p => {
                var modPlayer = p.Value.DeepCopy();
                modPlayer.Key = Guid.Empty;
                modPlayer.ConnectionId = string.Empty;
                return modPlayer;
            });
            response.PlayerKey = first.PlayerKey;
            response.PlayerName = room.GetPlayer(first.PlayerKey).Name;
            response.Wind = first.Wind;
            response.WindIndex = first.WindIndex;
            var playerKey = first.PlayerKey;
            response.FirstPlayer = true;

            // 配牌
            request.DealtTilesRequest.RoomKey = roomKey;
            request.DealtTilesRequest.PlayerKey = playerKey;
            request.DealtTilesRequest.Attach();
            var dealtTilesResponse = DiProvider.GetContainer().GetInstance<DealtTilesReceiver>().
                Receive(request.DealtTilesRequest);
            room.NextPosition = dealtTilesResponse.NextDrawPosition;
            dealtTilesResponse.Detach(roomKey);
            request.SelectRoomRequest.RoomKey = roomKey;
            request.SelectRoomRequest.PlayerKey = playerKey;
            var selectRoomResponse = DiProvider.GetContainer().GetInstance<SelectRoomReceiver>().
                Receive(request.SelectRoomRequest);
            response.Hand = selectRoomResponse.Hand;
            response.Rivers = selectRoomResponse.Rivers;
            response.State = room.State.ToInt();
            response.Turn = selectRoomResponse.Turn;
            return response;
        }
    }
}
