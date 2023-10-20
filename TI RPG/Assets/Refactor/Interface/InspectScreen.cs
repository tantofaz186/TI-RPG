using System;
using UnityEngine;

namespace Rpg.Interface
{
    public class InspectScreen : MonoBehaviour
    {
        public Transform gimbal;
        public Transform content;
        
        public void Update()
        {
            Vector2 vec = 5 * new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            if (Input.GetMouseButton(0))
            {
                gimbal.localRotation *= Quaternion.Euler(vec.y, -vec.x, 0);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Quaternion now = content.rotation;
                gimbal.localRotation = Quaternion.identity;
                content.rotation = now;
            }
        }
    }
}