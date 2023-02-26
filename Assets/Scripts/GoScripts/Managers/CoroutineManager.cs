using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class CoroutineManager : MonoBehaviour
    {
        public static CoroutineManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}