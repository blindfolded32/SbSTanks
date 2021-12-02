using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Bullet
{
    public sealed class BulletPool 
    {
        private readonly Dictionary<string, HashSet<Bullet>> _bulletPool;
        private readonly int _capacity;
        private readonly Transform _rootPool;
        public BulletPool(int capacityPool)
        {
            _bulletPool = new Dictionary<string, HashSet<Bullet>>();
            _capacity = capacityPool;
            if (!_rootPool)
            {
                _rootPool = new GameObject(NameManager.BULLET_POOL).transform;
            }
            
        }
        public Bullet GetItem(string type)
        {
            Bullet result;
            switch (type)
            {
                case "Bullet":
                    result = GetBullet(GetListBullet(type));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, "Не предусмотрен в программе");
            }
            return result;
        }
        private HashSet<Bullet> GetListBullet(string type)
        {
            return _bulletPool.ContainsKey(type) ? _bulletPool[type] : _bulletPool[type] = new HashSet<Bullet>();
        }
        private Bullet GetBullet(HashSet<Bullet> bullets)
        {
            var bullet = bullets.FirstOrDefault(a => !a.gameObject.activeSelf);
            if (bullet == null )
            {
                var laser = Resources.Load<Bullet>("Prefabs/Shell");
                for (var i = 0; i < _capacity; i++)
                {
                    var instantiate = Object.Instantiate(laser);
                    
                    ReturnToPool(instantiate.transform);
                    bullets.Add(instantiate);
                    
                }
                GetBullet(bullets);
            }
            bullet = bullets.FirstOrDefault(a => !a.gameObject.activeSelf);
            return bullet;
        }
        private void ReturnToPool(Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.gameObject.SetActive(false);
            transform.SetParent(_rootPool);
        }
        public void RemovePool()
        {
            Object.Destroy(_rootPool.gameObject);
        }
        
    }
}