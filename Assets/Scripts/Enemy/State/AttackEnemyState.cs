using Enemy.Data;
using StateMachine.PlayerState;

namespace Enemy.State
{
    public class AttackEnemyState : BaseState
    {
        private EnemyAttackData _enemyAttackData;
        public bool IsAttacking { get; set; }
        public override void Enter(object obj)
        {
            if (SetActor(obj) == false) return;
            IsAttacking = true;
            _actor._animator.SetTrigger(_enemyAttackData.animatorName);
        }

        public override void Update(object obj)
        {
            base.Update(obj);
        }

        public override void Exit(object obj)
        {
            base.Exit(obj);
            IsAttacking = false;
            if(_actor.nextState != null)
                _actor.nextState.Enter(_actor);
        }

        public void SetEnemyData(EnemyAttackData data)
        {
            _enemyAttackData = data;
        }
    }
}