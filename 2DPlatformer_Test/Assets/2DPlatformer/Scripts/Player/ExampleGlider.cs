namespace GSGD2.Player
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class ExampleGlider : MonoBehaviour
    {
        [SerializeField]
        private PlayerReferences _playerReferences = null;


        [SerializeField]
        private float _speedWhileInAir = 5f;

        [SerializeField]
        private float _ascendingGravityScale = 1f;

        [SerializeField]
        private float _descendingGravityScale = 1f;

        [SerializeField]
        private CubeController.State _usableInState = CubeController.State.None;

        private bool _isGliding = false;
        private Rigidbody _rigidbody = null;
        private CubeController _cubeController = null;
        private PlayerController _playerController = null;
        private CharacterCollision _characterCollision = null;

        private void Awake()
        {
            _playerReferences.TryGetRigidbody(out _rigidbody);
            _playerReferences.TryGetCubeController(out _cubeController);
            _playerReferences.TryGetPlayerController(out _playerController);
            _playerReferences.TryGetCharacterCollision(out _characterCollision);
        }

        private void OnEnable()
        {
            _playerController.GlidePerformed -= PlayerControllerOnGlidePerformed;
            _playerController.StopGlidePerformed -= PlayerControllerOnStopGlidePerformed;

            _playerController.GlidePerformed += PlayerControllerOnGlidePerformed;
            _playerController.StopGlidePerformed += PlayerControllerOnStopGlidePerformed;
        }

        private void OnDisable()
        {
            _playerController.GlidePerformed -= PlayerControllerOnGlidePerformed;
            _playerController.StopGlidePerformed -= PlayerControllerOnStopGlidePerformed;
        }

        private void PlayerControllerOnGlidePerformed(PlayerController sender, UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            if (_usableInState.HasFlag(_cubeController.CurrentState) && _isGliding == false)
            {
                _isGliding = true;
                _cubeController.EnableJump(false);
                _cubeController.ChangeState(CubeController.State.None);
                Debug.Log("Gliding");
            }
        }

        private void PlayerControllerOnStopGlidePerformed(PlayerController sender, UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            if (_isGliding == true)
            {
                _isGliding = false;
                _cubeController.EnableJump(true);
                Debug.Log("StopGliding");
            }
        }

        private void Update()
        {
            if (_cubeController.CurrentState == CubeController.State.None && _isGliding == false)
            {
                _cubeController.ChangeState(CubeController.State.Falling);
                _cubeController.ForceCheckGround();
            }

            if (_isGliding == true)
            {
                float movementDirection = _playerController.HorizontalMove;
                if (movementDirection != 0)
                {
                    movementDirection = movementDirection > 0 ? 1 : -1;
                }
                _characterCollision.HandleWallCollisionAndApplyBonusYReplacement((int)movementDirection);

                if (_characterCollision.HasAWallInFrontOfCharacter == true)
                {
                    _cubeController.ChangeState(CubeController.State.Falling);
                    _cubeController.ForceCheckGround();
                    _isGliding = false;
                }

                _cubeController.ForceCheckGround();
                if (_cubeController.CurrentState == CubeController.State.Grounded)
                {
                    _isGliding = false;
                    _cubeController.EnableJump(true);
                    Debug.Log("StopGliding");
                }

                Vector3 velocity = _rigidbody.velocity;
                velocity.z = movementDirection * _speedWhileInAir;
                _rigidbody.velocity = velocity;
            }
        }


        private void FixedUpdate()
        {
            if (_isGliding == true)
            {
                //_rigidbody.velocity = Vector3.zero;

                Vector3 gravityVelocity = _rigidbody.velocity;
                float gravityScale = (gravityVelocity.y > 0 ? _ascendingGravityScale : _descendingGravityScale);
                gravityVelocity.y += Physics.gravity.y * gravityScale * Time.deltaTime;
                _rigidbody.velocity = gravityVelocity;
            }
        }
    }
}
