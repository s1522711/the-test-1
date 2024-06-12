using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBulletOnActivate : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float bulletSpeed = 20;
    public float fireDelay = 0.5f; // Delay in seconds
    private bool isTriggerPressed = false;
    private bool canFire = true;
    
    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(TriggerPressed);
        grabbable.deactivated.AddListener(TriggerReleased);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isTriggerPressed && canFire)
        {
            StartCoroutine(FireBulletWithDelay());
        }
    }
    
    public void TriggerPressed(ActivateEventArgs args)
    {
        isTriggerPressed = true;
    }
    
    public void TriggerReleased(DeactivateEventArgs args)
    {
        isTriggerPressed = false;
    }
    
    IEnumerator FireBulletWithDelay()
    {
        canFire = false;
        FireBullet();
        yield return new WaitForSeconds(fireDelay);
        canFire = true;
    }
    
    public void FireBullet()
    {
        GameObject spawnedBullet = Instantiate(bullet);
        spawnedBullet.transform.position = spawnPoint.position;
        spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * bulletSpeed;
        Destroy(spawnedBullet, 5);
    }
}
