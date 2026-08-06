using LmpClient.Base;
using LmpClient.Base.Interface;
using LmpClient.VesselUtilities;
using LmpCommon.Message.Data.Editor;
using LmpCommon.Message.Interface;
using System.Collections.Concurrent;

namespace LmpClient.Systems.VesselEditorSys
{
    public class VesselEditingMessageHandler : SubSystem<VesselEditorSystem>, IMessageHandler
    {
        public ConcurrentQueue<IServerMessageBase> IncomingMessages { get; set; } = new ConcurrentQueue<IServerMessageBase>();

        public void HandleMessage(IServerMessageBase msg)
        {
            if (!(msg.Data is EditorMsgData msgData)) return;
            if (HighLogic.LoadedScene != GameScenes.EDITOR) return;

            var queue = VesselEditorSystem.TargetEditorUpdateQueue
                .GetOrAdd(msgData.CraftID, _ => new EditorUpdateQueue());

            queue.Enqueue(msgData);
        }
    }
}
