using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharactersMovement
{
    public void PlayerMov(LayerMask layerMask, RaycastHit hit)
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.magenta);

        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            Vector3 aimPosition = hit.point - transform.position;
            aimPosition.y = this.transform.position.y;
            Rotation(aimPosition);
        }
    }
}