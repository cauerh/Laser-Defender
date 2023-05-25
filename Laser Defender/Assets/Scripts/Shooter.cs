using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 5f;
    [SerializeField] float basefiringRate = 0.2f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFirerate = 1f;


    [HideInInspector]public bool isFiring;

    Coroutine firingCourontine;

    void Start()
    {
        if(useAI)
        {
            isFiring = true;
        }
    }


    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && firingCourontine == null)
        {
            firingCourontine = StartCoroutine(FireContinuosly());
        }
        else if(!isFiring && firingCourontine != null)
        {
            StopCoroutine(firingCourontine);
            firingCourontine = null;
        }

    }

    IEnumerator FireContinuosly()
    {
        while (true)
        {
            GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }

            Destroy(instance, projectileLifeTime);

            float timeToNextProjectile = Random.Range(basefiringRate - firingRateVariance, basefiringRate + firingRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFirerate, float.MaxValue);

            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }
}
