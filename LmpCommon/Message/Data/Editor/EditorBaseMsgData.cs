using Lidgren.Network;
using LmpCommon.Message.Base;
using LmpCommon.Message.Types;
using System;

namespace LmpCommon.Message.Data.Editor
{
    public abstract class EditorBaseMsgData : MessageData
    {
        /// <inheritdoc />
        internal EditorBaseMsgData() { }
        public override ushort SubType => (ushort)(int)EditorMessageType;
        public virtual EditorMessageType EditorMessageType => throw new NotImplementedException();

        //Avoid using reference types in this message as it can generate allocations and is sent VERY often (specially positions and flight states)
        public double GameTime;

        internal override void InternalSerialize(NetOutgoingMessage lidgrenMsg)
        {
            lidgrenMsg.Write(GameTime);
        }

        internal override void InternalDeserialize(NetIncomingMessage lidgrenMsg)
        {
            GameTime = lidgrenMsg.ReadDouble();
        }

        internal override int InternalGetMessageSize()
        {
            return sizeof(double);
        }
    }
}
