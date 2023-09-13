using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ingame.Character
{
    public class CharacterAttack : MonoBehaviour
    {
        [SerializeField]
        GameObject LaserEffect;
        [SerializeField]
        float reloadTime = 1f;
        [SerializeField]
        private Transform InstantiateParent;
        [SerializeField]
        private int AttackPower = 50;
        void Start()
        {
            StartCoroutine(AttackWait());
        }

        void Update()
        {
        }

        IEnumerator AttackWait()
        {
            while (true)
            {
                yield return new WaitUntil(() => Input.GetKey(KeyCode.Mouse0));
                Attack();
                yield return new WaitForSeconds(reloadTime);
            }
        }
        private void Attack()
        {
            var effect = Instantiate(LaserEffect, InstantiateParent);
            if(effect.TryGetComponent(out LaserHitCollition hitCollitionCs))
            {
                Debug.Log($"Count{hitCollitionCs.hitCollitionList.Count}");
                if (hitCollitionCs.hitCollitionList.Count < 1) return;
                GameObject hitObject = hitCollitionCs.hitCollitionList[0];

                if (hitObject.TryGetComponent(out IDamageable damageCs))
                {
                    Debug.Log("damageCs");
                    damageCs.addDamage(AttackPower);
                    Debug.Log($"{hitObject.name}‚É{AttackPower}ƒ_ƒ[ƒW‚ð—^‚¦‚Ü‚µ‚½");
                }
            }
        }
    }
}