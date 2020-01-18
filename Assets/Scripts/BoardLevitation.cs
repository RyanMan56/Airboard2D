using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardLevitation : MonoBehaviour
{
    private float L0 = 1.0f;
    private Rigidbody rb;
    private BoxCollider boxCollider;
    private float k = -88.68f;
    private float zeta = 0.3f;
    public Rigidbody board;
    private int springsInParallel = 2;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        k /= springsInParallel;
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
        Vector3 adjustedPos = new Vector3(transform.position.x, transform.position.y - boxCollider.bounds.extents.y, transform.position.z);

        LayerMask mask = LayerMask.GetMask("Ground");

        Physics.Raycast(adjustedPos, -Vector3.up, out RaycastHit hit, 0.75f, mask);
        if (hit.collider != null)
        {
            float L = Mathf.Abs(hit.point.y - adjustedPos.y);
            float x = L0 - L;

            float c = zeta * 2 * Mathf.Sqrt(board.mass * -k);
            Vector2 force = Vector3.up * (-k * x - c * rb.velocity.y);

            Debug.DrawRay(adjustedPos, -Vector3.up * L);
            Debug.Log("F = " + force.y + " L = " + L + " L0 = " + L0 + " x = " + x + " c = " + c);

            //rb.AddForce(force); to levitate the board directly upwards instead of in the direction of the thrusters
            rb.AddRelativeForce(force);
        }
    }
}
