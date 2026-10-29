using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTarget : MonoBehaviour
{
    [SerializeField]
    private float blinkInterval = 0.2f;

    [SerializeField]
    private AudioClip startSE;

    private AudioSource audioSource;
    private Renderer targetRenderer;

    private Color[] colors =
    {
        Color.red,
        Color.yellow,
        Color.green
    };

    private int currentColorIndex = 0;
    private bool isStarting = false;

    private void Start()
    {
        targetRenderer = GetComponent<Renderer>();

        audioSource = GetComponent<AudioSource>();

        InvokeRepeating(
            nameof(ChangeColor),
            0f,
            blinkInterval);
    }

    private void ChangeColor()
    {
        if (targetRenderer == null)
            return;

        currentColorIndex++;

        if (currentColorIndex >= colors.Length)
        {
            currentColorIndex = 0;
        }

        targetRenderer.material.color =
            colors[currentColorIndex];
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isStarting)
            return;

        if (collision.gameObject.CompareTag("Bullet"))
        {
            isStarting = true;

            CancelInvoke(nameof(ChangeColor));

            if (startSE != null && audioSource != null)
            {
                audioSource.PlayOneShot(startSE);
            }

            Invoke(nameof(StartGame), 1.0f);
        }
    }

    private void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}