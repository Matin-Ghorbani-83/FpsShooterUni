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
    private Transform pickUpParentTransfrom;

    [SerializeField]
    private GameObject inHandItemGameObject;

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

        Use();
    }

    private void DetectPickableItems()
    {




        //Show RayCast
        // Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitRange,color:Color.red);

        if (raycastHit.collider != null)
        {
            pickUpUI.SetActive(false);
        }

        if (inHandItemGameObject != null) return;

        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out raycastHit, hitRange, pickableLayerMask))
        {
            pickUpUI.SetActive(true);
        }
    }
    private void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (raycastHit.collider != null && inHandItemGameObject == null)
            {
                Rigidbody rb = raycastHit.collider.GetComponent<Rigidbody>();

                IPickable pickable = raycastHit.collider.GetComponent<IPickable>();
                if (pickable != null)
                {
                    inHandItemGameObject = pickable.PickUp();
                    inHandItemGameObject.transform.SetParent(pickUpParentTransfrom.transform, false);
                }

                //if (raycastHit.collider.GetComponent<Food>())
                //{
                //    Debug.Log("Its Food");
                //    inHandItemGameObject = raycastHit.collider.gameObject;

                //    inHandItemGameObject.transform.position = Vector3.zero;
                //    inHandItemGameObject.transform.rotation = Quaternion.identity;
                //    inHandItemGameObject.transform.SetParent(pickUpParentTransfrom.transform, false);

                //    if (rb != null)
                //    {
                //        rb.isKinematic = true;
                //    }
                //    return;
                //}
                //if (raycastHit.collider.GetComponent<Weapon>())
                //{
                //    Debug.Log("Its Food");
                //    inHandItemGameObject = raycastHit.collider.gameObject;

                //    inHandItemGameObject.transform.position = Vector3.zero;
                //    inHandItemGameObject.transform.rotation = Quaternion.identity;
                //    inHandItemGameObject.transform.SetParent(pickUpParentTransfrom.transform, false);

                //    if (rb != null)
                //    {
                //        rb.isKinematic = true;
                //    }
                //    return;
                //}

            }
        }
    }
    private void Drop()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (inHandItemGameObject != null)
            {
                inHandItemGameObject.transform.SetParent(null);
                inHandItemGameObject = null;

                Rigidbody rb = raycastHit.collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                }
            }
        }
    }
    private void Use()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (inHandItemGameObject != null)
            {
                IUsable usable = inHandItemGameObject.GetComponent<IUsable>();
                if (usable != null)
                {
                    usable.Use(this.gameObject);
                }
            }
        }
    }
}