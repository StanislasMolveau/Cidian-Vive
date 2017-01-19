using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {



    public EquipGun CtrlLeft;
    public EquipGun CtrlRight;

    public GameObject pistol;
    public GameObject mp7;
    public GameObject LaserPistol;

    public AudioClip equip_gun;

    public Image currentGunImage;

    public Sprite pistolImage;
    public Sprite mp7Image;
    public Sprite LaserImage;

    public TextMesh ScoreText;
    public TextMesh KillsText;
    public TextMesh LevelText;
    public Text BulletsLeft;
    // Use this for initialization
    void Start () {
        //Equipez une arme suivant son ID
        EquipWeapon(1);
        
    }

    public void SetBulletText()
    {
        int bullet1;
        int bullet2;
        if (CtrlLeft.handHold.childCount == 0)
        {
            bullet1 = 0;
        }
        else
        {
            bullet1 = CtrlLeft.GetComponentInChildren<GunController>().bulletsLeft;
        }
        if (CtrlRight.handHold.childCount == 0)
        {
            bullet2 = 0;
        }
        else
        {
            bullet2 = CtrlRight.GetComponentInChildren<GunController>().bulletsLeft;
        }
        int TotBullet = bullet1 + bullet2;
        BulletsLeft.text = "" + TotBullet;
    }

    void Update()
    {
        if (CtrlLeft.handHold.childCount == 0 &&
            CtrlRight.handHold.childCount == 0)// Regarder si mains sont vides
        {

            EquipWeapon(1);
        }
    }

    
    public void ChangeSpriteGun(int gunID)
    {
        switch (gunID)
        {
            case 1:
                currentGunImage.sprite = pistolImage;
                break;
            case 2:
                currentGunImage.sprite = mp7Image;
                break;
            case 3:
                currentGunImage.sprite = LaserImage;
                break;
        }
    }

    public void EquipWeapon(int gunID)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = equip_gun;
        audio.volume = 0.1f;
        audio.Play();
        ChangeSpriteGun(gunID);
        switch (gunID)
        {

            case 1:
                
                CtrlLeft.Equip(pistol);
                CtrlRight.Equip(pistol);
                break;
            case 2:

                CtrlLeft.Equip(mp7);
                CtrlRight.Equip(mp7);
                break;
            case 3:
                CtrlLeft.Equip(LaserPistol);
                CtrlRight.Equip(LaserPistol);
                break;
        }
    }
	
}
