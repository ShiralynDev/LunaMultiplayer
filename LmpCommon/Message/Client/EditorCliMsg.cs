using Lidgren.Network;
using LmpCommon.Enums;
using LmpCommon.Message.Client.Base;
using LmpCommon.Message.Data.Editor;
using LmpCommon.Message.Types;
using LmpCommon.Message.Base;
using System;
using System.Collections.Generic;

namespace LmpCommon.Message.Client
{
    public class EditorCliMsg : CliMsgBase<EditorBaseMsgData>
    {
        /// <inheritdoc />
        internal EditorCliMsg() { }

        /// <inheritdoc />
        public override string ClassName { get; } = nameof(EditorCliMsg);

        /// <inheritdoc />
        /// 
        protected override Dictionary<ushort, Type> SubTypeDictionary { get; } = new Dictionary<ushort, Type>
        {
            [(ushort)EditorMessageType.Sync] = typeof(EditorMsgData),
        };

        public override ClientMessageType MessageType => ClientMessageType.Editor;
        protected override int DefaultChannel => IsUnreliableMessage() ? 0 : 8;
        public override NetDeliveryMethod NetDeliveryMethod => IsUnreliableMessage() ?
            NetDeliveryMethod.UnreliableSequenced : NetDeliveryMethod.ReliableOrdered;

        private bool IsUnreliableMessage()
        {
            return Data.SubType != (ushort)EditorMessageType.Sync; // idk if it should be like this, just want this to be classed as something that should not be dropped, I think that's the correct way to do this atleast
        }
    }
}