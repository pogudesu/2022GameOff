
using PlayerGun;

namespace Damageable
{
    public interface IDamageable
    {
        void TakeDamage(int attackDamage, GunType attackType);
    }
}