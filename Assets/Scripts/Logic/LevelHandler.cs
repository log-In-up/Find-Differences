using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class LevelHandler : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> _topObjects, _bottomObjects;

        internal void SetBottomDifferences(int[] bottomImages)
        {
            SetObjectsToBeHidden(_bottomObjects, bottomImages);
        }

        internal void SetTopDifferences(int[] topImages)
        {
            SetObjectsToBeHidden(_topObjects, topImages);
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
    }
}