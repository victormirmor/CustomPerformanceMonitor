using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotacion : MonoBehaviour{

    public Vector2 rotationSpeed;
    

    private void Update()
    {

        Vector3 Rotation=new Vector3(0f,rotationSpeed.x,rotationSpeed.y);

        transform.Rotate(Rotation * Time.deltaTime);
    }

}
