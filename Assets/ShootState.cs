using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : MonoBehaviour
{
    public void ResetState()
    {
        
        PlayerMovement.Instance.ShootState.SetActive(false);
        PlayerMovement.Instance.AttackState.SetActive(true);


    }


}
