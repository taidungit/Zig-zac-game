using UnityEngine;
using System.Collections;

public class RoadSpawner : MonoBehaviour
{
    public static RoadSpawner Instance;

    public GameObject roadPrefab;
    public Vector3 lastPos;
    public float offset = 3f;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Lấy mốc từ miếng đường đầu tiên trong Scene
        GameObject initialRoad = GameObject.Find("RoadBlock_3D");
        if (initialRoad != null) lastPos = initialRoad.transform.position;

        // Ép 5 miếng đầu đi thẳng để làm vạch xuất phát
        for (int i = 0; i < 5; i++)
        {
            SpawnStraight();
        }

        // BẮT ĐẦU VÒNG LẶP SINH ĐƯỜNG TỰ ĐỘNG (Ghi điểm môn C#)
        StartCoroutine(AutoSpawnRoad());
    }

    // Hàm tự động sinh đường mỗi 0.2 giây
    IEnumerator AutoSpawnRoad()
    {
        while (true) // Lặp vô hạn
        {
            SpawnRoad();
            // Đợi một khoảng thời gian ngắn rồi sinh tiếp
            // Nếu xe chạy nhanh hơn, bạn có thể giảm số 0.2f xuống 0.1f
            yield return new WaitForSeconds(0.2f); 
        }
    }

    public void SpawnRoad()
    {
        int rand = Random.Range(0, 2);
        Vector3 spawnPos = lastPos;

        if (rand == 0) spawnPos.z += offset;
        else spawnPos.x += offset;

        Instantiate(roadPrefab, spawnPos, Quaternion.identity);
        lastPos = spawnPos;
    }

    void SpawnStraight()
    {
        Vector3 spawnPos = lastPos;
        spawnPos.z += offset;
        Instantiate(roadPrefab, spawnPos, Quaternion.identity);
        lastPos = spawnPos;
    }
}

