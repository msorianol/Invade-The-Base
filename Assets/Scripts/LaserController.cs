using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float laserDistance = 8f;
    [SerializeField] private LayerMask ignoreMask;
    [SerializeField] private float laserOnTime = 2f;  // Tiempo que el l�ser permanece encendido
    [SerializeField] private float laserOffTime = 1f; // Tiempo que el l�ser permanece apagado

    private RaycastHit rayHit;
    private Ray ray;
    private bool laserActive = true;

    private void Awake()
    {
        lineRenderer.positionCount = 2;
        StartCoroutine(ToggleLaser());
    }

    private void Update()
    {
        if (!laserActive)
        {
            lineRenderer.enabled = false;
            return;
        }

        lineRenderer.enabled = true;

        ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out rayHit))
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, rayHit.point);

            if (rayHit.collider.TryGetComponent(out Life_Player_Controller l_LifePlayerController))
            {
                l_LifePlayerController.KilledByDeadZone();
            }
        }
        else
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + transform.forward * laserDistance);
        }
    }

    private IEnumerator ToggleLaser()
    {
        while (true)
        {
            laserActive = true;
            yield return new WaitForSeconds(laserOnTime);
            laserActive = false;
            yield return new WaitForSeconds(laserOffTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray.origin, ray.direction * laserDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(rayHit.point, 0.23f);
    }
}
