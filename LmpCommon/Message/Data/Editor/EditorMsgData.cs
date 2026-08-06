using Lidgren.Network;
using LmpCommon.Message.Base;
using LmpCommon.Message.Types;
using System;

namespace LmpCommon.Message.Data.Editor
{
    public class EditorMsgData : EditorBaseMsgData
    {
        /// <inheritdoc />
        internal EditorMsgData() { }
        public override EditorMessageType EditorMessageType => EditorMessageType.Sync;

        public UInt32 CraftID;

        //Avoid using reference types in this message as it can generate allocations and is sent VERY often.
        public string ShipName;
        public UInt32[] Parts = new UInt32[100]; // use the part limit set in settings

        public override string ClassName { get; } = nameof(EditorMsgData);

        internal override void InternalSerialize(NetOutgoingMessage lidgrenMsg)
        {
            base.InternalSerialize(lidgrenMsg);
            lidgrenMsg.Write(CraftID);
            lidgrenMsg.Write(ShipName);

            for (var i = 0; i < 100; i++)
                lidgrenMsg.Write(Parts[i]);
        }

        internal override void InternalDeserialize(NetIncomingMessage lidgrenMsg)
        {
            base.InternalDeserialize(lidgrenMsg);
            CraftID = lidgrenMsg.ReadUInt32();
            ShipName = lidgrenMsg.ReadString();

            for (var i = 0; i < 100; i++)
                Parts[i] = lidgrenMsg.ReadUInt32();
        }

        internal override int InternalGetMessageSize()
        {
            return base.InternalGetMessageSize() + sizeof(UInt32) + ShipName.GetByteCount() + sizeof(UInt32) * 100; // same here, 100 is a magic number
        }
    }

    /// <inheritdoc />
}
