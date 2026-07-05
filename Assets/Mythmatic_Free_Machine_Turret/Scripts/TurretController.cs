using UnityEngine;
using System.Collections.Generic;

namespace Mythmatic.TurretSystem
{
    public class TurretController : MonoBehaviour
    {
        #region Turret Movement
        [Header("================ TURRET MOVEMENT ================")]
        [Header(">> Base Rotation")]
        [Range(0, 360)]
        [SerializeField] private float baseRotationSpeed = 180f;
        [Range(0.01f, 1f)]
        [SerializeField] private float alignmentThreshold = 0.1f;

        [Header(">> Weapon Mount")]
        [Range(0, 360)]
        [SerializeField] private float weaponRotationSpeed = 180f;
        [SerializeField] private Transform weaponMount;
        [SerializeField] private Transform aimReference;
        #endregion

        #region Enemy Detection
        [Header("================ TARGET DETECTION ================")]
        [Range(1f, 500f)]
        [SerializeField] private float attackRange = 200f;
        #endregion

        #region Combat Settings
        [Header("================== COMBAT ===================")]
        [Header(">> Firing Controls")]
        [SerializeField] private List<Transform> projectileSpawnPoints = new List<Transform>();
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private GameObject explosionPrefab;
        [Range(0.1f, 10f)]
        [SerializeField] private float fireRate = 1f;

        [Header(">> Projectile Behavior")]
        [Range(1f, 100f)]
        [SerializeField] private float projectileSpeed = 20f;
        [Range(0f, 360f)]
        [SerializeField] private float projectileRotationSpeed = 180f;
        [Range(0f, 10f)]
        [SerializeField] private float projectileHomingStrength = 5f;
        [Range(0.1f, 10f)]
        [SerializeField] private float projectileLifetime = 3f;
        #endregion

        #region Animation
        [Header("================== ANIMATION ==================")]
        [SerializeField] private Animator animator;
        [Range(0.1f, 10f)]
        public float animationSpeed = 1f;
        #endregion

        private GameObject player;
        private bool torreteActiva = false;
        private bool isBaseRotating = false;
        private bool isWeaponRotating = false;
        private float targetWeaponAngle = 0f;
        private float currentWeaponAngle = 0f;
        private bool isInRange = false;
        private float nextFireTime = 0f;
        private bool isReadyToFire = false;
        private bool isFiring = false;
       
        private Transform cabezaPlayer;

