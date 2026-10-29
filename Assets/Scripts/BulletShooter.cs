using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    public GameObject bulletPrefab;

    public Transform leftFirePoint;
    public Transform rightFirePoint;

    public float bulletSpeed = 50f;


    private int shotCount = 0;


    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            Shoot(leftFirePoint);
            GameManager.Instance.PlayShootSE();
        }

        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            Shoot(rightFirePoint);
            GameManager.Instance.PlayShootSE();
        }
    }

    void Shoot(Transform firePoint)
    {
        GameObject bullet =
            Instantiate(
                bulletPrefab,
                firePoint.position,
                firePoint.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        rb.linearVelocity =
            firePoint.forward * bulletSpeed;
    }
}