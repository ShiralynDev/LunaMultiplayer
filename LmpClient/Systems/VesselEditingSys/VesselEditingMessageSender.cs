using LmpClient.Base;
using LmpClient.Base.Interface;
using LmpClient.Extensions;
using LmpClient.Network;
using LmpClient.Systems.TimeSync;
using LmpClient.Systems.Warp;
using LmpClient.Utilities;
using LmpCommon.Message.Client;
using LmpCommon.Message.Data.Editor;
using LmpCommon.Message.Interface;
using System;
using UnityEngine;
using System.Collections.Generic;

namespace LmpClient.Systems.VesselEditorSys
{
    public class VesselEditingMessageSender : SubSystem<VesselEditorSystem>, IMessageSender
    {
        public void SendMessage(IMessageData msg)
        {
            NetworkSender.QueueOutgoingMessage(MessageFactory.CreateNew<EditorCliMsg>(msg));
        }

        /// <summary>
        /// Sends a vessel editor update
        /// </summary>
        /// <param name="rootPart">root part of the vessel to send</param>
        public void SendVesselEditorUpdate(List<Part> parts)
        {
            if (parts == null) return;

            var msg = CreateMessageFromPart(parts);
            if (msg == null) return;

            SendMessage(msg);
        }

        public static EditorMsgData CreateMessageFromPart(List<Part> parts)
        {
            if (parts == null || parts.Count == 0) return null;

            var msgData = MessageFactory.CreateNewMessageData<EditorMsgData>();
            msgData.GameTime = TimeSyncSystem.UniversalTime;
            try
            {
                msgData.CraftID = parts[0].craftID;
                msgData.ShipName = EditorLogic.fetch.ship.shipName;

                SetParts(parts, msgData);

                return msgData;
            }
            catch (Exception e)
            {
                LunaLog.Log($"[LMP]: Failed to get vessel editing update, exception: {e}");
            }

            return null;
        }

        #region Set message values

        private static void SetParts(List<Part> parts, EditorMsgData msgData) // need to add more part data than ID
        {
            var count = Math.Min(parts.Count, msgData.Parts.Length);
            for (var i = 0; i < count; i++)
                msgData.Parts[i] = parts[i].persistentId;
        }

        #endregion
    }
}
