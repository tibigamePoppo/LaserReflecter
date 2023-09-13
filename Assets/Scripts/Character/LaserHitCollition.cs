using ingame.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHitCollition : MonoBehaviour
{
    [SerializeField]
    int reflections;
    [SerializeField]
    float MaxRayDistance = 20f;
    [SerializeField]
    LayerMask layerDetection;
    public List<GameObject> hitCollitionList = new List<GameObject>();
    void Awake()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, MaxRayDistance, layerDetection);

        bool isMirror = false;
        Vector2 mirrorHitPoint = Vector2.zero;
        Vector2 mirrorHitNormal = Vector2.zero;


        for (int i = 0; i < reflections; i++)
        {

            if (hitInfo.collider != null)
            {
                if(hitInfo.collider.TryGetComponent(out IDamageable damageCs))
                {
                    hitCollitionList.Add(hitInfo.collider.gameObject);
                }

                isMirror = false;
                if (hitInfo.collider.CompareTag("ReflectPlatform"))
                {
                    mirrorHitPoint = hitInfo.point;
                    mirrorHitNormal = hitInfo.normal;
                    hitInfo = Physics2D.Raycast(hitInfo.point, Vector2.Reflect(hitInfo.point - new Vector2(transform.position.x, transform.position.y), hitInfo.normal), MaxRayDistance, layerDetection);
                    isMirror = true;
                }
                else
                    break;
            }
        }
    }
}
