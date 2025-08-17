using System;
using System.Collections.Generic;
using UnityEngine;

namespace GAS.Runtime
{
    public abstract class TargetCatcherBase
    {
        public AbilitySystemComponent Owner;

        protected TargetCatcherBase()
        {
        }

        public virtual void Init(AbilitySystemComponent owner)
        {
            Owner = owner;
        }

        public void CatchTargets(AbilitySystemComponent mainTarget, List<AbilitySystemComponent> results)
        {
            results.Clear();

            CatchTargetsNonAlloc(mainTarget, results);
        }

        protected abstract void CatchTargetsNonAlloc(AbilitySystemComponent mainTarget, List<AbilitySystemComponent> results);

#if UNITY_EDITOR
        public virtual void OnEditorPreview(GameObject obj)
        {
        }
#endif
    }
}