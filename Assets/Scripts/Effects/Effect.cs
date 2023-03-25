using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public class Effect : MonoBehaviour
    {
        public bool IsLooping;
        private Animator myAnimator;

        public void Awake()
        {
            if (!IsLooping)
            {
                myAnimator = GetComponent<Animator>();
                AnimationClip animation = myAnimator.runtimeAnimatorController.animationClips.First();
                AnimationEvent animationEndEvent = new AnimationEvent();
                animationEndEvent.time = animation.length;
                animationEndEvent.functionName = "AnimationCompleteHandler";
                animationEndEvent.stringParameter = animation.name;

                animation.AddEvent(animationEndEvent);
            }
        }

        public void AnimationCompleteHandler(string name)
        {
            Destroy(gameObject);
        }
    }
}
