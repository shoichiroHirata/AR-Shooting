using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private TargetSpawner targetSpawner;

    [Header("Score")]
    public int Score { get; private set; }

    [Header("Timer")]
    [SerializeField]
    private float timeLimit = 60f;

    private float currentTime;

    [Header("UI")]
    [SerializeField]
    private TMP_Text scoreText;

    [SerializeField]
    private TMP_Text timerText;

    [SerializeField]
    private AudioSource bgmSource;

    [SerializeField]
    private AudioClip gameBGM;

    [SerializeField]
    private AudioSource seSource;

    [SerializeField]
    private AudioClip shootSE;

    [SerializeField]
    private AudioClip hitSE;

    [SerializeField]
    private AudioClip destroySE;

    [SerializeField]
    private GameObject resultPanel;

    [SerializeField]
    private TMP_Text finalScoreText;

    private bool isGameOver = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        currentTime = timeLimit;

        UpdateScoreUI();
        UpdateTimerUI();

        resultPanel.SetActive(false);

        if (bgmSource != null && gameBGM != null)
        {
            bgmSource.clip = gameBGM;
            bgmSource.Play();
        }
    }

    private void Update()
    {
        if (isGameOver)
            return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            GameOver();
        }

        UpdateTimerUI();
    }

    public void AddScore(int value)
    {
        Score += value;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = $"Score : {Score}";
    }

    private void UpdateTimerUI()
    {
        timerText.text = $"Time : {currentTime:F1}";
    }

    private void GameOver()
    {
        isGameOver = true;

        // BGM停止
        if (bgmSource != null)
        {
            bgmSource.Stop();
        }

        // スポーン停止
        if (targetSpawner != null)
        {
            targetSpawner.enabled = false;
        }

        // 既存ターゲット削除
        GameObject[] targets =
            GameObject.FindGameObjectsWithTag("Target");

        foreach (GameObject target in targets)
        {
            Destroy(target);
        }

        resultPanel.SetActive(true);

        finalScoreText.text =
            $"SCORE : {Score}";

        StartCoroutine(ReturnToTitle());
    }

    public void PlayShootSE()
    {
        if (shootSE != null)
        {
            seSource.PlayOneShot(shootSE);
        }
    }

    public void PlayHitSE()
    {
        if (hitSE != null)
        {
            seSource.PlayOneShot(hitSE);
        }
    }

    public void PlayDestroySE()
    {
        if (destroySE != null)
        {
            seSource.PlayOneShot(destroySE);
        }
    }

    private IEnumerator ReturnToTitle()
    {
        yield return new WaitForSeconds(15f);

        SceneManager.LoadScene("StartScene");
    }
}