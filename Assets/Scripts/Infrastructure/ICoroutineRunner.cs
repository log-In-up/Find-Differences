using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);

        void StopCoroutine(string methodName);

        void StopCoroutine(IEnumerator routine);

        void StopCoroutine(Coroutine routine);
    }
}