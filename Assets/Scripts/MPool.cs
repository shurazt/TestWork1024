using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test1024
{
    public class MPool<T> where T : MonoBehaviour
    {
        public T Prefab { get; set; }
        public Transform Container;
        private List<T> pool = new List<T>();

        public MPool(T prefab, int count, Transform container = null)
        {
            Prefab = prefab;
            Container = container;
            createPoll(count);
        }

        public void createPoll(int count)
        {
            for (int i = 0; i < count; i++)
            {
                createMono();
            }
        }
        private T createMono()
        {
            var obj = Object.Instantiate(Prefab, Container);
            obj.gameObject.SetActive(false);
            pool.Add(obj);
            return obj;
        }

        public T GetMono()
        {
            var obj = GetFree();
            if (obj == null)
                return createMono();
            else return obj;
        }

        public T GetFree()
        {
            foreach (var obj in pool)
            {
                if (!obj.gameObject.activeInHierarchy)
                {
                    return obj;
                }
            }
            return null;
        }

    }

}