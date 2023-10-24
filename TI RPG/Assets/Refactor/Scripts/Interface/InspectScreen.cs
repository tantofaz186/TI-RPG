using System;
using Rpg.Crafting;
using UnityEditor;
using UnityEngine;

namespace Rpg.Interface
{
    public class InspectScreen : MonoBehaviour
    {
        public Transform gimbal;
        public Transform content;
        private Mesh itemMesh;
        
        public Vector2 rotateSpeed = Vector2.zero;
        public float scaleSpeed = 0f;
        public float minScale = 0.75f;
        public float maxScale = 1.5f;
        
        [SerializeField] private Mesh testMesh;
        [SerializeField] private Mesh testMesh2;
        private Mesh defaultMesh;
        
        
        public Mesh ItemMesh
        {
            get => itemMesh;
            private set
            {
                itemMesh = value;
                content.GetComponent<MeshFilter>().mesh = value;
            }
        }
        
        private void OnEnable()
        {
            gimbal.localScale = Vector3.one;
            defaultMesh = content.GetComponent<MeshFilter>().sharedMesh;
            Item.onItemInspect += ChangeMesh;
        }

        private void OnDisable()
        {
            Item.onItemInspect -= ChangeMesh;
        }
        public void Update()
        {
            float deltaTime = Time.deltaTime;

            #region Zoom
            float msw = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(msw) > 0.01f)
            {
                scaleSpeed = Mathf.Lerp(scaleSpeed, msw, deltaTime * 8);
            }
            else
                scaleSpeed = Mathf.Lerp(scaleSpeed, 0, deltaTime * 16f);
            
            float f = gimbal.localScale.x + scaleSpeed;
            gimbal.localScale = Vector3.one * Mathf.Clamp(f, minScale, maxScale);
            #endregion

            #region Rotation
            if (Input.GetMouseButton(0))
            {
                rotateSpeed = 5 * new Vector2(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"));
                gimbal.Rotate(rotateSpeed, Space.World);
            }
            else
            {
                gimbal.Rotate(rotateSpeed, Space.World);
                rotateSpeed = Vector2.Lerp(rotateSpeed, Vector2.zero, deltaTime * 16f);
            }
            #endregion
        }

        public void ChangeMesh(Mesh _mesh)
        {
            ItemMesh = _mesh;
        }


#if UNITY_EDITOR
        [CustomEditor(typeof(InspectScreen))]
        public class InspectScreenEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                if (GUILayout.Button("Change Mesh to first test mesh"))
                {
                    InspectScreen screen = (target as InspectScreen)!;
                    screen.ChangeMesh(screen.testMesh);
                }
                    
                if (GUILayout.Button("Change Mesh to second test mesh"))
                {
                    InspectScreen screen = (target as InspectScreen)!;
                    screen.ChangeMesh(screen.testMesh2);
                }
                if (GUILayout.Button("Reset mesh"))
                {
                    InspectScreen screen = (target as InspectScreen)!;
                    screen.ChangeMesh(screen.defaultMesh);
                }
            }
        }
#endif
    }
}