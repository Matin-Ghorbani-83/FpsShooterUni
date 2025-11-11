using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //1
    [SerializeField]
    private LayerMask pickableLayerMask;

    [SerializeField]
    private Transform playerCameraTransform;

    [SerializeField]
    private Transform pickUpParent;

    [SerializeField]
    private GameObject inHandItem;

    [SerializeField]
    private GameObject pickUpUI;

    [SerializeField]
    [Min(1)]
    private float hitRange = 3f;

    private RaycastHit raycastHit;



    private void Update()
    {
        DetectPickableItems();

        Interact();

        Drop();
    }

    private void DetectPickableItems()
    {




        //Show RayCast
        // Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitRange,color:Color.red);

        if (raycastHit.collider != null)
        {
            pickUpUI.SetActive(false);
        }

        if (inHandItem != null) return;

        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out raycastHit, hitRange, pickableLayerMask))
        {
            pickUpUI.SetActive(true);
        }
    }
    private void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (raycastHit.collider != null)
            {
                Rigidbody rb = raycastHit.collider.GetComponent<Rigidbody>();

                if (raycastHit.collider.GetComponent<Food>())
                {
                    Debug.Log("Its Food");
                    inHandItem = raycastHit.collider.gameObject;

                    inHandItem.transform.position = Vector3.zero;
                    inHandItem.transform.rotation = Quaternion.identity;
                    inHandItem.transform.SetParent(pickUpParent.transform, false);

                    if (rb != null)
                    {
                        rb.isKinematic = true;
                    }
                    return;
                }
                if (raycastHit.collider.GetComponent<Weapon>())
                {
                    Debug.Log("Its Food");
                    inHandItem = raycastHit.collider.gameObject;

                    inHandItem.transform.position = Vector3.zero;
                    inHandItem.transform.rotation = Quaternion.identity;
                    inHandItem.transform.SetParent(pickUpParent.transform, false);

                    if (rb != null)
                    {
                        rb.isKinematic = true;
                    }
                    return;
                }

            }
        }
    }
    private void Drop()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (inHandItem != null)
            {
                inHandItem.transform.SetParent(null);
                inHandItem = null;

                Rigidbody rb = raycastHit.collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                }
            }
        }
    }
}