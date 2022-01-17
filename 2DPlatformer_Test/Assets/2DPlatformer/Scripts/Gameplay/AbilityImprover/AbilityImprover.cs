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
        private AbilityImproverHeader _abilityImproverHUD = null;

        [SerializeField]
        private AbilityImproverButton _abilityImproverMenu = null;

        private InputAction _abilityImproverInteractionInputAction = null;

        private void Start()
        {
            _abilityImproverHUD.gameObject.SetActive(false);
            _abilityImproverMenu.gameObject.SetActive(false);
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
            _abilityImproverMenu.gameObject.SetActive(true);
        }
    }
}