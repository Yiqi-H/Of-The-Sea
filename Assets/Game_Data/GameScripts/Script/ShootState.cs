using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : MonoBehaviour
{
    public void ResetState()
    {
        
        PlayerShooting.Instance.ShootState.SetActive(false);
        PlayerShooting.Instance.AttackState.SetActive(true);


    }


}
