using UnityEngine;
using Utils.Singleton;
using UnityEngine.Pool;

namespace Utils
{
    public abstract class PoolBase<T, S> : Singleton<S> where T : Component, IPoolItem where S : MonoBehaviour
    {
        public int preWarmSize = 2;
        public T PFB_item;
        protected ObjectPool<T> _pool;

        protected override void Awake()
        {
            base.Awake();
            InitPool();
        }

        private void InitPool()
        {
            _pool = new ObjectPool<T>(
                CreatePoolItem,
                OnGetItem,
                OnReturnItem,
                null,
                true,
                preWarmSize
            );
        }

        protected void OnGetItem(T item)
        {
            item.GetFromPool();
        }
        protected void OnReturnItem(T item)
        {
            if(item != null)
            {
                item.ReturnToPool();
            }
        }

        protected virtual T CreatePoolItem()
        {
            return Instantiate(PFB_item, gameObject.transform);
        }

        public T GetPoolItem()
        {
            return _pool.Get();
        }

        public void ReturnPoolItem(T item)
        {
            _pool.Release(item);
        }
    }
}
