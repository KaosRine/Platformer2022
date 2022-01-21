namespace GSGD2.Gameplay
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using GSGD2.Player;

    public class StaminaManager : MonoBehaviour
    {
        [SerializeField]
        private int _staminaAtStart = 1;

        [SerializeField]
        private int _maxStamina = 1;

        [SerializeField]
        private float _staminaRecoveryRate = 1f;

        //[SerializeField]
        //private float _staminaFullRecoveryTime = 1f;

        [SerializeField]
        private bool _passiveRegen = true;

        private float _currentStamina = 0;
        private bool _isIncreasing = false;
        private bool _isDecreasing = false;
        private float _increaseRate = 0f;
        private float _decreaseRate = 0f;

        public float CurrentStamina => _currentStamina;
        public float MaxStamina => _maxStamina;
        public float StaminaRecoveryRate => _staminaRecoveryRate;
        //public float StaminaFullRecoveryTime => _staminaFullRecoveryTime;

        public struct StaminaArgs
        {
            public float currentStamina;
            public int maxStamina;
            public float staminaRecoveryRate;
            //public float staminaFullRecoveryTime;

            public StaminaArgs(float currentStamina, int maxStamina, float staminaRecoveryRate)
            {
                this.currentStamina = currentStamina;
                this.maxStamina = maxStamina;
                this.staminaRecoveryRate = staminaRecoveryRate;
                //this.staminaFullRecoveryTime = staminaFullRecoveryTime;
            }
        }

        public delegate void StaminaManagerEvent(StaminaManager sender, StaminaArgs args);
        public event StaminaManagerEvent StaminaIncreasing = null;
        public event StaminaManagerEvent StaminaDecreasing = null;

        private StaminaArgs GetArgs()
        {
            return new StaminaArgs(_currentStamina, _maxStamina, _staminaRecoveryRate);
        }

        //TODO: Use Decrease/Increase Stamina according to states

        private void OnEnable()
        {
            _currentStamina = _staminaAtStart;
            _isIncreasing = _passiveRegen;

            if (_isIncreasing == true && _currentStamina < _maxStamina)
            {
                SetIncreasingStamina(true, _staminaRecoveryRate);
            }
        }

        public void SetIncreasingStamina(bool isIncreasing, float increaseRate)
        {
            _isIncreasing = isIncreasing;
            _increaseRate = increaseRate;
        }

        public void SetDecreasingStamina(bool isDecreasing, float decreaseRate) 
        {
            _isDecreasing = isDecreasing;
            _decreaseRate = decreaseRate;
        }

        private void Update()
        {
            if (_currentStamina > 0 && _isDecreasing == true)
            {
                DoDecreaseStamina();
            }
            else if (_currentStamina < _maxStamina && _isIncreasing == true)
            {
                DoIncreaseStamina();
            }
        }

        private void DoIncreaseStamina()
        {
            _currentStamina += _increaseRate * Time.deltaTime;
            StaminaIncreasing?.Invoke(this, GetArgs());
        }

        private void DoDecreaseStamina()
        {
            _currentStamina -= _decreaseRate * Time.deltaTime;
            StaminaDecreasing?.Invoke(this, GetArgs());
        }
    }
}
