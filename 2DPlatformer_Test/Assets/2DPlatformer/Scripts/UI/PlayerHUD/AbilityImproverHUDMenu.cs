namespace GSGD2.UI
{
    using GSGD2.Gameplay;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;

    public class AbilityImproverHUDMenu : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _abilityCostText = null;

        [SerializeField]
        private AbilityImprover _abilityImprover = null;

        private void Start()
        {
            _abilityCostText.text = _abilityImprover.AbilityCost.ToString();
        }
    }
}