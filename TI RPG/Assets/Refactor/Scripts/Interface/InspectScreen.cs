using Controllers;
using Rpg.Crafting;
using TMPro;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Rpg.Interface
{
    public class InspectScreen : Singleton<InspectScreen>
    {
        [Header("REFERNECES")]
        public Transform gimbal;
        public Camera inspectCamera;
        public TMP_Text labelItemName;
        
        [Header("STATE")]
        public Quaternion rotateSpeed = Quaternion.identity;
        public float scaleSpeed = 0f;
        public float inactiveTime = 0f;
        
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

            labelItemName.text = item.displayName;
        }

        public void SetOpen(bool open)
        {
            inspectCamera.gameObject.SetActive(open);
            enabled = open;
            Time.timeScale = open ? 0f : 1f;
            gimbal.gameObject.SetActive(open);
            gimbal.localScale = Vector3.one;
        }
        
        public void Update()
        {
            float deltaTime = Time.unscaledDeltaTime;

            if (inactiveTime >= 3f)
                ResetState();

            #region Zoom
            float msw = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(msw) > 0.01f)
            {
                scaleSpeed = Mathf.Lerp(scaleSpeed, msw, deltaTime * 8f);
                inactiveTime = 0;
            }
            else
                scaleSpeed = Mathf.Lerp(scaleSpeed, 0f, deltaTime * 8f);
            
            float f = gimbal.localScale.x + scaleSpeed;
            gimbal.localScale = Vector3.one * Mathf.Clamp(f, minScale, maxScale);
            #endregion

            #region Rotation
            if (Input.GetMouseButton(0))
            {
                Vector2 rot = 5 * new Vector2(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"));
                Quaternion pre = gimbal.rotation;
                gimbal.Rotate(rot, Space.World);
                rotateSpeed = Quaternion.Inverse(pre) * gimbal.rotation;
                inactiveTime = 0;
            }
            else
            {
                gimbal.rotation *= rotateSpeed;
                rotateSpeed = Quaternion.Lerp(rotateSpeed, Quaternion.identity, deltaTime * 16f);
            }
            #endregion

            inactiveTime += deltaTime;
        }

        public void ResetState()
        {
            rotateSpeed = Quaternion.Slerp(Quaternion.identity, Quaternion.Inverse(gimbal.rotation), 1/512F);
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