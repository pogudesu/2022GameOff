using UnityEngine;

namespace PlayerGun
{
    [CreateAssetMenu()]
    public class Gun : ScriptableObject, IGunnable
    {
        public int currentNumBullet;
        public string animationName;
        public GunType type;
        public int attackPower;
        public int GetBullet()
        {
            return currentNumBullet;
        }

        public string GetAnimationName()
        {
            return animationName;
        }

        public GunType GetType()
        {
            return type;
        }
    }

    public enum GunType
    {
        PISTOL,
        SNIPER,
        DUAL_PISTOL
    }
}