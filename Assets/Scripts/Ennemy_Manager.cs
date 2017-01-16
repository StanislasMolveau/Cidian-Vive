using UnityEngine;
using System.Collections;
public class Ennemy_Manager : Entite
{

    private int zombies_damage = 1;
    private Animator anim;
    float timeToDestroy = 0.5f;
    public GameObject skin;
    private Transform myTransform;
    private int DeathpointValue = 100;
    public Rigidbody rb;
    public int zombie_ID;
    private float speed;
    private bool dead;

    void Update()
    {
        //TEMPS AVANT MORT DU ZOMBIE
        //if (dead == true)
        //{
        //    this.zombies_damage = 0;
        //    rb = null;
        //    timeToDestroy -= Time.deltaTime;

        //    if (timeToDestroy <= 0.0f)
        //    {

        //        Destroy(gameObject);
        //    }

        //}
    }

    void Start()
    {
        speed = this.GetComponent<Ennemy_Controller>().z_speed;
        if (zombie_ID == 1)
        {
            z_health = GameParameters.Instance.Z_Health;
            zombies_damage = GameParameters.Instance.Z_dmg;
        }
        if (zombie_ID == 2)
        {
            z_health = GameParameters.Instance.Z_Health * 2;
            zombies_damage = GameParameters.Instance.Z_dmg;
        }
        if (zombie_ID == 3)
        {
            z_health = GameParameters.Instance.Z_Health * 5;
            zombies_damage = GameParameters.Instance.Z_dmg * 2;
        }

        rb = GetComponent<Rigidbody>();
    }


    public override void Die()
    {
        //if (GameParameters.Instance.nEnnemyRestants == 1)
        //{

        //    Vector3 pos = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y + 1.7f, this.transform.localPosition.z);
        //    GameParameters.Instance.SpawnBonus(pos, this.transform.rotation);


        //}


        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GameParameters.Instance.AddPoints(DeathpointValue);
        //this.GetComponent<BoxCollider>().enabled = false;
        GameParameters.Instance.RemoveFromCache(this);
        dead = true;
        base.Die();
        
        

    }




    void OnTriggerEnter(Collider col)
    {
        Vector3 dir = col.transform.position - transform.position;

        if (col.gameObject.GetComponent<DoorManager>())
        {

            col.gameObject.GetComponent<DoorManager>().TakeDamage(zombies_damage);

        }
    }

    void Awake()
    {

        anim = skin.GetComponent<Animator>();
        myTransform = transform;
    }



}


