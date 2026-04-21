using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Singleton để có thể gọi từ bất cứ đâu
    public static SoundManager instance;

    [Header("Audio Sources")]
    public AudioSource musicSource; // Nguồn phát nhạc nền
    public AudioSource sfxSource;   // Nguồn phát âm thanh hiệu ứng

    [Header("Audio Clips")]
    public AudioClip backgroundMusic; // File nhạc mình vừa gen
    public AudioClip scoreSound;      // Tiếng khi ăn điểm
    public AudioClip fallSound;       // Tiếng khi đường sập/xe rơi

    void Awake()
    {
        // Khởi tạo Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Giữ âm thanh không bị ngắt khi đổi cảnh
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Phát nhạc nền ngay khi vào game
        if (backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    // Hàm để phát tiếng hiệu ứng (gọi từ script khác)
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}