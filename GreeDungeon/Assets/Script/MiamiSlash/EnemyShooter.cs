using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shootCD = 5;
    private float timer;

    
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(PlayerSlash.Instance.transform);
        if (!GameStates.Instance.gameStarted) return;
        if (timer < shootCD) timer += Time.deltaTime;
        else Shoot();

    }

    void Shoot()
    {
        timer = 0;
        var obj = Instantiate(bulletPrefab, transform.position, transform.rotation);
        obj.transform.LookAt(PlayerSlash.Instance.transform);
    }
}
