using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour
{

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, 4, 0 * Time.deltaTime);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    
}
