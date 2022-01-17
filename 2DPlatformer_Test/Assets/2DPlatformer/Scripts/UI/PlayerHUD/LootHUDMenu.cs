namespace GSGD2.UI
{
    using GSGD2.Gameplay;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;
    public class LootHUDMenu : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _lootAmountText = null;

        private void OnEnable()
        {
            var lootManager = LevelReferences.Instance.LootManager;
            lootManager.LootAdded -= LootManagerOnLootAdded;
            lootManager.LootAdded += LootManagerOnLootAdded;
            lootManager.LootRemoved -= LootManagerOnLootRemoved;
            lootManager.LootRemoved += LootManagerOnLootRemoved;

            UpdateValues(lootManager.CurrentLoot);
        }

        private void OnDisable()
        {
            if (LevelReferences.HasInstance == true)
            {
                LevelReferences.Instance.LootManager.LootAdded -= LootManagerOnLootAdded;
                LevelReferences.Instance.LootManager.LootRemoved -= LootManagerOnLootRemoved;
            }
        }

        private void LootManagerOnLootAdded(LootManager sender, int currentLoot)
        {
            UpdateValues(currentLoot);
        }

        private void LootManagerOnLootRemoved(LootManager sender, int currentLoot)
        {
            UpdateValues(currentLoot);
        }

        private void UpdateValues(int loot)
        {
            _lootAmountText.text = loot.ToString();
        }
    }

}