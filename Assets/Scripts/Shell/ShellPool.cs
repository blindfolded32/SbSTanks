using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SbSTanks
{
    public class ShellPool
    {
        internal sealed class BulletPool
        {
            private readonly Dictionary<string, HashSet<Shell>> _bulletPool;
            private readonly int _capacity;
            private Transform _rootPool;

            public BulletPool(int capacityPool)
            {
                _bulletPool = new Dictionary<string, HashSet<Shell>>();
                _capacity = capacityPool;
                if (!_rootPool)
                {
                    _rootPool = new GameObject(NameManager.BULLET_POOL).transform;
                }
            }

            public Shell GetItem(string type)
            {
                Shell result;
                switch (type)
                {
                    case "Shell":
                        result = GetBullet(GetListBullet(type));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, "Не предусмотрен в программе");
                }

                return result;
            }

            private HashSet<Shell> GetListBullet(string type)
            {
                return _bulletPool.ContainsKey(type)
                    ? _bulletPool[type]
                    : _bulletPool[type] = new HashSet<Shell>();
            }

            private Shell GetBullet(HashSet<Shell> bullets)
            {
                var bullet = bullets.FirstOrDefault(a => !a.isOnScene);
                if (bullet == null)
                {
                    var laser = Resources.Load("Prefabs/Bullet");
                    for (var i = 0; i < _capacity; i++)
                    {
                        var instantiate = Object.Instantiate(laser);
                      //  ReturnToPool(instantiate.transform);
                      //  bullets.Add(instantiate);
                    }

                    GetBullet(bullets);
                }
                bullet = bullets.FirstOrDefault(a => !a.isOnScene);
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
}