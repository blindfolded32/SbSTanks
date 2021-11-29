using Interfaces;
using Markers;
using UnityEngine;

namespace Bullet
{
    public class Bullet : MonoBehaviour
    {
        private Transform _bulletPool;
        public Transform Transform;
        private float Damage { get; set; }
        private NameManager.ElementList Element { get; set; }
        
        [SerializeField] private float _ttl = 0.8f;
        public float _timeToDie;

        public float AddDamage(float value) => Damage = value;
        public NameManager.ElementList AddElement(NameManager.ElementList value) => Element = value;
        
        private void Awake()
        {
            Transform = transform;
            _timeToDie = Time.time + _ttl;
        }
        private void Update()
        {
            if (Time.time > _ttl + _timeToDie && isActiveAndEnabled)
            {
                ReturnToPool();
            }
        }
       
        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.TryGetComponent<IDamagebleUnit>(out var unit)) return;
            Debug.Log($"Damaging {Damage} elem {Element}");
            unit.TakingDamage(Damage,Element);
            ReturnToPool();
        }
        private Transform BulletPool
        {
            get
            {
                if (_bulletPool != null) return _bulletPool;
                var find = GameObject.Find(NameManager.BULLET_POOL);
                _bulletPool = find == null ? null : find.transform;
                return _bulletPool;
            }
        }
        public void ReturnToPool()
        {
            transform.localPosition = Vector2.zero;
            transform.localRotation = Quaternion.identity;
            gameObject.SetActive(false);
            transform.SetParent(BulletPool);
            if (!BulletPool)
            {
                Destroy(gameObject);
            }
        }
    }
}