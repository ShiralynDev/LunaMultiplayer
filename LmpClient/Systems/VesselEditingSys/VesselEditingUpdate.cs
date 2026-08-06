using LmpClient.Extensions;
using LmpClient.Systems.SettingsSys;
using LmpClient.Systems.TimeSync;
using LmpClient.Systems.Warp;
using LmpClient.VesselUtilities;
using LmpCommon;
using LmpCommon.Message.Data.Editor;
using System;
using UnityEngine;

namespace LmpClient.Systems.VesselEditorSys
{
    /// <summary>
    /// This class handle the vessel editing updates that we received and applies it. 
    /// </summary>
    public class VesselEditingUpdate
    {
        #region Fields

        public VesselEditingUpdate Target { get; set; }

        #region Message Fields

        public UInt32 CraftID { get; set; }
        public string ShipName { get; set; }
        public UInt32[] Parts { get; set; } = new UInt32[100];

        #endregion

        #endregion

        #region Constructor

        public VesselEditingUpdate() { }

        public VesselEditingUpdate(EditorMsgData msgData)
        {
            CraftID = msgData.CraftID;
            ShipName = msgData.ShipName;

            //Array.Copy(msgData.Parts, Parts, 100);
        }

        public void CopyFrom(VesselEditingUpdate update)
        {
            CraftID = update.CraftID;
            ShipName = update.ShipName;

            //Array.Copy(update.Parts, Parts, 100);
        }

        #endregion

        #region Main method

        public void SetEditorStuff()
        {
            EditorLogic.fetch.ship.shipName = ShipName;
        }

        #endregion
    }
}
