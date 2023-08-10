using BepInEx.Logging;
using HarmonyLib;
using Multiplayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;

namespace HumanMaxPlayerMod
{
    [HarmonyPatch(typeof(MultiplayerLobbySettingsMenu), nameof(MultiplayerLobbySettingsMenu.OnGotFocus))]
    public static class MultiplayerLobbySettingsMenu_OnGotFocus
    {
        public static bool Prefix(MenuSelector ___maxPlayersSelector)
        {
            var count = byte.MaxValue - 1;
            if (___maxPlayersSelector.optionLabels.Length != count)
            {
                var optionLabel = ___maxPlayersSelector.optionLabels.First();
                var right = optionLabel.transform.parent.Find("Right");
                right.SetParent(null);
                var newLabels = Enumerable.Range(___maxPlayersSelector.optionLabels.Length + 2, count - ___maxPlayersSelector.optionLabels.Length).Select(x =>
                {
                    var newOptionLabel = UnityEngine.Object.Instantiate(optionLabel, optionLabel.transform.parent, false);
                    newOptionLabel.name = x.ToString();
                    newOptionLabel.text = x.ToString();
                    return newOptionLabel;
                });

                ___maxPlayersSelector.optionLabels = ___maxPlayersSelector.optionLabels.Concat(newLabels).ToArray();
                right.SetParent(optionLabel.transform.parent);
            }

            return true;
        }
    }
}
