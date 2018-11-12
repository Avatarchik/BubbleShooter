using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Test
{
    public class ComponentWithOnDestroy : MonoBehaviour
    {
        void Start()
        {
            StartCoroutine(TestCoroutine(10));
        }

        private IEnumerator TestCoroutine(int delaySeconds)
        {
            yield return new WaitForSeconds(delaySeconds);
            Debug.Log("TestCoroutine trigger");
        }

        void OnDestroy()
        {
            Debug.Log("ComponentWithOnDestroy OnDestroy trigger");
        }
    }
}