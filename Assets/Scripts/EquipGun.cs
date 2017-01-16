using UnityEngine;
using System.Collections;



public class EquipGun : MonoBehaviour
{
    
    public Transform handHold;
    private GameObject currentGun;
    void Start()
    {

    }

    public void UnEquip()
    {
        Destroy(currentGun);
    }

    public void Equip(GameObject tmpGun)
    {

        UnEquip();
        tmpGun.GetComponent<GunController>().controllerObject = this.gameObject;
        
        Instantiate(tmpGun, handHold);
        
        currentGun = tmpGun;
    }

    void Update()
    {

    }

}
