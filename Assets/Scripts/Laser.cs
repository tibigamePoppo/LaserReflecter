using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    LineRenderer lineOfSight;
    [SerializeField]
    int reflections;
    [SerializeField]
    float MaxRayDistance;
    [SerializeField]
    LayerMask layerDetection;
    public float rotationSpeed;
    void Start()
    {
        Physics2D.queriesStartInColliders = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(rotationSpeed * Vector3.forward * Time.deltaTime);

        lineOfSight.positionCount = 1;
        lineOfSight.SetPosition(0, transform.position);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, MaxRayDistance, layerDetection);

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
                if (hitInfo.collider.CompareTag("ReflectPlatform") )
                {
                    mirrorHitPoint = hitInfo.point;
                    mirrorHitNormal = hitInfo.normal;
                    hitInfo = Physics2D.Raycast(hitInfo.point , Vector2.Reflect(hitInfo.point , hitInfo.normal), MaxRayDistance, layerDetection);
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
                    lineOfSight.SetPosition(lineOfSight.positionCount - 1, transform.position + transform.right * MaxRayDistance);
                    break;
                }
            }
        }
    }
}
