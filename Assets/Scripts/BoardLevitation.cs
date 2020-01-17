using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardLevitation : MonoBehaviour
{
    private float L0 = 1.0f;
    private Rigidbody2D rb2D;
    private float k = -88.68f;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        CalcForce(L0);    
    }

    void CalcForce(float L0)
    {
        Vector3 adjustedPos = new Vector2(transform.position.x, transform.position.y - 0.4038926f / 2);

        LayerMask mask = LayerMask.GetMask("Ground");

        RaycastHit2D hit = Physics2D.Raycast(adjustedPos, -Vector2.up, 0.75f, mask);
        if (hit.collider != null)
        {
            float L = Mathf.Abs(hit.point.y - adjustedPos.y);
            float x = L0 - L;

            Debug.DrawRay(adjustedPos, -Vector2.up * L);

            Vector2 force = Vector3.up * (-k * x);
            Debug.Log("F = " + force.y + " L = " + L + " L0 = " + L0 + " x = " + x);

            if (force.y > 0)
            {
                rb2D.AddForce(force);
            }
        }
    }
}
