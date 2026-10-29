using TMPro;
using UnityEngine;

public class DebugUI : MonoBehaviour
{
    public static DebugUI Instance;

    public TMP_Text debugText;

    private void Awake()
    {
        Instance = this;
    }

    public void Log(string message)
    {
        debugText.text = message;
    }
}