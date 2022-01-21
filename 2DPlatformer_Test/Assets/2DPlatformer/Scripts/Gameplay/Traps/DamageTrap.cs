namespace GSGD2.Gameplay
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class DamageTrap : ATrap
    {
        [SerializeField]
        private int _damage = 1;

        public override void OnTrapTriggerEnter(PhysicsTriggerEvent triggerEvent, Collider other)
        {
            Debug.LogFormat("Damage: {0}", _damage);
            base.OnTrapTriggerEnter(triggerEvent, other);
        }
    }
}