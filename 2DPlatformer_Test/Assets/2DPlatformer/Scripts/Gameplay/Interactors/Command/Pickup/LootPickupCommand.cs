namespace GSGD2.Gameplay
{
    using GSGD2.UI;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;

    [CreateAssetMenu(menuName = "GameSup/LootPickupCommand", fileName = "LootPickupCommand")]
    public class LootPickupCommand : PickupCommand
    {
        [SerializeField]
        private int _pickupAmount = 1;

        protected override bool ApplyPickup(ICommandSender from)
        {
            LevelReferences.Instance.LootManager.AddLoot(_pickupAmount);
            return true;
        }
    }
}