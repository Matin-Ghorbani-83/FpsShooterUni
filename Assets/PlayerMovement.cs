using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float hMove;
    float vMove;
    public float speedMove;
    float xRotation;
    float yRotation;
    public float mouseSensitivity = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        // اگر دکمه راست ماوس تازه فشار داده شد
        if (Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // اگر دکمه راست ماوس رها شد
        if (Input.GetMouseButtonUp(1))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // فقط وقتی نگه داشته شده
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            yRotation += mouseX;

            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        }
        void Movement()
        {
            hMove = Input.GetAxis("Horizontal") * speedMove * Time.deltaTime;
            vMove = Input.GetAxis("Vertical") * speedMove * Time.deltaTime;



            Vector3 Movement = new Vector3(hMove ,0, vMove );

            transform.Translate(Movement);
        }

    }
}
