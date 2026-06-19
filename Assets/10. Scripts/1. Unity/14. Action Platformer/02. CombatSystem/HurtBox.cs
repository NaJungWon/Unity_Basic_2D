using UnityEngine;

namespace Study_ActionPlatformer
{
    public class HurtBox : MonoBehaviour
    {
        [field: SerializeField] public CombatEntity Owner { get; private set; }

        private void Awake()
        {
            Owner = GetComponent<CombatEntity>();
            if(Owner == null) Owner = GetComponentInParent<CombatEntity>();

            if (Owner == null)
            {
                Debug.LogError($"{name} : CombatEntity를 찾지 못했습니다.");
            }
        }
    }
}

