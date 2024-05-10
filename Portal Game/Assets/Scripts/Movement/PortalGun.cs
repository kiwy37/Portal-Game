using System.Collections;
using UnityEngine;

public class PortalGun : MonoBehaviour
{
    public Camera cam;
    public GameObject A;
    public GameObject B;
    public GameObject projectilePrefabA; // Assign this in the inspector
    public GameObject projectilePrefabB; // Assign this in the inspector
    public AudioClip PortalSound;
    public float speed = 20f; // You can adjust this value in the Unity inspector
    private RaycastHit rayHit;
    private static string PORTAL_SURFACE = "PortalSurface";

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out rayHit))
            {
                if (rayHit.transform.gameObject.tag == PORTAL_SURFACE)
                {
                    SoundFXManager.Instance.PlaySoundFXClip(PortalSound, transform, 1f);
                    Invoke("ShootA", 0.1f);
                }
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out rayHit))
            {
                if (rayHit.transform.gameObject.tag == PORTAL_SURFACE)
                {
                    SoundFXManager.Instance.PlaySoundFXClip(PortalSound, transform, 1f);
                    Invoke("ShootB", 0.1f);
                }
            }
        }
    }

    public void ShootA()
    {
        Vector3 hitPos = rayHit.point;
        GameObject hit = rayHit.transform.gameObject;

        // Instantiate projectile and start moving it from camera to portal A
        GameObject projectile = Instantiate(projectilePrefabA, cam.transform.position, Quaternion.identity);
        StartCoroutine(MoveProjectile(projectile, hitPos, 1f, () =>
        {
            A.transform.position = hitPos;
            A.transform.rotation = hit.transform.rotation;
        }));
    }

    public void ShootB()
    {
        RaycastHit rayHit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out rayHit))
        {
            if (rayHit.transform.gameObject.tag == PORTAL_SURFACE)
            {
                Vector3 hitPos = rayHit.point;
                GameObject hit = rayHit.transform.gameObject;

                // Instantiate projectile and start moving it from camera to portal B
                GameObject projectile = Instantiate(projectilePrefabB, cam.transform.position, Quaternion.identity);
                StartCoroutine(MoveProjectile(projectile, hitPos, 1f, () =>
                {
                    B.transform.position = hitPos;
                    B.transform.rotation = hit.transform.rotation;
                }));
            }
        }
    }

    IEnumerator MoveProjectile(GameObject projectile, Vector3 targetPosition, float duration, System.Action onComplete)
    {
        float elapsedTime = 0;
        Vector3 startingPosition = projectile.transform.position;

        while (elapsedTime < duration)
        {
            Vector3 direction = (targetPosition - startingPosition).normalized;
            projectile.transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        projectile.transform.position = targetPosition;
        Destroy(projectile);

        onComplete?.Invoke();
    }
}
