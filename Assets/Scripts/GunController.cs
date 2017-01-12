using UnityEngine;
using System.Collections;
using EZEffects;
public class GunController : MonoBehaviour {


    public GameObject controllerObject;

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;

    private SteamVR_TrackedController controller;

    public EffectTracer tracerEffect;
    public Transform muzzleTrsfrm;

    public int gun_Damage;
    int bulletsPerMag = 50;
    public int bulletsLeft;

    public int GunID;

    // Use this for initialization
    void Start () {
        controller = controllerObject.GetComponent<SteamVR_TrackedController>();
        controller.TriggerClicked += TriggerPressed;
        trackedObj = controllerObject.GetComponent<SteamVR_TrackedObject>();

        // METTRE EN PLACE DIFFERENTS STYLES D'ARMES
        gun_Damage = 1;
	}

    private void TriggerPressed(object sender, ClickedEventArgs e)
    {
        ShootWeapon();
    }

    public void ShootWeapon()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = new Ray(muzzleTrsfrm.position, muzzleTrsfrm.forward);

        device = SteamVR_Controller.Input((int)trackedObj.index);
        device.TriggerHapticPulse(750);
        tracerEffect.ShowTracerEffect(muzzleTrsfrm.position, muzzleTrsfrm.forward, 250f);

        if (Physics.Raycast(ray, out hit, 5000f))
        {
            if (hit.collider.attachedRigidbody)
            {
                if (hit.collider.GetComponent<Entite>())
                {
                    Debug.Log("HitNormal");
                    hit.collider.GetComponent<Entite>().TakeDamage(gun_Damage);
                    hit.rigidbody.GetComponent<Entite>().Hit(ray.direction);
                }
                if (hit.collider.tag == "Head")
                {
                    Debug.Log("HeadShot");
                    hit.collider.GetComponentInParent<Entite>().TakeDamage(gun_Damage * 2);
                    hit.rigidbody.GetComponentInParent<Entite>().Hit(ray.direction);
                }
            }

        }
        bulletsLeft--;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