        private void Start()
        {
           
            Physics.IgnoreLayerCollision(
                LayerMask.NameToLayer("Projectile"),
                LayerMask.NameToLayer("Projectile"), true);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("PlayerHead")) return;
            player = other.transform.root.gameObject; // agarra el root del player
            cabezaPlayer = other.transform;
            torreteActiva = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("PlayerHead")) return;
            torreteActiva = false;
            cabezaPlayer = null;
            UpdateFiringState(false);
        }

        private void Update()
        {
            if (!torreteActiva || player == null) return;

            if (weaponMount == null || aimReference == null)
            {
                UpdateFiringState(false);
                return;
            }

            float distanciaAlPlayer = Vector3.Distance(transform.position, player.transform.position);
            isInRange = distanciaAlPlayer <= attackRange;

            Vector3 direccionAlPlayer = (player.transform.position - transform.position).normalized;
            Vector3 direccionPlana = new Vector3(direccionAlPlayer.x, 0, direccionAlPlayer.z);

            if (direccionPlana != Vector3.zero)
            {
                Quaternion rotacionObjetivo = Quaternion.LookRotation(direccionPlana, Vector3.up);

                if (Quaternion.Angle(transform.rotation, rotacionObjetivo) > alignmentThreshold)
                {
                    isBaseRotating = true;
                    RotarBase(rotacionObjetivo);
                }
                else if (isBaseRotating)
                {
                    isBaseRotating = false;
                    CalcularRotacionArma();
                }

                if (!isBaseRotating)
                {
                    CalcularRotacionArma();
                    if (isWeaponRotating)
                        RotarArma();
                }

                isReadyToFire = isInRange; 
                Debug.Log("isInRange: " + isInRange + " | isBaseRotating: " + isBaseRotating + " | isWeaponRotating: " + isWeaponRotating + " | distancia: " + Vector3.Distance(transform.position, player.transform.position));

                if (isReadyToFire && Time.time >= nextFireTime)
                {
                    DispararProyectil();
                    UpdateFiringState(true);
                }
                else if (!isReadyToFire && isFiring)
                {
                    UpdateFiringState(false);
                }
            }
            else
            {
                UpdateFiringState(false);
            }

            
        }

        private void UpdateFiringState(bool firing)
        {
            if (isFiring != firing)
            {
                isFiring = firing;
                if (animator != null)
                {
                    animator.SetBool("Attack", firing);
                    animator.speed = animationSpeed;
                }
            }
        }

        private void RotarBase(Quaternion rotacionObjetivo)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                rotacionObjetivo,
                baseRotationSpeed * Time.deltaTime);
        }

        private void CalcularRotacionArma()
        {
            Vector3 haciaPlayer = player.transform.position - aimReference.position;
            Vector3 localHaciaPlayer = transform.InverseTransformDirection(haciaPlayer);
            Vector3 localAimForward = transform.InverseTransformDirection(aimReference.forward);

            float anguloObjetivo = -Mathf.Atan2(localHaciaPlayer.y, localHaciaPlayer.z) * Mathf.Rad2Deg;
            currentWeaponAngle = -Mathf.Atan2(localAimForward.y, localAimForward.z) * Mathf.Rad2Deg;

            float diferencia = Mathf.DeltaAngle(currentWeaponAngle, anguloObjetivo);

            if (Mathf.Abs(diferencia) > alignmentThreshold)
            {
                isWeaponRotating = true;
                targetWeaponAngle = anguloObjetivo;
            }
            else
            {
                isWeaponRotating = false;
            }
        }

        private void RotarArma()
        {
            float diferencia = Mathf.DeltaAngle(currentWeaponAngle, targetWeaponAngle);
            float maxRotacion = weaponRotationSpeed * Time.deltaTime;
            float rotacion = Mathf.Clamp(diferencia, -maxRotacion, maxRotacion);

            weaponMount.localRotation *= Quaternion.Euler(rotacion, 0, 0);
            currentWeaponAngle += rotacion;

            if (Mathf.Abs(diferencia) <= alignmentThreshold)
                isWeaponRotating = false;
        }

        private void SetLayerRecursively(GameObject obj, int layer)
        {
            obj.layer = layer;
            foreach (Transform child in obj.transform)
                SetLayerRecursively(child.gameObject, layer);
        }

        private void DispararProyectil()
        {
            {
                if (projectilePrefab == null || projectileSpawnPoints.Count == 0) return;

                foreach (Transform spawnPoint in projectileSpawnPoints)
                {
                    if (spawnPoint == null) continue;

                    Vector3 direccionAlPlayer = (cabezaPlayer.position - spawnPoint.position).normalized;
                    Quaternion rotacionHaciaPlayer = Quaternion.LookRotation(direccionAlPlayer);
                    GameObject proyectil = Instantiate(projectilePrefab, spawnPoint.position, rotacionHaciaPlayer);

                    proyectil.tag = "BalaTorreta";

                    BalaTorreta scriptBala = proyectil.GetComponent<BalaTorreta>();
                    scriptBala.IniciarDireccion(cabezaPlayer.position);
                }

                nextFireTime = Time.time + (1f / fireRate);
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            DrawWireDisc(transform.position, Vector3.up, attackRange);
        }
#endif

        private void DrawWireDisc(Vector3 center, Vector3 normal, float radius)
        {
            int segments = 32;
            Vector3 previousPoint = center + (Vector3.forward * radius);
            for (int i = 1; i <= segments; i++)
            {
                float angle = i * 2 * Mathf.PI / segments;
                Vector3 newPoint = center + new Vector3(Mathf.Sin(angle) * radius, 0, Mathf.Cos(angle) * radius);
                Gizmos.DrawLine(previousPoint, newPoint);
                previousPoint = newPoint;
            }
        }
    }
}