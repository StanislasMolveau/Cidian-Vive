using UnityEngine;
using System.Collections;

public class Entite : MonoBehaviour
{
    [HideInInspector]
    public float z_health = 3;

    [HideInInspector]
    int pointValue = 10;

    void apparaitre(GameObject objet)
    {
        Instantiate(objet);
    }

    void Update()
    {

    }

    public virtual void TakeDamage(float dmg)
    {

        z_health -= dmg;
        GameParameters.Instance.AddPoints(pointValue);



        if (z_health <= 0)
        {

            Die();


        }
    }


    public virtual void Die()
    {

        GameParameters.Instance.nbrZombiesTuesTotal++;
        GameParameters.Instance.nEnnemyInstancies--;
        GameParameters.Instance.nEnnemyRestants--;

    }

    void FixedUpdate()
    { }

    public virtual void Hit(Vector3 hit_direction)
    {
        if (this.z_health > 0)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + hit_direction.x / 5, gameObject.transform.position.y, gameObject.transform.position.z + hit_direction.z / 3);
        }

    }
}