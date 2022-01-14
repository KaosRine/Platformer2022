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

        private int _lootAmount;

        public void AddLootCount(int amount)
        {
            _lootAmount += amount;
            _lootAmountText.text = _lootAmount.ToString();
        }

        private void Start()
        {
            _lootAmountText.text = _lootAmount.ToString();
        }
    }

}