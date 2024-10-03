using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test1024
{
    public class OreProduction : MonoBehaviour
    {
        [SerializeField] int _wareNum = 1;
        [SerializeField] float _prodInterval = 2f;
        [SerializeField] float _prodDuration = 1f;
        [SerializeField] OrePool _orePool;
        [SerializeField] Material[] _oreMat;
        [SerializeField] OreStack _ware1;
        [SerializeField] OreStack _ware2;

        OreStack _oreStack;

        private void Start()
        {
            _oreStack = GetComponent<OreStack>();
            if (_oreStack != null)
                StartCoroutine(produce());
        }

        IEnumerator produce()
        {
            yield return new WaitForSeconds(1);
            // for (int i = 0; i < 20; i++)
            while (true)
            {
                if (!_oreStack.OreStackFull())
                {
                    bool w1 = true;
                    bool w2 = true;

                    if ((_ware1 != null) && (_ware1.GetLastOre() == null)) w1 = false;
                    if ((_ware2 != null) && (_ware2.GetLastOre() == null)) w2 = false;

                    if (w1 && w2)
                    {
                        if (_ware1 != null) _ware1.UsageOre();
                        yield return new WaitForSeconds(_prodDuration/2);
                        if (_ware2 != null) _ware2.UsageOre();
                        yield return new WaitForSeconds(_prodDuration/2);
                        
                        var ore = _orePool.Pool.GetMono();
                        ore.gameObject.GetComponent<MeshRenderer>().materials = _oreMat;
                        
                        if (_oreStack.AddOre(ore))
                            EventService.Instance.WareOk(_wareNum);
                    }
                    else
                        EventService.Instance.LowOre(_wareNum);
                }
                else
                    EventService.Instance.WareFull(_wareNum);

                yield return new WaitForSeconds(_prodInterval);
            }
        }
    }
}
