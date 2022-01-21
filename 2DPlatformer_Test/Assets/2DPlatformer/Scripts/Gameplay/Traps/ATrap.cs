namespace GSGD2.Gameplay
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class ATrap : MonoBehaviour
    {
        [SerializeField]
        private bool _destroyOnTrigger = false;

        public virtual void OnTrapTriggerEnter(PhysicsTriggerEvent triggerEvent, Collider other)
        {
            if (_destroyOnTrigger == true)
            {
                Destroy(this.gameObject);
            }
            Debug.Log("EnterTrap");
        }
    }
}