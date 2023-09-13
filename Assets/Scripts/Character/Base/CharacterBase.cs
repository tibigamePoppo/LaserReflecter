using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ingame.Character
{
    public class CharacterBase : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private int baseHp = 100;
        private int currentHp;
        [SerializeField]
        private int baseMoveSpeed = 300;
        Vector3 moveBelocity;
        Rigidbody2D rigbody2d;
        protected void Start()
        {
            currentHp = baseHp;
            rigbody2d = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            moveBelocity = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                moveBelocity += transform.up;
            }
            if (Input.GetKey(KeyCode.S))
            {
                moveBelocity -= transform.up;
            }
            if (Input.GetKey(KeyCode.D))
            {
                moveBelocity += transform.right;
            }
            if (Input.GetKey(KeyCode.A))
            {
                moveBelocity -= transform.right;
            }
            moveBelocity = moveBelocity.normalized;
            rigbody2d.velocity = moveBelocity * baseMoveSpeed * Time.deltaTime;
        }

        public void addDamage(int damage)
        {
            currentHp -= damage;
            if (currentHp < 0)
            {
                Debug.Log("CharacterDead");
                currentHp = 0;
            }
        }
    }
}