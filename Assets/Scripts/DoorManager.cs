using UnityEngine;
using System.Collections;

public class DoorManager : MonoBehaviour
{ 

    private int DoorHealth = 100;
    public RectTransform healthbar;

    void Start()
    {
        DoorHealth = 100;
    }


    public void TakeDamage(int dmg)
    {
        DoorHealth -= dmg;
        healthbar.sizeDelta = new Vector2(DoorHealth / 2, healthbar.sizeDelta.y);
        CheckHealth();
    }

    void CheckHealth()
    {

        if (DoorHealth <= 0)
        {
            //anim et gameover
            Destroy(gameObject);
        }

    }
}

