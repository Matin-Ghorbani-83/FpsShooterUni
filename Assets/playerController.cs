using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 5f;     // سرعت حرکت (m/s)

    void Update()
    {
        // ورودی‌های افقی/عمودی (WASD یا فلش‌ها)
        float h = Input.GetAxisRaw("Horizontal"); // A/D یا چپ/راست
        float v = Input.GetAxisRaw("Vertical");   // W/S یا بالا/پایین

        // بالا/پایین آزاد با Q/E (اختیاری)
        float y = 0f;
        if (Input.GetKey(KeyCode.E)) y = 1f;      // بالا
        if (Input.GetKey(KeyCode.Q)) y = -1f;     // پایین

        // وِکتور حرکت در فضای جهانی
        Vector3 dir = new Vector3(h, y, v).normalized;

        // جابه‌جایی نرم با توجه به deltaTime
        transform.Translate(dir * moveSpeed * Time.deltaTime, Space.World);
    }
}

