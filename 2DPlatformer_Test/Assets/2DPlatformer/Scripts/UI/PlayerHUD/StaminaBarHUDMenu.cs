namespace GSGD2.UI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using GSGD2.Gameplay;

    public class StaminaBarHUDMenu : MonoBehaviour
    {
        [SerializeField]
        private Image _staminaBarForeground = null;

        private StaminaManager _staminaManager = null;

        private void Awake()
        {
            _staminaManager = LevelReferences.Instance.Player.GetComponent<StaminaManager>();
        }

        private void OnEnable()
        {
            _staminaManager.StaminaIncreasing -= StaminaManagerOnStaminaIncreasing;
            _staminaManager.StaminaDecreasing -= StaminaManagerOnStaminaDecreasing;

            _staminaManager.StaminaIncreasing += StaminaManagerOnStaminaIncreasing;
            _staminaManager.StaminaDecreasing += StaminaManagerOnStaminaDecreasing;
        }


        private void OnDisable()
        {
            _staminaManager.StaminaIncreasing -= StaminaManagerOnStaminaIncreasing;
            _staminaManager.StaminaDecreasing -= StaminaManagerOnStaminaDecreasing;

        }

        private void Start()
        {
            if (_staminaManager != null)
            {
                UpdateStamina(_staminaManager.CurrentStamina, _staminaManager.MaxStamina);
            }
        }

        private void StaminaManagerOnStaminaIncreasing(StaminaManager sender, StaminaManager.StaminaArgs args)
        {
            UpdateStamina(args.currentStamina, args.maxStamina);
        }

        private void StaminaManagerOnStaminaDecreasing(StaminaManager sender, StaminaManager.StaminaArgs args)
        {
            UpdateStamina(args.currentStamina, args.maxStamina);
        }

        private void UpdateStamina(float stamina, float maxStamina)
        {
            float perc = Mathf.Clamp01(stamina / maxStamina);
            _staminaBarForeground.fillAmount = perc;
        }
    }
}