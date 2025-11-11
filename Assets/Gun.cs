
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public Camera fpsCam;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    public float fireRate = 15f;
    private float nextTimeToFire;
    // Start is called before the first frame update
    void Start()
    {
        muzzleFlash.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)&& Time.time>nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1/fireRate;
            Shoot();

        }
        else
        {
            muzzleFlash.Stop();
        }
       
    }

    private void Shoot()
    {

        muzzleFlash.Play();
        RaycastHit hit;
        Target target;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            hit.transform.TryGetComponent<Target>(out target);
            if (target != null)
            {
                {
                    target.TakeDamage(damage);
                    print(target.health);
                   
                }

            }
            GameObject wood =Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(wood,2);
        }

       

    }
}