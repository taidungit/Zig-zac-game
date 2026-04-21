using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 8f; 
    [SerializeField] private float speedMultiplier = 0.2f; 
    
    public bool _gameStarted = false;
    private bool _isGameOver = false;
    private bool _movingRight = false;

    public bool IsGameOver => _isGameOver;

    void Update()
    {
        if (_isGameOver) return;

        // Nhấn chuột trái hoặc chạm màn hình để đổi hướng
        if (Input.GetMouseButtonDown(0))
        {
            HandleInput();
        }

        if (_gameStarted)
        {
            Move();
            CheckFall();

            // 1. Tăng tốc độ xe dần đều theo thời gian
            moveSpeed += speedMultiplier * Time.deltaTime;

            // 2. TÍNH ĐIỂM: Cộng điểm dựa trên tốc độ và thời gian (Quãng đường)
            // Xe chạy càng nhanh, điểm nhảy càng lẹ
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScoreOverTime(moveSpeed * Time.deltaTime);
            }
        }
    }

    private void HandleInput()
    {
        if (!_gameStarted)
        {
            _gameStarted = true;
        }
        else
        {
            SwitchDirection();
            // Sinh đường mới mỗi khi rẽ
            RoadSpawner.Instance?.SpawnRoad();
        }
    }

    private void Move()
    {
        // Di chuyển xe về phía trước theo hướng rotation hiện tại
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    private void CheckFall()
    {
        // Nếu xe rơi khỏi mặt đường (trục Y thấp xuống)
        if (transform.position.y < -0.5f)
        {
            _isGameOver = true;
            // Lưu điểm kỷ lục lần cuối trước khi reset
            PlayerPrefs.Save();
            Invoke("RestartGame", 1f); 
        }
    }

    void SwitchDirection()
    {
        _movingRight = !_movingRight;
        // Xoay 90 độ nếu rẽ phải, 0 độ nếu đi thẳng (theo trục Z)
        transform.rotation = _movingRight ? Quaternion.Euler(0, 90, 0) : Quaternion.Euler(0, 0, 0);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}