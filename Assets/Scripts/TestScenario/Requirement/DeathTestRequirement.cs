using Assets.Scripts.Systems.Health;
using System;


namespace Assets.Scripts.TestScenario.Requirement
{
    public class DeathTestRequirement : TestRequirementBase
    {
        bool _subjectDead;
        public virtual void Awake()
        {
            _subject.GetComponent<HealthSystem>().Death += OnDeath;
        }
        public override bool Check()
        {
            return _subjectDead;
        }

        private void OnDeath(object sender, EventArgs args)
        {
            _subjectDead = true;
        }
    }
}
