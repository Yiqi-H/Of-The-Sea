using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field:Header("Coin SFX")]
    [field:SerializeField] public EventReference coinCollected{get;private set;}

    [field:Header("Shoot SFX")]
    [field:SerializeField] public EventReference playerShoot{get;private set;}

    [field:Header("Death SFX")]
    [field:SerializeField] public EventReference playerDeath{get;private set;}

    [field:Header("Door SFX")]
    [field:SerializeField] public EventReference doorOpen{get;private set;}

    [field:Header("Enemy SFX")]
    [field:SerializeField] public EventReference enemyHit{get;private set;}

    [field:Header("Music")]
    [field:SerializeField] public EventReference music{get;private set;}

    public static FMODEvents instance{get;private set;}

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found more than one FMOD Events instance ");
        }

        instance = this;
    }
}
