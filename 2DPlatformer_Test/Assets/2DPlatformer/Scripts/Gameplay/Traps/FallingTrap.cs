namespace GSGD2.Gameplay
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class FallingTrap : ATrap
    {
        public override void OnTrapTriggerEnter(PhysicsTriggerEvent triggerEvent, Collider other)
        {
            Debug.Log("Falling");
            base.OnTrapTriggerEnter(triggerEvent, other);
        }
    }
}