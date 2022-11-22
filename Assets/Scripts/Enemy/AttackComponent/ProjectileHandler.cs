using UnityEngine;

namespace Enemy.AttackComponent
{
    public class ProjectileHandler : MonoBehaviour
    {
        [SerializeField] private GameObject particle;
        [SerializeField] private GameObject outputPosition;
        public float missileForce = 1;

        public void AttackTowards(Transform obj)
        {
            GameObject projectile = Instantiate(particle, outputPosition.transform.position, outputPosition.transform.rotation);
            projectile.transform.position =
                new Vector3(projectile.transform.position.x, projectile.transform.position.y, 0f);
            ProjectileMissile missile = projectile.GetComponent<ProjectileMissile>();
            missile.Init(missileForce, obj);
            Destroy(projectile, 5);
        }
    }
}