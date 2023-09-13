using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEffect : MonoBehaviour
{
    [SerializeField]
    LineRenderer lineOfSight;
    [SerializeField]
    int reflections;
    [SerializeField]
    float MaxRayDistance = 20f;
    [SerializeField]
    LayerMask layerDetection;
    [SerializeField]
    private float width = 0.1f;
    void Start()
    {
        Physics2D.queriesStartInColliders = false;
        lineOfSight.positionCount = 1;
        lineOfSight.SetPosition(0, transform.position);
        lineOfSight.SetWidth(width, width);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, MaxRayDistance, layerDetection);

        bool isMirror = false;
        Vector2 mirrorHitPoint = Vector2.zero;
        Vector2 mirrorHitNormal = Vector2.zero;


        for (int i = 0; i < reflections; i++)
        {
            lineOfSight.positionCount += 1;

            if (hitInfo.collider != null)
            {
                lineOfSight.SetPosition(lineOfSight.positionCount - 1, hitInfo.point);

                isMirror = false;
                if (hitInfo.collider.CompareTag("ReflectPlatform"))
                {
                    mirrorHitPoint = hitInfo.point;
                    mirrorHitNormal = hitInfo.normal;
                    hitInfo = Physics2D.Raycast(hitInfo.point, Vector2.Reflect(hitInfo.point- new Vector2(transform.position.x, transform.position.y), hitInfo.normal), MaxRayDistance, layerDetection);
                    isMirror = true;
                }
                else
                    break;
            }
            else
            {
                if (isMirror)
                {
                    lineOfSight.SetPosition(lineOfSight.positionCount - 1, mirrorHitPoint + Vector2.Reflect(mirrorHitPoint, mirrorHitNormal) * MaxRayDistance);
                    break;
                }
                else
                {
                    lineOfSight.SetPosition(lineOfSight.positionCount - 1, transform.position + transform.up * MaxRayDistance);
                    break;
                }
            }
        }
        StartCoroutine(thining());
    }

    IEnumerator thining()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.02f);
            lineOfSight.SetWidth(width - width / (20 - i), width - width / (20 - i));
        }
        Destroy(gameObject);
    }
}
