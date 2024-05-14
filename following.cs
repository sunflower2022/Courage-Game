using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class following : MonoBehaviour
{
    private Vector3 current;
    private Transform player;
    [SerializeField]
    private float leftX, rightX;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        current = transform.position;//store camera position
        current.x = player.position.x;//change current x position to player's x position
      // let the scamera stay in the scene
        if(current.x<leftX)
           current.x = leftX;
        if (current.x > rightX)
            current.x = rightX;

        //let camera following player

        transform.position = current;

    }
}
