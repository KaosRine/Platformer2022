namespace GSGD2.Gameplay
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using GSGD2.Player;

    public class Zipline : MonoBehaviour
    {
        [SerializeField]
        private Transform _startPoint = null;

        [SerializeField]
        private Transform _endPoint = null;

        [SerializeField]
        private float _speed = 1f;

        [SerializeField]
        private float _tilt = 0f;

        private bool _isOnZipline = false;
        private CubeController _cubeController = null;
        private PlayerController _playerController = null;
        private Rigidbody _rigidbody = null;
        private CharacterCollision _characterCollision = null;

        public void OnZiplineTriggerEnter(PhysicsTriggerEvent triggerEvent, Collider other)
        {
            _cubeController = other.GetComponentInParent<CubeController>();
            _playerController = other.GetComponentInParent<PlayerController>();
            _rigidbody = other.GetComponentInParent<Rigidbody>();
            _characterCollision = other.GetComponentInParent<CharacterCollision>();

            if (HasReferences() == true)
            {
                _isOnZipline = true;
                _playerController.enabled = false;
                _cubeController.transform.position = _startPoint.position;
                _cubeController.ChangeState(CubeController.State.None);
            }
        }

        public void OnZiplineTriggerExit(PhysicsTriggerEvent triggerEvent, Collider other)
        {
            if (HasReferences() == true && _isOnZipline == true)
            {
                _isOnZipline = false;
                _playerController.enabled = true;

                _cubeController.transform.Rotate(0, 0, 0);

                _cubeController.ChangeState(CubeController.State.Falling);
                _cubeController.ForceCheckGround();

                ClearReferences();
            }
        }

        private void Update()
        {
            if (_isOnZipline == true)
            {
                MoveOnZipline(_endPoint.transform.position);
                _cubeController.transform.Rotate(_tilt, 0, 0);

                if (_characterCollision.HasAWallInFrontOfCharacter == true)
                {
                    _cubeController.ChangeState(CubeController.State.Falling);
                    _cubeController.ForceCheckGround();
                    _isOnZipline = false;
                }
            }
        }

        private void FixedUpdate()
        {
            if (_isOnZipline == true)
            {
                _rigidbody.velocity = Vector3.zero;
            }
        }

        private void OnDrawGizmos()
        {
            if (_startPoint != null && _endPoint != null)
            {
                Gizmos.DrawLine(_startPoint.position, _endPoint.position);
            }
        }

        private bool HasReferences()
        {
            if (_cubeController != null 
                && _playerController != null 
                && _rigidbody != null
                && _characterCollision != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void MoveOnZipline(Vector3 endZipline)
        {
            _cubeController.transform.position += (endZipline - _cubeController.transform.position).normalized * _speed * Time.deltaTime;
        }

        private void ClearReferences()
        {
            _cubeController = null;
            _playerController = null;
            _rigidbody = null;
            _characterCollision = null;
        }
    }
}