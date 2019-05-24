using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicalKit : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject,5f);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player.hp < player.status.initialHP)
            {
                player.Heal(15);
                Destroy(this.gameObject);
            }
        }
    }
}
