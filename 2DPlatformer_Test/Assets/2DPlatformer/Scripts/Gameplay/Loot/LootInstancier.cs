namespace GSGD2.Gameplay
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class LootInstancier : MonoBehaviour
    {
        [SerializeField]
        private PickupInteractor _loot = null;

        public void InstantiateLoot()
        {
            PickupInteractor loot = Instantiate<PickupInteractor>(_loot);
            loot.transform.position = transform.position;
        }
    }
}