using LmpCommon.Message.Data.Editor;
using LmpCommon.Message.Interface;
using LmpCommon.Message.Server;
using Server.Client;
using Server.Message.Base;
using Server.Server;

namespace Server.Message
{
    public class EditorMsgReader : ReaderBase
    {
        public override void HandleMessage(ClientStructure client, IClientMessageBase message)
        {
            var data = (EditorMsgData)message.Data;
            MessageQueuer.RelayMessage<EditorSrvMsg>(client, data);
        }
    }
}