using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.Trees.AI
{
    [System.Serializable]
    public class AIBlackBoard
    {
        public bool isAlive;
        public Vector3 targetOffset;
        public GameObject player;
        public GameObject objectToThrow;
    }
}
