using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour
{
    [SerializeField]
    protected int maxHp = 1;

    [SerializeField]
    protected int scoreValue = 1;

    protected int currentHp;

    private bool isDead = false;

    protected virtual void Start()
    {
        currentHp = maxHp;
    }

    public virtual void Hit()
    {
        if (isDead)
        {
            return;
        }

        GameManager.Instance.PlayHitSE();

        currentHp--;

        Debug.Log($"{name} HP:{currentHp}");

        if (currentHp <= 0)
        {
            isDead = true;
            StartCoroutine(DieCoroutine());
        }
    }

    protected virtual void Die()
    {
        GameManager.Instance.AddScore(scoreValue);
        Destroy(gameObject);
    }

    protected virtual IEnumerator DieCoroutine()
    {
        GameManager.Instance.PlayDestroySE();
        GameManager.Instance.AddScore(scoreValue);

        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            Color originalColor =
                renderer.material.color;

            for (int i = 0; i < 5; i++)
            {
                renderer.material.color =
                    Color.white;

                yield return new WaitForSeconds(0.05f);

                renderer.material.color =
                    originalColor;

                yield return new WaitForSeconds(0.05f);
            }
        }

        Destroy(gameObject);
    }
}