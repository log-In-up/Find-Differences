using UnityEngine;

namespace Assets.Scripts.Logic
{
    [System.Serializable]
    public class PairObject
    {
        //[SerializeField]
        //public GameObject FirstObject, SecondObject;

        [SerializeField] public float start;
        [SerializeField] public float end;
        [SerializeField] public int nextSegment;
    }
}