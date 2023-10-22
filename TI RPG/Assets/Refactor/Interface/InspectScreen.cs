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
            defaultMesh = content.GetComponent<MeshFilter>().sharedMesh;
            Item.onItemInspect += ChangeMesh;
        }

        private void OnDisable()
        {
            Item.onItemInspect -= ChangeMesh;
        }

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