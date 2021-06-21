using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("Hit Bullet..... Wall!!");
            Destroy(collision.gameObject, 0.01f);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy..... Wall!!");
            Destroy(collision.gameObject, 0.01f);
        }

    }
}
