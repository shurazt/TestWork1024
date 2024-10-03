using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test1024
{
    public class OrePool : MonoBehaviour
    {
        [SerializeField] Ore _orePrefab;
        [SerializeField] int _count = 10;

        public MPool<Ore> Pool {get; private set;}

        private void Awake()
        {
            Pool=new MPool<Ore>(_orePrefab, _count, transform);
        }
    }
}
