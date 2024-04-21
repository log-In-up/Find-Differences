using Assets.Scripts.Infrastructure.Services.InputSystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

namespace Assets.Scripts.Logic
{
    public class LevelHandler : MonoBehaviour
    {
        [SerializeField]
        private GameObject _findingEffect;

        [SerializeField]
        private LayerMask _layerMask;

        [SerializeField]
        private List<GameObject> _topObjects, _bottomObjects;

        private InputActionsMap _actionsMap;
        private List<(GameObject, GameObject)> _pair;

        public delegate void FindHandler();

        public event FindHandler OnAllObjectsFind;

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
            _pair = new List<(GameObject, GameObject)>();
        }

        private void CachePairs()
        {
            for (int index = 0; index < _topObjects.Count; index++)
            {
                _pair.Add((_topObjects[index], _bottomObjects[index]));
            }
        }

        private void DisableCollider(GameObject item)
        {
            if (item.TryGetComponent(out Collider collider))
            {
                collider.enabled = false;
            }
        }

        private void DisableUnnecessaryColliders()
        {
            foreach ((GameObject, GameObject) item in _pair)
            {
                if (item.Item1.activeInHierarchy == item.Item2.activeInHierarchy)
                {
                    DisableCollider(item.Item1);
                    DisableCollider(item.Item2);
                }
            }
        }

        private bool FindPair(GameObject objectOnScene, out (GameObject, GameObject) pair)
        {
            foreach ((GameObject, GameObject) item in _pair)
            {
                if (objectOnScene == item.Item1 || objectOnScene == item.Item2)
                {
                    pair = item;
                    return true;
                }
            }

            pair = new(null, null);
            return false;
        }

        private void OnDisable()
        {
            _actionsMap.UI.Click.performed -= TapPerformed;
            _actionsMap.Disable();
        }

        private void OnEnable()
        {
            _actionsMap.Enable();
            _actionsMap.UI.Click.performed += TapPerformed;
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

        private void Start()
        {
            CachePairs();
            DisableUnnecessaryColliders();
        }

        private void TapPerformed(InputAction.CallbackContext callbackContext)
        {
            Vector2 position = _actionsMap.UI.Point.ReadValue<Vector2>();
            Ray ray = Camera.main.ScreenPointToRay(position);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 25.0f, _layerMask);

            if (hit.collider != null && hit.collider.enabled && FindPair(hit.collider.gameObject, out (GameObject, GameObject) pair))
            {
                Instantiate(_findingEffect, pair.Item1.transform.position, Quaternion.identity);
                Instantiate(_findingEffect, pair.Item2.transform.position, Quaternion.identity);

                hit.collider.gameObject.SetActive(false);

                CheckAllItems();
            }
        }

        private void CheckAllItems()
        {
            foreach ((GameObject, GameObject) item in _pair)
            {
                if (item.Item1.activeInHierarchy != item.Item2.activeInHierarchy)
                {
                    return;
                }
            }

            OnAllObjectsFind?.Invoke();
        }
    }
}