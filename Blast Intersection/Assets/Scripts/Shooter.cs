using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{

    public int Damage = 10;
    public float reloadTime;
    public bool canShoot;
    public int score;

    public SpriteRenderer reloadIndicator;

    public GameObject Laser;

    public Text scoreboard;

    RaycastHit2D hit;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        canShoot = true;
        updateScore();
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.W) ) // press up or W
        {
            print("Up was pressed");
            fire(0);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) ) //Press down or S
        {
            print("Down was pressed");
            fire(1);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) ) //Press left or A
        {
            print("Left was pressed");
            fire(2);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) ) //Press right or D
        {
            print("Right was pressed");
            fire(3);
        }
    }

    void fire (int direction)
    {

        //set ray to 0 direction to begin
        hit = Physics2D.Raycast(transform.position, Vector2.zero);
        
        //below determines the direction of the Ray. Each direction is given a number associated with it
        if (direction == 0)
        {
            hit = Physics2D.Raycast( new Vector2(transform.position.x, transform.position.y) + new Vector2(0,(float).5), Vector2.up);
        }
        else if (direction == 1)
        {
            hit = Physics2D.Raycast( new Vector2(transform.position.x, transform.position.y) + new Vector2(0,-(float).5), Vector2.down);
        }
        else if (direction == 2)
        {
            hit = Physics2D.Raycast( new Vector2(transform.position.x, transform.position.y) + new Vector2(-(float).5, 0), Vector2.left);
        }
        else if (direction == 3)
        {
            hit = Physics2D.Raycast( new Vector2(transform.position.x, transform.position.y) + new Vector2((float).5, 0), Vector2.right);
        }


        //This part determines if the ray hits them and kills them if it do
        if ( (canShoot == true) && (hit.collider != null) && (hit.collider.gameObject.tag == "Enemy" ) )
        {
            print("Enemy was Hit");
            onEnemyHit(hit.collider.gameObject);
        }
        
    }

    void onEnemyHit(GameObject target) // Damage Enemy and Give a Score
    {

        Enemy enemyController = target.GetComponent<Enemy>();

        enemyController.health -= Damage;
        if(enemyController.health <= 0)
        {
            score += 1;
        }
        updateScore();
        StartCoroutine(reload(target));
    }

    IEnumerator reload (GameObject target) // the Reload mechanics and Draw the laser
    {
        canShoot = false;
        reloadIndicator.color = Color.red;

        Vector2 middlepoint = new Vector2( (transform.position.x + target.transform.position.x)/2 , (transform.position.y + target.transform.position.y)/2 );

        GameObject Line = Instantiate (Laser, middlepoint, Quaternion.identity);
        Line.transform.localScale = new Vector2( ((transform.position.x - target.transform.position.x) * 6.6f) + 1 , ((transform.position.y + target.transform.position.y) * 6.6f) +1 );

        yield return new WaitForSeconds (0.03f);

        Destroy(Line);

        yield return new WaitForSeconds (reloadTime);

        canShoot = true;
        reloadIndicator.color = Color.green;
    }

    void updateScore()
    {
        scoreboard.text = "Score: " + score;
    }
}
