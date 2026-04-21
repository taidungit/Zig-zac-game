using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour
{
    [Header("Falling Settings")]
    public float delayBeforeFall = 0.5f; 
    public float fallSpeed = 25f;

    private bool _isFalling = false;

    // Sửa thành OnTriggerStay: Nó sẽ kiểm tra liên tục nếu xe còn đứng trên miếng đường này
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !_isFalling)
        {
            CarController car = other.GetComponent<CarController>();

            // Chỉ cần bạn click chuột (gameStarted = true), miếng đường dưới chân sẽ sập ngay
            if (car != null && car._gameStarted)
            {
                _isFalling = true;
                StartCoroutine(FallAndDestroy());
            }
        }
    }

    IEnumerator FallAndDestroy()
    {
        yield return new WaitForSeconds(delayBeforeFall);

        float currentSpeed = 0f;
        // Cho rơi xuống cho đến khi mất hút
        while (transform.position.y > -15f)
        {
            currentSpeed += Time.deltaTime * fallSpeed;
            transform.Translate(Vector3.down * currentSpeed * Time.deltaTime);
            yield return null;
        }

        Destroy(gameObject);
    }
}