using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public GameManager gameManager;
    public float forcePower;
    public Vector2 moveVector;
    List<Vector3> playerPos = new List<Vector3>();
    public LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameStarted)
        {
            if (Input.GetKey(KeyCode.Space))
                moveVector = Vector2.up;
            else
                moveVector = Vector2.down;
        }
        
    }
    private void FixedUpdate()
    {
        rigidbody.AddForce(moveVector * forcePower);
        playerPos.Add(transform.position);
        if (playerPos.Count > 50)
            playerPos.RemoveAt(0);
        lineRenderer.SetVertexCount(playerPos.Count);
        for (int i = 0; i < playerPos.Count; i++)
        {
            //Change the postion of the lines
            lineRenderer.SetPosition(i, playerPos[i]);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Wall")
        {
            gameManager.RestartGame();
        }
        if (collision.tag == "Coin")
        {
            Destroy(collision.gameObject);
            gameManager.TakeCoin();
        }
    }
}
