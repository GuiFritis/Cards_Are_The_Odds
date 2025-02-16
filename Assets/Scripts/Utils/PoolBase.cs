using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Singleton;

namespace Utils
{
    public abstract class PoolBase<T, S> : Singleton<S> where T : Component where S : MonoBehaviour
    {
        public int preWarmSize = 2;
        public T PFB_item;

        protected List<T> _pool = new();

        protected override void Awake()
        {
            base.Awake();
            InitPool();
        }

        private void InitPool()
        {
            _pool = new();

            for (int i = 0; i < preWarmSize; i++)
            {
                CreatePoolItem();
            }
        }

        private void CreatePoolItem()
        {
            T item = Instantiate(PFB_item, gameObject.transform);
            _pool.Add(item);
        }

        public T GetPoolItem()
        {
            T item = null;
            item = _pool.Find(CheckItem);
            if(item == null)
            {
                CreatePoolItem();
                item = _pool[^1];
            }
            return item;
        }

        protected abstract bool CheckItem(T item);
    }
}
