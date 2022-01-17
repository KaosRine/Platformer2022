namespace GSGD2.Gameplay
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(menuName = "GameSup/UpgradePlayerAbilityModifierCommand", fileName = "UpgradePlayerAbilityModifierCommand")]
    public class UpgradePlayerAbilityModifierCommand : PlayerAbilityModifierCommand
    {
        [SerializeField]
        private int _abilityCost = 1;

        public int AbilityCost => _abilityCost;
    }
}