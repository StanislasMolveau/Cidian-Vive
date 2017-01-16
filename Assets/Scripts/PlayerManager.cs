using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {



    public EquipGun CtrlLeft;
    public EquipGun CtrlRight;

    public GameObject pistol;
    public GameObject mp7;
    public GameObject LaserPistol;

    public AudioClip equip_gun;

    // Use this for initialization
    void Start () {
        EquipWeapon(2);
	}

    public void EquipWeapon(int gunID)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = equip_gun;
        audio.volume = 0.1f;
        audio.Play();
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
	
	// Update is called once per frame
	void Update () {
	
	}
}
