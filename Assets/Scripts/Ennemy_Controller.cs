using UnityEngine;
using System.Collections;

public class Ennemy_Controller : MonoBehaviour
{

    public float z_speed;
    UnityEngine.AI.NavMeshAgent nav;
    Transform player;
    GameObject zombie;
    Animator controler;
    private bool isStopped = false;
    public bool isBoss = false;
    private float STimer = 0;
    //public GameObject skin;

    public AudioClip[] Taper_player;

    void Start()
    {

        //StartCoroutine(AutoSeekPlayer());
        if (isBoss == false)
        {
            z_speed = GameParameters.Instance.Z_Speed;
        }
        else
        {
            z_speed = GameParameters.Instance.Z_Speed * 1.1f;
        }



    }


    // Use this for initialization
    void Awake()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        //controler = skin.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (this.GetComponent<Ennemy_Manager>().z_health > 0)
        //{
            STimer += 1.0f * Time.deltaTime;

            if (STimer > 0.5f)
            {
                if (nav != null)
                   
                    nav.SetDestination(player.position);

                STimer = 0;

            }
        //}
    }


    IEnumerator Zombie_Attack()
    {

        controler.SetFloat("attack", 1);

        yield return new WaitForSeconds(0.1f);

        controler.SetFloat("attack", 0);

    }

    void OnTriggerEnter(Collider col)
    {
        if (this.GetComponent<Ennemy_Manager>().z_health >= 0)
        {
            Vector3 dir = col.transform.position - transform.position;
            dir.y = -0.50f;
            int rdm = Random.Range(0, 2);
            if (col.gameObject.GetComponent<DoorManager>())
            {
                StartCoroutine("Zombie_Attack");

                AudioSource audio = GetComponent<AudioSource>();
                audio.clip = Taper_player[rdm];
                audio.volume = 0.008f;
                audio.Play();
                audio.loop = false;

            }
        }


    }




}
