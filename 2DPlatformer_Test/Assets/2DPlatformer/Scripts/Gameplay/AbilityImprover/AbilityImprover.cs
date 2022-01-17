namespace GSGD2.Gameplay
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using GSGD2.Player;
    using UnityEngine.InputSystem;
    using UnityEngine.UI;
    using GSGD2.UI;

    public class AbilityImprover : MonoBehaviour, ICommandSender
    {
        [SerializeField]
        private InputActionMapWrapper _inputActionMap = null;

        [SerializeField]
        private AbilityImproverHUDMenu _abilityImproverHUD = null;

        [SerializeField]
        private int _abilityCost = 5;

        [SerializeField]
        private PlayerAbilityModifierCommand _playerAbilityModifier = null;

        private InputAction _abilityImproverInteractionInputAction = null;

        public int AbilityCost => _abilityCost;

        private void Start()
        {
            _abilityImproverHUD.gameObject.SetActive(false);
        }

        public void EnterAbilityImproverTrigger()
        {
            if (_inputActionMap.TryFindAction("AbilityImproverInteraction", out _abilityImproverInteractionInputAction) == true)
            {
                _abilityImproverInteractionInputAction.performed -= AbilityImproverInteractionInputActionPerformed;
                _abilityImproverInteractionInputAction.performed += AbilityImproverInteractionInputActionPerformed;
                _abilityImproverInteractionInputAction.Enable();

                _abilityImproverHUD.gameObject.SetActive(true);
            }
        }

        public void ExitAbilityImproverTrigger()
        {
            _abilityImproverInteractionInputAction.performed -= AbilityImproverInteractionInputActionPerformed;

            _abilityImproverInteractionInputAction.Disable();

            _abilityImproverHUD.gameObject.SetActive(false);
        }

        private void AbilityImproverInteractionInputActionPerformed(InputAction.CallbackContext obj)
        {
            if (LevelReferences.Instance.LootManager.CurrentLoot >= _abilityCost)
            {
                _playerAbilityModifier.Apply(this);
            }
            else
            {
                Debug.Log("Not enough curreny");
            }
        }

        GameObject ICommandSender.GetGameObject() => gameObject;
    }
}