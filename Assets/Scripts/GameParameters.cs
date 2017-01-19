using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class GameParameters : MonoBehaviour
{
    private static GameParameters instance;
    public static GameParameters Instance
    {
        // si le component n'a pas été ajouté à un objet, ajout d'un GO avec ce component
        get
        {
            return instance ?? (instance = new GameObject("GameParameters_object").AddComponent<GameParameters>());
        }
        // set non fixé : read only variable !
    }

    public bool paused = false;

    [HideInInspector]
    public GameObject[] Zombie;
    [HideInInspector]
    public int Level;
    [HideInInspector]
    public int nEnnemy;
    [HideInInspector]
    public int nEnnemyBoss;
    [HideInInspector]
    public int nEnnemyRestants;
    [HideInInspector]
    public int nEnnemyInstancies;
    [HideInInspector]
    public int nEnnemyTues;
    [HideInInspector]
    public float SpawnDelay;
    [HideInInspector]
    public int Z_dmg;
    [HideInInspector]
    public int Z_Health;
    [HideInInspector]
    public float Z_Speed;



    [HideInInspector]
    public int PlayerPrecision;
    [HideInInspector]
    public int PlayerHealth;
    [HideInInspector]
    public float GunDamage;
    [HideInInspector]
    public int GunSpeed;
    [HideInInspector]
    public int Score;
    [HideInInspector]
    private float SpawnTimer;

    private SpawnManager mSpawnManager;
    public GameObject Player;
    private List<Ennemy_Manager> mZombieList;

    // Text
    public TextMesh ScoreText;
    public TextMesh KillsText;
    public TextMesh LevelText;

    void Start()
    {

        ScoreText = Player.GetComponent<PlayerManager>().ScoreText;
        KillsText = Player.GetComponent<PlayerManager>().KillsText;
        LevelText = Player.GetComponent<PlayerManager>().LevelText;
        SetScore();
        SetKillsText();
        SetLevelText();
        Transform tmpTransform = transform.Find("SpawnPlane");
        if (tmpTransform == null)
        {
            return;
        }

        mSpawnManager = tmpTransform.GetComponent<SpawnManager>();
        SetLevelParameters();
        Level = 1;
        nEnnemyBoss = 0;
        Score = 0;
        SpawnDelay = Random.Range(0, 2f);
        PlayerHealth = 5;
        GunDamage = 1;
        GunSpeed = 500;
        Z_dmg = 1;
        Z_Health = 2;
        Z_Speed = Random.Range(3.5f, 4.1f);
        nEnnemyTues = 0;

    }

    public void CreateEnnemy(GameObject vartype_Zombie, Vector3 varPos, Quaternion varRot)
    {
        if (vartype_Zombie == null)
        {
            return;
        }

        GameObject tmpObject = Instantiate(vartype_Zombie, varPos, varRot) as GameObject;
        Ennemy_Manager tmpScript = tmpObject.GetComponent<Ennemy_Manager>();
        if (tmpScript == null)
        {
            return;
        }
        if (mZombieList == null)
        {
            mZombieList = new List<Ennemy_Manager>();
        }

        mZombieList.Add(tmpScript);

    }




    public void RemoveFromCache(Ennemy_Manager VarZombie)
    {
        if (mZombieList == null)
        {
            return;
        }
        if (VarZombie == null)
        {
            return;
        }
        mZombieList.Remove(VarZombie);
    }


    public void SetScore()
    {
        ScoreText.text  = "Score: " + GameParameters.Instance.Score;
   
    }

    public void SetKillsText()
    {
        KillsText.text = "Kills: " + GameParameters.Instance.nEnnemyTues;
    }

    public void SetLevelText()
    {
        LevelText.text = "Level: " + GameParameters.Instance.Level;
    }

 


    public void SpawnBonus(Vector3 varPos, Quaternion varRot)
    {
        mSpawnManager.SpawnUpgrade(varPos, varRot);
    }


    public void AddPoints(int pointValue)
    {
        Score += pointValue;
        SetScore();
    }



    

    public int getHealth()
    {
        return PlayerHealth;
    }

    public void Awake()
    {
        if (instance)
        {
            Destroy(gameObject); // pas deux instances de ce component!!
        }
        else
        {
            instance = this;
            // TOKNOW attention la fonction DontDestroyOnLoad ne marche pas
            // si l'objet fait partie d'une hiérarchie !!
            DontDestroyOnLoad(gameObject);
            Player = GameObject.FindGameObjectWithTag("Player");
            Zombie = GameObject.FindGameObjectsWithTag("ennemies");
            if (Player == null) ;
            if (Zombie == null) ;


        }
    }

    void Update()
    {
       
    }

    public void SetLevelParameters()
    {

            if (Level < 10)
            {
                nEnnemy = Level + 5;
                SpawnDelay = Random.Range(0.7f, 1.75f);
                
            }

            if (Level == 10)
            {
                nEnnemy = 10;
                nEnnemyBoss = 1;
                Z_Health = 4;
                Z_Speed = Random.Range(3.6f, 4.2f);
                Z_dmg = 2;
            }

            if (Level > 10 && Level < 20)
            {
                nEnnemy = Level + 8;

            }

            if (Level == 20)
            {
                nEnnemy = 30;
                nEnnemyBoss = 2;

                Z_Health = 6;
                Z_Speed = Random.Range(3.8f, 4.42f);
                Z_dmg = 3;
            }

            if (Level > 20 && Level < 30)
            {
                nEnnemy = Level + 15;
            }

            if (Level == 30)
            {
                nEnnemy = 60;
                SpawnDelay = 4.8f;
                nEnnemyBoss = 3;
                Z_Health = 8;
                Z_Speed = Random.Range(4f, 4.6f);
                Z_dmg = 4;
            }

            if (Level > 30 && Level < 40)
            {
                nEnnemy = Level * 2;
                SpawnDelay = Random.Range(0.5f, 1f);
            }

            if (Level == 40)
            {
                nEnnemyBoss = 4;
                nEnnemy = 80;
                SpawnDelay = 3.5f;
                Z_Health = 10;
                Z_Speed = Random.Range(4.2f, 4.8f);
                Z_dmg = 5;
            }

            if (Level > 40 && Level < 50)
            {
                nEnnemy = Level * 3;
            }

            if (Level == 50)
            {
                nEnnemyBoss = 5;
                nEnnemy = 100;
                SpawnDelay = 3f;
                Z_Health = 12;
                Z_Speed = Random.Range(4.4f, 5f);
                Z_dmg = 6;
            }
            if (Level == 51)
            {
                //LOAD MENU DE FIN 

            }
            nEnnemyRestants = nEnnemy + nEnnemyBoss;
        }

        
        

}


