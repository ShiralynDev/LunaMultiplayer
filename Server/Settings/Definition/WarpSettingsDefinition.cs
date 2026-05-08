using LmpCommon.Enums;
using LmpCommon.Xml;
using System;

namespace Server.Settings.Definition
{
    [Serializable]
    public class WarpSettingsDefinition
    {
        [XmlComment(Value = "Specify the warp Type. Values: None, Subspace")]
        public WarpMode WarpMode { get; set; } = WarpMode.Subspace;
        [XmlComment(Value = "Tells the server to warp forward on startup to compensate for the time the server was down.")]
        public bool WarpSyncOnStartup { get; set; } = true;
    }
}
