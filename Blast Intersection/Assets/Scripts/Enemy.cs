using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed;
    private Transform target;
    public float startHealth;
    public float health;
    public Image healthBar;

    void Start()
    {
       target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
       health = startHealth;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,target.position, speed * Time.deltaTime);  //moves towards player
         healthBar.fillAmount = health/startHealth;
            if (health<=0){ //destroy if no health
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other){ //kills player when collide
        if (other.gameObject.tag == "Player"){
            Destroy (other.gameObject);
            print("Player is Dead");
        }
    }
}
