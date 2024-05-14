using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robot : MonoBehaviour
{
    public float move;
    private Rigidbody2D enemy;
    private float startLocation;
    private bool right;


    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
        startLocation= transform.position.x;
        move = 5;//set the speed of enemy
        right = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(right)
        //change x axis velocity while keep y axis velocity unchange
           enemy.velocity = new Vector2(move, enemy.velocity.y);
        else
           enemy.velocity = new Vector2(-move, enemy.velocity.y);

        if (Mathf.Abs(transform.position.x - startLocation) >= 6.2)
        {
            // Change movement direction
            right = !right;
        }




    }
}
