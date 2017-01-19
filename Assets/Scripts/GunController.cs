using UnityEngine;
using System.Collections;
using EZEffects;
public class GunController : MonoBehaviour {

    // ------------------------- VIVE ATTRIBUTES --------------------
    public GameObject controllerObject;

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;
    
    private SteamVR_TrackedController controller;

    public EffectTracer tracerEffect;
    public Transform muzzleTrsfrm;
    public bool triggerButtonDown = false;

    // -------------------------- GUNS ATTRIBUTES -------------------------
    public enum GunType { Semi, Auto };
    public GunType gunType;


    private float rpm;
    public int gun_Damage;
    private int gun_Bonus;
    int bulletsPerMag = 50;
    public int bulletsLeft;
    public int GunID;
    public AudioClip gunSound;
    private bool isShooting = false;

    // System:
    private float secondsBetweenShots;
    private float nextPossibleShootTime;

    private PlayerManager player;

    // Use this for initialization
    void Start () {
        controller = controllerObject.GetComponent<SteamVR_TrackedController>();
        //controller.TriggerUnclicked += TriggerUnpressed;
        controller.TriggerClicked += TriggerPressed;
        
        trackedObj = controllerObject.GetComponent<SteamVR_TrackedObject>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        switch(GunID)
        {
            case 1:
                gun_Damage = 1;               
                break;
            case 2:
                gun_Damage = 2;
                bulletsLeft = 30;
                this.rpm = GameParameters.Instance.GunSpeed;
                secondsBetweenShots = 60 / rpm;
                break;
            case 3:
                gun_Damage = 3;
                bulletsLeft = 15;
                break;
        }
	}

    private bool CanShoot()
    {
        bool canShoot = true;

        if (Time.time < nextPossibleShootTime)
        {
            canShoot = false;
        }

        return canShoot;
    }

    //private void TriggerUnpressed(object sender, ClickedEventArgs e)
    //{
    //    triggerButtonDown = false;
    //    Debug.Log("Trigger Up");
    //}
    private void TriggerPressed(object sender, ClickedEventArgs e)
    {
        Debug.Log("Trigger Down");
        triggerButtonDown = true;
        ShootWeapon();
        
    }

    void Update()
    {
        
    }


    public void ShootWeapon()
    {
        if (triggerButtonDown == true)
        {

            
            if (bulletsLeft > 0 || GunID == 1) // Soit il a un pistolet = munitions illimités, soit un autre pistolet et balle limités
            {
                
                if (GunID == 1 || GunID == 3 || GunID == 2) // Coup par coup ( pistolet ) 
                {
                    
                    RaycastHit hit = new RaycastHit();
                    Ray ray = new Ray(muzzleTrsfrm.position, muzzleTrsfrm.forward);

                    device = SteamVR_Controller.Input((int)trackedObj.index);
                    device.TriggerHapticPulse(750);
                    tracerEffect.ShowTracerEffect(muzzleTrsfrm.position, muzzleTrsfrm.forward, 250f);
                    AudioSource audio = GetComponent<AudioSource>();
                    audio.clip = gunSound;
                    audio.volume = 0.1f;
                    audio.Play();
                    bulletsLeft--;
                    player.SetBulletText();
                    Debug.Log(bulletsLeft);
                    if (Physics.Raycast(ray, out hit, 5000f))
                    {
                        if (hit.collider.attachedRigidbody)
                        {
                            if (hit.collider.GetComponent<Entite>())
                            {

                                hit.collider.GetComponent<Entite>().TakeDamage(gun_Damage);
                                hit.rigidbody.GetComponent<Entite>().Hit(ray.direction);
                            }
                            if (hit.collider.tag == "Head")
                            {
                                Debug.Log("HeadShot");
                                hit.collider.GetComponentInParent<Entite>().TakeDamage(gun_Damage * 2);
                                hit.rigidbody.GetComponentInParent<Entite>().Hit(ray.direction);
                            }
                            if (hit.collider.tag == "Equip_MP7")
                            {
                                Debug.Log("Toucher Bonus MP7");
                                player.EquipWeapon(2);
                                hit.collider.GetComponentInParent<Bonus>().Destroy();
                            }
                            if (hit.collider.tag == "Equip_LaserGun")
                            {
                                Debug.Log("Toucher Bonus Laser");
                                player.EquipWeapon(3);
                                hit.collider.GetComponentInParent<Bonus>().Destroy();
                            }
                            
                        }
                        
                    }
                    CheckNbBullets();


                    //}

                    //if (GunID == 2) // Tir automatique
                    //{

                    //    if (CanShoot()) // regarde si le temps entre chaque tir est passé
                    //    {
                    //        isShooting = true;
                    //        RaycastHit hit = new RaycastHit();
                    //        Ray ray = new Ray(muzzleTrsfrm.position, muzzleTrsfrm.forward);

                    //        device = SteamVR_Controller.Input((int)trackedObj.index);
                    //        device.TriggerHapticPulse(750);
                    //        tracerEffect.ShowTracerEffect(muzzleTrsfrm.position, muzzleTrsfrm.forward, 250f);

                    //        if (Physics.Raycast(ray, out hit, 5000f))
                    //        {
                    //            if (hit.collider.attachedRigidbody)
                    //            {
                    //                if (hit.collider.GetComponent<Entite>())
                    //                {

                    //                    hit.collider.GetComponent<Entite>().TakeDamage(gun_Damage);
                    //                    hit.rigidbody.GetComponent<Entite>().Hit(ray.direction);
                    //                }
                    //                if (hit.collider.tag == "Head")
                    //                {
                    //                    Debug.Log("HeadShot");
                    //                    hit.collider.GetComponentInParent<Entite>().TakeDamage(gun_Damage * 2);
                    //                    hit.rigidbody.GetComponentInParent<Entite>().Hit(ray.direction);
                    //                }
                    //                if (hit.collider.tag == "Equip_MP7")
                    //                {
                    //                    Debug.Log("Toucher Bonus MP7");
                    //                    player.EquipWeapon(2);
                    //                    hit.collider.GetComponentInParent<Bonus>().Destroy();
                    //                }
                    //                if (hit.collider.tag == "Equip_LaserGun")
                    //                {
                    //                    Debug.Log("Toucher Bonus Laser");
                    //                    player.EquipWeapon(3);
                    //                    hit.collider.GetComponentInParent<Bonus>().Destroy();
                    //                }
                    //            }
                    //        }
                    //        nextPossibleShootTime = Time.time + secondsBetweenShots;

                    //        nextPossibleShootTime = Time.time + secondsBetweenShots;






                    //    }
                    //}

                }
            }
           
        }
    }

    private void CheckNbBullets()
    {
        if (bulletsLeft > 0 || GunID != 1)
        {
            return;
        }
        else
        {
            Destroy(gameObject);
            
        }
    }
	
	// Update is called once per frame

}
