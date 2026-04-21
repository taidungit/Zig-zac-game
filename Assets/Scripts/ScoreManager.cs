using UnityEngine;

using TMPro;

public class ScoreManager : MonoBehaviour

{

    public static ScoreManager Instance;

   

    [Header("Score Data")]

    public int score = 0;

    public int highScore = 0;



    [Header("UI References")]

    // Kéo cái Text hiển thị điểm vào đây trong Inspector

    public TextMeshProUGUI scoreText;

    // Kéo cái Text hiển thị kỷ lục vào đây (nếu có)

    public TextMeshProUGUI highScoreText;



    void Awake()

    {

        if (Instance == null) Instance = this;

       

        // Lấy kỷ lục cũ khi vừa mở game

        highScore = PlayerPrefs.GetInt("HighScore", 0);

        UpdateUI();

    }



    public void AddScore(int amount)

    {

        score += amount;

       

        if (score > highScore)

        {

            highScore = score;

            PlayerPrefs.SetInt("HighScore", highScore);

        }



        UpdateUI();

    }



    // Hàm riêng để cập nhật chữ lên màn hình (Đúng chuẩn Clean Code)

    void UpdateUI()

    {

        if (scoreText != null)

            scoreText.text = "Score: " + score.ToString();



        if (highScoreText != null)

            highScoreText.text = "Best: " + highScore.ToString();

    }

}

