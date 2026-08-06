using Lidgren.Network;
using LmpCommon.Enums;
using LmpCommon.Message.Data.Editor;
using LmpCommon.Message.Server.Base;
using LmpCommon.Message.Types;
using System;
using System.Collections.Generic;

namespace LmpCommon.Message.Server
{
    public class EditorSrvMsg : SrvMsgBase<EditorBaseMsgData>
    {
        /// <inheritdoc />
        internal EditorSrvMsg() { }

        /// <inheritdoc />
        public override string ClassName { get; } = nameof(EditorSrvMsg);

        /// <inheritdoc />
        protected override Dictionary<ushort, Type> SubTypeDictionary { get; } = new Dictionary<ushort, Type>
        {
            [(ushort)EditorMessageType.Sync] = typeof(EditorMsgData),
        };

        public override ServerMessageType MessageType => ServerMessageType.Editor;
        protected override int DefaultChannel => IsUnreliableMessage() ? 0 : 8;
        public override NetDeliveryMethod NetDeliveryMethod => IsUnreliableMessage() ?
            NetDeliveryMethod.UnreliableSequenced : NetDeliveryMethod.ReliableOrdered;

        private bool IsUnreliableMessage()
        {
            return false;
        }
    }
}