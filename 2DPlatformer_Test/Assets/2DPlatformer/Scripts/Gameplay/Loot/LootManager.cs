namespace GSGD2.Gameplay
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class LootManager : MonoBehaviour
    {
        private static int _currentLoot = 0;

        public int CurrentLoot => _currentLoot;

        public delegate void LootManagerEvent(LootManager sender, int currentLoot);
        public event LootManagerEvent LootAdded = null;
        public event LootManagerEvent LootRemoved = null;

        public void AddLoot(int value)
        {
            _currentLoot += value;

            LootAdded?.Invoke(this, _currentLoot);
        }

        public void RemoveLoot(int value)
        {
            _currentLoot -= value;
            if (_currentLoot < 0)
            {
                _currentLoot = 0;
            }
            LootRemoved?.Invoke(this, _currentLoot);
        }
    }
}