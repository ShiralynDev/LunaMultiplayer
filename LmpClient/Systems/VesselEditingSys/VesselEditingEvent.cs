using LmpClient.Base;

namespace LmpClient.Systems.VesselEditorSys
{
    public class EditingEvents : SubSystem<VesselEditorSystem>
    {
        public void EditorEnter()
        {
            var parts = EditorLogic.fetch.ship.parts;

            System.MessageSender.SendVesselEditorUpdate(parts);
        }
    }
}
