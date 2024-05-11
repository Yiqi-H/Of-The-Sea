using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootCooldown = 1f;

    private float timeSinceLastShot;
    public GameObject ShootState, AttackState;

    public static PlayerShooting Instance;

    public static class MyEnumClass
    {
        public const string
        Skip = "space",
        Shoot = "j";
    }
    string skip = MyEnumClass.Skip;
    string shoot = MyEnumClass.Shoot;
    public bool CanShoot = false;
    private void Start()
    {
        Instance = this;
        timeSinceLastShot = shootCooldown;
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if (Input.GetKeyDown(shoot) && CanShoot && timeSinceLastShot >= shootCooldown)
        {
            timeSinceLastShot = 0f; // Reset the cooldown timer
            AttackState.SetActive(false);
            ShootState.SetActive(true);
            Invoke("Shoot", 0.5f);
        }

    }

    void Shoot()
    {
        // Instantiate the bullet prefab at the fire point position and rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        if (AudioManager.instance)
        AudioManager.instance.PlayOneShot(FMODEvents.instance.playerShoot, this.transform.position);
    }

    //private void Shoot()
    //{
    //    Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    //}
}
