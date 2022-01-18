namespace GSGD2.Gameplay
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using GSGD2.Player;
    using UnityEngine.InputSystem;
    using UnityEngine.UI;
    using GSGD2.UI;

    public class AbilityImprover : MonoBehaviour
    {
        [SerializeField]
        private InputActionMapWrapper _inputActionMap = null;

        [SerializeField]
        private MenuController _abilityImproverMenu = null;

        private InputAction _abilityImproverInteractionInputAction = null;

        private void Awake()
        {
            _abilityImproverMenu.HideMenu();
        }

        public void EnterAbilityImproverTrigger()
        {
            if (_inputActionMap.TryFindAction("AbilityImproverInteraction", out _abilityImproverInteractionInputAction) == true)
            {
                _abilityImproverInteractionInputAction.performed -= AbilityImproverInteractionInputActionPerformed;
                _abilityImproverInteractionInputAction.performed += AbilityImproverInteractionInputActionPerformed;
                _abilityImproverInteractionInputAction.Enable();
            }
        }

        public void ExitAbilityImproverTrigger()
        {
            _abilityImproverInteractionInputAction.performed -= AbilityImproverInteractionInputActionPerformed;

            _abilityImproverInteractionInputAction.Disable();

        }

        private void AbilityImproverInteractionInputActionPerformed(InputAction.CallbackContext obj)
        {
            _abilityImproverMenu.ShowMenu();
        }
    }
}