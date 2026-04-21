using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    [Header("Skybox Materials")]
    public Material nightSkybox; // Kéo file Night vào đây
    public Material daySkybox;   // Kéo file Day vào đây

    [Header("Settings")]
    public float timeToChange = 10f; // Sau 10 giây sẽ đổi bầu trời
    public float transitionSpeed = 0.5f; // Tốc độ mượt khi đổi (nếu dùng hiệu ứng)

    private bool _isDay = false;

    void Start()
    {
        // Lúc đầu mới vào luôn là ban đêm
        RenderSettings.skybox = nightSkybox;
    }

    void Update()
    {
        // Nếu game đã chạy được một khoảng thời gian và chưa chuyển sang ban ngày
        if (Time.timeSinceLevelLoad > timeToChange && !_isDay)
        {
            ChangeToDay();
        }
    }

    public void ChangeToDay()
    {
        _isDay = true;
        // Đổi bầu trời sang Ban ngày
        RenderSettings.skybox = daySkybox;
        
        // Cập nhật lại ánh sáng của toàn Scene để không bị tối
        DynamicGI.UpdateEnvironment();
    }

    // Hàm này để bạn có thể gọi từ script khác (ví dụ khi đạt 100 điểm)
    public void ResetToNight()
    {
        _isDay = false;
        RenderSettings.skybox = nightSkybox;
        DynamicGI.UpdateEnvironment();
    }
}