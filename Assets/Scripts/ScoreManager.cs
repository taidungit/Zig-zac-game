using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("Score Data")]
    public float score = 0;
    public float highScore = 0;

    [Header("UI References")]
    // Kéo Text Mesh Pro hiển thị điểm vào đây
    public TextMeshProUGUI scoreText;
    // Kéo Text Mesh Pro hiển thị kỷ lục vào đây
    public TextMeshProUGUI highScoreText;

    void Awake()
    {
        if (Instance == null) Instance = this;

        // Lấy kỷ lục cũ (lưu dưới dạng số nguyên cho gọn)
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateUI();
    }

    // Hàm cộng điểm liên tục mỗi khung hình
    public void AddScoreOverTime(float amount)
    {
        score += amount;

        if (score > highScore)
        {
            highScore = score;
            // Lưu kỷ lục mới
            PlayerPrefs.SetInt("HighScore", (int)highScore);
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        // Hiển thị số nguyên (ép kiểu float về int) để nhìn điểm không bị lẻ
        if (scoreText != null)
            scoreText.text = "Score: " + ((int)score).ToString();

        if (highScoreText != null)
            highScoreText.text = "Best: " + ((int)highScore).ToString();
    }
}