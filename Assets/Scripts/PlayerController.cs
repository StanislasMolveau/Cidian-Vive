using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    //Joueur
    public int health = 5;
    private float dmgDelay = 3;
    private float dmgtmp;
    public AudioClip equip_gun;
    public AudioClip dmg_sound;
    [HideInInspector]
    public bool CanShoot = true;
    private Rigidbody rigidbody;
    public Transform handHold;
    [HideInInspector]
    public GunController currentGun;
    private Vector3 startPosGun;
    private CharacterController controller;
    private Animator anim;
    private float knockbackForce;


    // GUI
    public GUISkin skin;

    [HideInInspector]
    public int nb_dgtsTot = 0;



    void Start()
    {

        rigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();


        //On ative juste le pistol comme premiere arme

        EquipGun(3, false);
    }

    public void OnGUI()
    {
        if (GameParameters.Instance.paused == false)
        {

                GUI.skin = skin;

                GUI.Label(new Rect(Screen.width - 200, Screen.height - 35, 200, 80), "Ammo : " + currentGun.bulletsLeft, "StyleAmmo");

            
        }
    }

    public void EquipGun(int GunId, bool achat)
    {       
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = equip_gun;
        audio.volume = 0.1f;
        audio.Play();
        //inventory.GetCurrentGun(GunId).SetActive(true);
    }





    void Update()
    {

        this.health = GameParameters.Instance.getHealth();

        if (GameParameters.Instance.PlayerHealth <= 0)
        {
            Die();
        }


    }


    public void TakeDamage(int dmg, Vector3 knock, float force)
    {

        if (Time.time >= dmgtmp)
        {
            if (dmg > 0)
            {
                AudioSource audio = GetComponent<AudioSource>();
                audio.clip = dmg_sound;
                audio.volume = 0.05f;
                audio.Play();
                GameParameters.Instance.PlayerHealth = health - dmg;
                nb_dgtsTot = nb_dgtsTot + dmg;
                dmgtmp = Time.time + dmgDelay;
            }

        }
        
    }



    public void Die()
    {
        //Destroy(gameObject);
        //ANIM DE MORT
        GameParameters.Instance.PlayerSpeed = 0;
        GameParameters.Instance.GunSpeed = 0;
    }




}
