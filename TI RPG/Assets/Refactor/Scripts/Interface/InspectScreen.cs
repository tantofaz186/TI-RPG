using System;
using System.Security.Cryptography;
using Controllers;
using Rpg.Crafting;
using UnityEditor;
using UnityEngine;

namespace Rpg.Interface
{
    public class InspectScreen : MonoBehaviour
    {
        [Header("REFERNECES")]
        public Transform gimbal;
        public Camera inspectCamera;
        
        [Header("STATE")]
        public Vector2 rotateSpeed = Vector2.zero;
        public float scaleSpeed = 0f;
        
        [Header("SETTINGS")]
        public float minScale = 0.75f;
        public float maxScale = 1.5f;
        public float defaultScaleFactor = 0.5f;
        
        private void Awake()
        {
            SetOpen(false);
        }
        
        public void InspectItem(Item item)
        {
            SetOpen(true);
            if (gimbal.childCount > 0)
                Destroy(gimbal.GetChild(0).gameObject);

            GameObject go = Instantiate(item.inspectPrefab, gimbal.transform);
            go.transform.localScale *= defaultScaleFactor;
        }

        public void SetOpen(bool open)
        {
            inspectCamera.gameObject.SetActive(open);
            enabled = open;
            gimbal.gameObject.SetActive(open);
            gimbal.localScale = Vector3.one;
        }
        
        public void Update()
        {
            float deltaTime = Time.deltaTime;

            #region Zoom
            float msw = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(msw) > 0.01f)
            {
                scaleSpeed = Mathf.Lerp(scaleSpeed, msw, deltaTime * 8f);
            }
            else
                scaleSpeed = Mathf.Lerp(scaleSpeed, 0f, deltaTime * 8f);
            
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
        
#if UNITY_EDITOR
        [CustomEditor(typeof(InspectScreen))]
        public class InspectScreenEditor : Editor
        {
            public Item testItem;
            
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                testItem = (Item) EditorGUILayout.ObjectField("Item", testItem, typeof(Item), false);
                if (GUILayout.Button("Test Inspect"))
                {
                    InspectScreen screen = (target as InspectScreen)!;
                    screen.InspectItem(testItem);
                }
            }
        }
#endif
    }
}