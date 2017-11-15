using System;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames
{
    public class UpdateObserver : MonoBehaviour
    {
        public enum UpdateType
        {
            FixedUpdate,
            Update,
            LateUpdate
        }

        public static bool hideGameObject = true;

        private event Action fixedUpdateTarget;
        private event Action updateTarget;
        private event Action lateUpdateTarget;

        public void Register(Action action, UpdateType updateType)
        {
            switch (updateType)
            {
                case UpdateType.FixedUpdate:
                    fixedUpdateTarget += action;
                    return;
                case UpdateType.Update:
                    updateTarget += action;
                    return;
                case UpdateType.LateUpdate:
                    lateUpdateTarget += action;
                    return;
            }
        }

        public void UnRegister(Action action, UpdateType updateType)
        {
            switch (updateType)
            {
                case UpdateType.FixedUpdate:
                    fixedUpdateTarget -= action;
                    return;
                case UpdateType.Update:
                    updateTarget -= action;
                    return;
                case UpdateType.LateUpdate:
                    lateUpdateTarget -= action;
                    return;
            }
        }

        void FixedUpdate()
        {
            DoUpdating(fixedUpdateTarget);
        }

        void Update()
        {
            DoUpdating(updateTarget);
        }

        void LateUpdate()
        {
            DoUpdating(lateUpdateTarget);
        }

        void DoUpdating(Action currentEvent)
        {
            if (currentEvent != null)
            {
                currentEvent();
            }
        }

        void OnDestroy()
        {
            RemoveAll(fixedUpdateTarget);
            RemoveAll(updateTarget);
            RemoveAll(lateUpdateTarget);
        }

        void RemoveAll(Action deadEvent)
        {
            if (deadEvent != null)
            {
                Delegate[] clientList = deadEvent.GetInvocationList();
                foreach (Delegate d in clientList)
                {
                    deadEvent -= (d as Action);
                }
            }
        }
    }
}

