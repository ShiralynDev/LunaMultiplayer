using KSP.UI.Screens.Flight;
using LmpClient.Extensions;
using LmpClient.Systems.VesselPositionSys;
using System;
using Object = UnityEngine.Object;

namespace LmpClient.Systems.VesselEditorSys
{
    public class VesselEditingLoader
    {
        /// <summary>
        /// Loads/Reloads a craft into game
        /// </summary>
        public static bool LoadCraft(string shipName, bool forceReload)
        {
            try
            {
                EditorLogic.fetch.ship.shipName = shipName;
                return true;
            }
            catch (Exception e)
            {
                LunaLog.LogError($"[LMP]: Error loading vessel: {e}");
                return false;
            }
        }
    }
}
