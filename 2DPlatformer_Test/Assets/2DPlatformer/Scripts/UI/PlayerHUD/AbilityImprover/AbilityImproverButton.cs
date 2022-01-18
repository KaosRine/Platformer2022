namespace GSGD2.UI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;
    using GSGD2.Gameplay;
    using UnityEngine.UI;

    public class AbilityImproverButton : MonoBehaviour, ICommandSender
    {
        [SerializeField]
        private UpgradePlayerAbilityModifierCommand _abilityCommand = null;

        [SerializeField]
        private TextMeshProUGUI _costText = null;

        private void Start()
        {
            _costText.text = _abilityCommand.AbilityCost.ToString();
        }

        public void ApplyAbilityCommand()
        {
            if (LevelReferences.Instance.LootManager.CurrentLoot >= _abilityCommand.AbilityCost == true)
            {
                _abilityCommand.Apply(this);
                LevelReferences.Instance.LootManager.RemoveLoot(_abilityCommand.AbilityCost);

                Button button = GetComponent<Button>();
                button.interactable = false;
            }
            else
            {
                Debug.Log("Not enough currency");
            }
        }

        GameObject ICommandSender.GetGameObject() => gameObject;
    }
}