using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharactersMovement
{
    public void PlayerMov()
    {
        Vector3 aimPosition = this.direction;
        //aimPosition.y = this.transform.position.y;
        Rotation(aimPosition);

    }
}