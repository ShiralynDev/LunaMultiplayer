using LmpClient.Base;
using LmpClient.Events;
using LmpClient.Systems.SettingsSys;
using LmpClient.Systems.TimeSync;
using LmpClient.VesselUtilities;
using LmpCommon.Time;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

namespace LmpClient.Systems.VesselEditorSys
{
    /// <summary>
    /// System that handle the received vessel update messages and also sends them
    /// </summary>
    public class VesselEditorSystem : MessageSystem<VesselEditorSystem, VesselEditingMessageSender, VesselEditingMessageHandler>
    {
        #region Fields & properties

        private static DateTime LastEditorUpdatesSentTime { get; set; } = LunaComputerTime.UtcNow;

        /* Needs to be replaced with an update interval for building craft */
        private static int UpdateIntervalLockedToUnity => (int)(Math.Floor(SettingsSystem.ServerSettings.VesselUpdatesMsInterval
            / TimeSpan.FromSeconds(Time.fixedDeltaTime).TotalMilliseconds) * TimeSpan.FromSeconds(Time.fixedDeltaTime).TotalMilliseconds);

        private static bool TimeToSendEditorUpdate => (LunaComputerTime.UtcNow - LastEditorUpdatesSentTime).TotalMilliseconds > UpdateIntervalLockedToUnity;

        public bool EditorUpdateSystemReady => Enabled && HighLogic.LoadedScene == GameScenes.EDITOR &&
                                        EditorLogic.fetch != null;

        public static VesselEditingUpdate EditorUpdate { get; set; } =
            new VesselEditingUpdate();

        public static ConcurrentDictionary<UInt32, EditorUpdateQueue> TargetEditorUpdateQueue { get; } =
            new ConcurrentDictionary<UInt32, EditorUpdateQueue>();

        private Part partToUpdate { get; } = new Part();

        #endregion

        #region Base overrides

        public override string SystemName { get; } = nameof(VesselEditorSystem);

        protected override bool ProcessMessagesInUnityThread => false;

        protected override void OnEnabled()
        {
            base.OnEnabled();

            SetupRoutine(new RoutineDefinition(0, RoutineExecution.Update, SendVesselEditorUpdates));
            SetupRoutine(new RoutineDefinition(0, RoutineExecution.Update, ApplyVesselEditorUpdates));
        }

        protected override void OnDisabled()
        {
            base.OnDisabled();

            EditorUpdate = new VesselEditingUpdate();
            TargetEditorUpdateQueue.Clear();
        }

        #endregion

        #region FixedUpdate methods

        /// <summary>
        /// Send the updates of our own vessel. We only send them after an interval specified.
        /// </summary>
        private void SendVesselEditorUpdates()
        {
            Profiler.BeginSample(nameof(SendVesselEditorUpdates));

            if (EditorUpdateSystemReady && TimeToSendEditorUpdate)
            {
                MessageSender.SendVesselEditorUpdate(EditorLogic.fetch.ship.parts);
                LastEditorUpdatesSentTime = LunaComputerTime.UtcNow;
            }

            Profiler.EndSample();
        }

        private void ApplyVesselEditorUpdates()
        {
            if (!EditorUpdateSystemReady) return;

            foreach (var kvp in TargetEditorUpdateQueue)
            {
                if (kvp.Value.TryDequeue(out var update))
                {
                    update.SetEditorStuff();
                }
            }
        }

        #endregion

        #region Public methods

        #endregion
    }
}
