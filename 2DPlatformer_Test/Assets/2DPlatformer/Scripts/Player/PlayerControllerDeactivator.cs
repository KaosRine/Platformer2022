namespace GSGD2.Player
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerControllerDeactivator : MonoBehaviour
    {
        private PlayerController _playerController = null;

        private void Awake()
        {
            LevelReferences levelReference = LevelReferences.Instance;
            levelReference.PlayerReferences.TryGetPlayerController(out _playerController);
        }

        public void DoDeactivatePlayerController()
        {
            if (_playerController != null)
            {
                _playerController.enabled = false;
            }
        }

        public void DoActivatePlayerController()
        {
            if (_playerController != null)
            {
                _playerController.enabled = true;
            }
        }
    }
}