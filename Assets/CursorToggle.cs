using UnityEngine;

public class CursorToggle : MonoBehaviour
{
    void Update()
    {
        // وقتی کلید S فشار داده بشه
        if (Input.GetKeyDown(KeyCode.S))
        {
            // اگر کرسر مخفیه، نشونش بده و از حالت قفل خارجش کن
            if (!Cursor.visible)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            // اگر کرسر فعاله، دوباره قفلش کن و مخفیش کن
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
