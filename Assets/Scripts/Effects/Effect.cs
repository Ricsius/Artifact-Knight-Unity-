
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public class Effect : MonoBehaviour
    {
        public bool IsLooping;
        private Animator _animator;

        public void Awake()
        {
            if (!IsLooping)
            {
                _animator = GetComponent<Animator>();
                AnimationClip animation = _animator.runtimeAnimatorController.animationClips.First();
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
