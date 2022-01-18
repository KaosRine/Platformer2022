namespace GSGD2.Gameplay
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.InputSystem;
	using GSGD2.Player;

	/// <summary>
	/// Class that permit to add cheat functionnality to ease production.
	/// </summary>
	public class CheatManager : MonoBehaviour
	{
		private const string GO_TO_PREVIOUS_CHECKPOINT_ACTION_NAME = "GoToPreviousCheckpoint";
		private const string GO_TO_NEXT_CHECKPOINT_ACTION_NAME = "GoToNextCheckpoint";
		private const string ADD_CURRENCY_ACTION_NAME = "AddCurrency";
		private const string REMOVE_CURRENCY_ACTION_NAME = "RemoveCurrency";
		private const string RESTORE_HEALTH_ACTION_NAME = "RestoreHealth";

		[SerializeField]
		private InputActionMapWrapper _inputActionMapWrapper;

		[SerializeField]
		private int _addCurrencyAmount = 1;

		[SerializeField]
		private int _removeCurrencyAmount = 1;

		[SerializeField]
		private int _restoreHealthAmount = 1;

		private InputAction _goToPreviousCheckpointInputAction = null;
		private InputAction _goToNextCheckpointInputAction = null;
		private InputAction _addCurrencyInputAction = null;
		private InputAction _removeCurrencyInputAction = null;
		private InputAction _restoreHealthInputAction = null;

		private PlayerStart _playerStart = null;
		private CameraEventManager _cameraEventManager = null;
		private LootManager _lootManager = null;
		private PlayerDamageable _playerDamageable = null;

		private void Awake()
		{
			var levelReference = LevelReferences.Instance;
			_playerStart = levelReference.PlayerStart;
			_cameraEventManager = levelReference.CameraEventManager;
			_lootManager = levelReference.LootManager;
			levelReference.PlayerReferences.TryGetPlayerDamageable(out _playerDamageable);
		}

		private void OnEnable()
		{
			if (_inputActionMapWrapper.TryFindAction(GO_TO_PREVIOUS_CHECKPOINT_ACTION_NAME, out _goToPreviousCheckpointInputAction, true) == true)
			{
				_goToPreviousCheckpointInputAction.performed -= GoToPreviousCheckpointInputActionOnPerformed;
				_goToPreviousCheckpointInputAction.performed += GoToPreviousCheckpointInputActionOnPerformed;
			}
			if (_inputActionMapWrapper.TryFindAction(GO_TO_NEXT_CHECKPOINT_ACTION_NAME, out _goToNextCheckpointInputAction, true) == true)
			{
				_goToNextCheckpointInputAction.performed -= GoToNextCheckpointInputActionOnPerformed;
				_goToNextCheckpointInputAction.performed += GoToNextCheckpointInputActionOnPerformed;
			}
			if (_inputActionMapWrapper.TryFindAction(ADD_CURRENCY_ACTION_NAME, out _addCurrencyInputAction, true) == true)
            {
				_addCurrencyInputAction.performed -= AddCurrencyInputActionOnPerformed;
				_addCurrencyInputAction.performed += AddCurrencyInputActionOnPerformed;
            }
            if (_inputActionMapWrapper.TryFindAction(REMOVE_CURRENCY_ACTION_NAME, out _removeCurrencyInputAction, true) == true)
            {
				_removeCurrencyInputAction.performed -= RemoveCurrencyInputActionOnPerformed;
				_removeCurrencyInputAction.performed += RemoveCurrencyInputActionOnPerformed;
            }
            if (_inputActionMapWrapper.TryFindAction(RESTORE_HEALTH_ACTION_NAME, out _restoreHealthInputAction, true) == true)
            {
				_restoreHealthInputAction.performed -= RestoreHealthInputActionOnPerformed;
				_restoreHealthInputAction.performed += RestoreHealthInputActionOnPerformed;
            }
		}


        private void OnDisable()
		{
			_goToPreviousCheckpointInputAction.Disable();
			_goToNextCheckpointInputAction.Disable();
			_addCurrencyInputAction.Disable();
			_removeCurrencyInputAction.Disable();

			_goToPreviousCheckpointInputAction.performed -= GoToPreviousCheckpointInputActionOnPerformed;
			_goToNextCheckpointInputAction.performed -= GoToPreviousCheckpointInputActionOnPerformed;
			_addCurrencyInputAction.performed -= AddCurrencyInputActionOnPerformed;
			_removeCurrencyInputAction.performed -= RemoveCurrencyInputActionOnPerformed;

		}

		private void GoToPreviousCheckpointInputActionOnPerformed(InputAction.CallbackContext obj)
		{
			_playerStart.SetPlayerPositionToCheckpoint(false);
			ResetCameraSettings();
		}

		private void GoToNextCheckpointInputActionOnPerformed(InputAction.CallbackContext obj)
		{
			_playerStart.SetPlayerPositionToCheckpoint(true);
			ResetCameraSettings();
		}

        private void AddCurrencyInputActionOnPerformed(InputAction.CallbackContext obj)
        {
			_lootManager.AddLoot(_addCurrencyAmount);
        }

        private void RemoveCurrencyInputActionOnPerformed(InputAction.CallbackContext obj)
        {
			_lootManager.RemoveLoot(_removeCurrencyAmount);
        }

        private void RestoreHealthInputActionOnPerformed(InputAction.CallbackContext obj)
        {
			_playerDamageable.RestoreHealth(_restoreHealthAmount);
        }

		private void ResetCameraSettings()
		{
			_cameraEventManager.SetActiveCameraConfiner(false);
			_cameraEventManager.ExitRoomCamera();
		}
	}
}