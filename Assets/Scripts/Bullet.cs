using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 20f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Target target =
            collision.gameObject.GetComponent<Target>();

        if (target != null)
        {
            target.Hit();
        }

        Destroy(gameObject);
    }
}