using Assets.Scripts.Infrastructure.Services.InputSystem;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class LevelHandler : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _layerMask;

        [SerializeField]
        private List<GameObject> _topObjects, _bottomObjects;

        private InputActionsMap _actionsMap;

        internal void SetBottomDifferences(int[] bottomImages)
        {
            SetObjectsToBeHidden(_bottomObjects, bottomImages);
        }

        internal void SetTopDifferences(int[] topImages)
        {
            SetObjectsToBeHidden(_topObjects, topImages);
        }

        private void Awake()
        {
            _actionsMap = new InputActionsMap();
        }

        private void OnDisable()
        {
            _actionsMap.UI.Click.performed -= TapPerformed;
            _actionsMap.Disable();
        }

        private void OnEnable()
        {
            _actionsMap.Enable();
            _actionsMap.UI.Click.started += TapPerformed;
        }

        private void SetObjectsToBeHidden(List<GameObject> gameObjects, int[] images)
        {
            for (int indexA = 0; indexA < gameObjects.Count; indexA++)
            {
                for (int indexB = 0; indexB < images.Length; indexB++)
                {
                    if (indexA == images[indexB])
                    {
                        gameObjects[indexA].SetActive(true);
                        break;
                    }
                }
            }
        }

        private void TapPerformed(InputAction.CallbackContext callbackContext)
        {
            Vector2 position = _actionsMap.UI.Point.ReadValue<Vector2>();
            Ray ray = Camera.main.ScreenPointToRay(position);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 25.0f, _layerMask);

            if (hit.collider != null)
            {
                Debug.LogWarning("Hit!");
            }
            else
            {
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100.0f, Color.red, 5.0f);
            }
        }
    }
}