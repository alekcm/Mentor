using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    public Transform coinTransform;
    public Transform moveTo;

    private void FixedUpdate()
    {
        if (moveTo != null)
        {
            Vector3 moveVector = (moveTo.position - coinTransform.position).normalized;
            coinTransform.position += moveVector * Time.fixedDeltaTime * 3;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            moveTo = collision.transform;
        }
    }
}
