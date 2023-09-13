using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ingame.Character
{
    public class CharacterVisual : MonoBehaviour
    {
        [SerializeField]
        private GameObject VisualObject;
        Camera mainCamera;
        Vector3 mousePosition;
        void Start()
        {
            mainCamera = Camera.main;
        }

        void FixedUpdate()
        {
            mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 lookDir = mousePosition - gameObject.transform.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            VisualObject.transform.rotation = Quaternion.Euler(0,0,angle);
        }
    }
}