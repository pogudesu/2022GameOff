using UnityEngine;

namespace Enemy.AttackComponent
{
    public class ProjectileHandler : MonoBehaviour
    {
        [SerializeField] private GameObject particle;
        [SerializeField] private GameObject outputPosition;

        public void AttackTowards(Transform obj)
        {
            GameObject projectile = Instantiate(particle, outputPosition.transform.position, Quaternion.identity);
            ProjectileMissile missile = projectile.GetComponent<ProjectileMissile>();
            missile.Init(1f, obj);
            Destroy(projectile, 5);
        }
    }
}