using PlayerGun;

namespace State.Interface
{
    public interface IShot
    {
        public void Shot();
        public void Interupt();
        public void SetGun(IGunnable gun);

        public bool IsCurrentlyInAttackState();
    }
}