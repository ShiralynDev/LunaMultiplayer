using LmpClient.Base;
using LmpCommon.Message.Data.Editor;
using System;

namespace LmpClient.Systems.VesselEditorSys
{
    public class EditorUpdateQueue : CachedConcurrentQueue<VesselEditingUpdate, EditorMsgData>
    {
        protected override void AssignFromMessage(VesselEditingUpdate value, EditorMsgData msgData)
        {
            value.CraftID = msgData.CraftID;
            value.ShipName = msgData.ShipName;

            Array.Copy(msgData.Parts, value.Parts, 100);
        }
    }
}
