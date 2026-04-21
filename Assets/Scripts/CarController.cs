using UnityEngine;

using UnityEngine.SceneManagement;



public class CarController : MonoBehaviour

{

    [Header("Movement Settings")]

    [SerializeField] private float moveSpeed = 8f; // Tăng tốc độ khởi đầu lên 8 cho máu

    [SerializeField] private float speedMultiplier = 0.2f; // Tốc độ tăng mỗi giây

   

    private bool _gameStarted = false;

    private bool _isGameOver = false;

    private bool _movingRight = false;



    public bool IsGameOver => _isGameOver;



    void Update()

    {

        if (_isGameOver) return;



        if (Input.GetMouseButtonDown(0))

        {

            HandleInput();

        }



        if (_gameStarted)

        {

            Move();

            CheckFall();

            // Tăng tốc độ dần đều

            moveSpeed += speedMultiplier * Time.deltaTime;

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

            RoadSpawner.Instance?.SpawnRoad();

            // Mỗi lần rẽ thành công thì cộng điểm

            ScoreManager.Instance?.AddScore(1);

        }

    }



    private void Move()

    {

        // Chạy thẳng theo hướng hiện tại của xe

        transform.position += transform.forward * moveSpeed * Time.deltaTime;

    }



    private void CheckFall()

    {

        // Nếu rơi sâu xuống vực

        if (transform.position.y < -0.5f)

        {

            _isGameOver = true;

            Invoke("RestartGame", 1f); // Đợi 1 giây cho kịch tính rồi mới reset

        }

    }



    void SwitchDirection()

    {

        _movingRight = !_movingRight;

        transform.rotation = _movingRight ? Quaternion.Euler(0, 90, 0) : Quaternion.Euler(0, 0, 0);

    }



    void RestartGame()

    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

}
