using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] TargetSpeed[] platformPath;
    TargetSpeed currentTarget;
    [SerializeField] Ease movingEase;
    [SerializeField] bool moving;
    int compteur = 0;

    private void Start()
    {
        if (platformPath.Length > 0)
        {
            currentTarget = platformPath[0];
            Move();
        }
    }

    private void Move()
    {
        transform.DOMove(currentTarget.target.position, currentTarget.timeToReach).OnComplete(() => { IncrementTarget(); Move(); }).SetEase(movingEase).SetDelay(currentTarget.delay);
    }

    private void IncrementTarget()
    {
        compteur++;
        if (compteur >= platformPath.Length)
        {
            compteur = 0;
        }
        currentTarget = platformPath[compteur];

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}

[System.Serializable]
public class TargetSpeed
{
    public Transform target;
    public float timeToReach;
    public float delay;
}
