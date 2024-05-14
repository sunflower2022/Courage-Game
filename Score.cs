using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public int point;
    public TextMeshProUGUI pointGui;
    // Start is called before the first frame update
    void Update()
    {
        pointGui.text = point.ToString();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "pointUp")
        {

            point += 10;
            
        }
     }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="robot")
          {
            point -= 10;
          }

        if (collision.gameObject.tag == "sharp")
        {
            point -= 10;

        }

    }
}
