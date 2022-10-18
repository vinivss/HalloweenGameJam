using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.Trees.AI
{
    //skeleton for nodes in dialogue BT
    public abstract class AINode : ScriptableObject
    {

        public enum State
        {
            RUN,
            FAIL,
            SUCC
        }

        [HideInInspector]public State state = State.RUN;
        [HideInInspector] public bool started = false;
        [HideInInspector] public string GUID;
        [HideInInspector] public Vector2 position;
        [HideInInspector] public AIBlackBoard blackboard;
        [HideInInspector] public AIAgent agent;
        [TextArea] public string description;
        public State Update()
        {
            if (!started)
            {
                OnStart();
                started = true;
            }

            state = OnUpdate();

            if (state == State.FAIL || state == State.SUCC)
            {
                OnStop();
                started = false;
            }
            return state;
        }

        public virtual AINode Clone()
        {
            return Instantiate(this);
        }

        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract State OnUpdate();



    }
}
