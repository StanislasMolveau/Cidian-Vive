using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SpawnManager : MonoBehaviour
{

    public Transform[] SpawnPoints;
    public GameObject EnnemyBoss;
    public GameObject EnnemyV1;
    public GameObject EnnemyV2;
    public GameObject[] Bonus;



    void StartLevel()
    {

        GameParameters.Instance.nEnnemyInstancies = 0;
        GameParameters.Instance.SetLevelParameters();


    }

    IEnumerator WaitForSpawn()
    {

        while (true)
        {
            if (GameParameters.Instance.nEnnemyRestants >= 0)
            {

                if (GameParameters.Instance.nEnnemyInstancies <= 25)
                {


                    yield return new WaitForSeconds(GameParameters.Instance.SpawnDelay);
                    Invoke("Spawn", GameParameters.Instance.SpawnDelay);

                }




            }



        }

    }

    void Update()
    {

        
    }

    void Spawn()
    {

        if (GameParameters.Instance.nEnnemyRestants != 0)
        {
            if (GameParameters.Instance.nEnnemyInstancies <= 25)
            {
                int SpawnPointIndex = Random.Range(0, SpawnPoints.Length);

              
                if (GameParameters.Instance.nEnnemyBoss > 0)
                {
                    GameParameters.Instance.CreateEnnemy(EnnemyBoss, SpawnPoints[SpawnPointIndex].position, SpawnPoints[SpawnPointIndex].rotation);
                    GameParameters.Instance.nEnnemyInstancies++;
                    GameParameters.Instance.nEnnemyRestants--;
                }
                int type_Ennemy = Random.Range(0, 4); // POUR LES DIFFERENTS TYPE DE Ennemy

                switch (type_Ennemy)
                {
                    case 0:
                    case 2:
                    case 3:
                        {

                            GameParameters.Instance.CreateEnnemy(EnnemyV1, SpawnPoints[SpawnPointIndex].position, SpawnPoints[SpawnPointIndex].rotation);
                            GameParameters.Instance.nEnnemyInstancies++;
                            GameParameters.Instance.nEnnemyRestants--;


                        }
                        break;
                    case 1:
                        {
                            GameParameters.Instance.CreateEnnemy(EnnemyV2, SpawnPoints[SpawnPointIndex].position, SpawnPoints[SpawnPointIndex].rotation);
                            GameParameters.Instance.nEnnemyInstancies++;
                            GameParameters.Instance.nEnnemyRestants--;

                        }
                       break;
                }
            }
        }
    }




    void FixedUpdate()
    {
        if (GameParameters.Instance.nEnnemyInstancies <= 0 && GameParameters.Instance.nEnnemyRestants <= 0)
        {

            Debug.Log("EndOfLevel " + GameParameters.Instance.Level);
            Endlevel();
        }

    }

    void Endlevel()
    {

        GameParameters.Instance.Level++;

        if (GameParameters.Instance.Level == 51)
        {
            Application.LoadLevel("Credits");
        }
        StartLevel();
        GameParameters.Instance.SetLevelText();
    }




    Dictionary<GameObject, int> mUpgradeDic;

    private void initdc()
    {
        if (mUpgradeDic != null)
        {
            return;
        }
        mUpgradeDic = new Dictionary<GameObject, int>();
        for (int i = 0; i <= Bonus.Length - 1; i++)
        {
            mUpgradeDic.Add(Bonus[i], 0);

        }

    }

    List<GameObject> temneedPlus = new List<GameObject>();

    public void SpawnUpgrade(Vector3 VarPos, Quaternion varRot)
    {
        initdc();
        int ChoixUp = Random.Range(0, 5);//0-4;

        if (ChoixUp >= 3)
        {
            return;
        }

        Dictionary<GameObject, int>.Enumerator tempEnum = mUpgradeDic.GetEnumerator();
        for (int i = 0; i < mUpgradeDic.Count; i++)
        {
            tempEnum.MoveNext();

            KeyValuePair<GameObject, int> tempkvp = tempEnum.Current;

            if (tempkvp.Value < 6 && ChoixUp < 5)
            {
                temneedPlus.Add(tempkvp.Key);
            }
        }

        int tempRandomIndex = Random.Range(0, temneedPlus.Count);
        Instantiate(temneedPlus[tempRandomIndex], VarPos, varRot);
        mUpgradeDic[temneedPlus[tempRandomIndex]]++;
        //tempRandomIndex++;

    }

    void Start()
    {

        StartLevel();
        StartCoroutine(WaitForSpawn());
    }
}