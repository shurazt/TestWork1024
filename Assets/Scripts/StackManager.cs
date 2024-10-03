using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using UnityEngine;

namespace Test1024
{
    public class StackManager : MonoBehaviour
    {
        [SerializeField] float _speedRecive = 2f;
        [SerializeField] float _speedTransmit = 1f;

        private OreStack _reciver = null;
        private TriggerRecive _recTrigger = null;
        private OreStack _transmitter = null;
        private OreStack _stack;

        private void Start()
        {
            _stack = GetComponent<OreStack>();
            if (_stack != null)
            {
                StartCoroutine(OreRecive());
                StartCoroutine(OreTransmit());
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var rec = other.GetComponent<TriggerRecive>();
            if (rec != null)
                if (!rec.Trash)
                {
                    if (isReciverOre(rec))
                    {
                        _recTrigger = rec;
                        _reciver = other.GetComponent<OreStack>();
                    }
                }
                else _recTrigger = rec;

            var trans = other.GetComponent<TriggerTransmit>();
            if (trans != null)
                _transmitter = other.GetComponent<OreStack>();
        }

        private void OnTriggerExit(Collider other)
        {
            var rec = other.GetComponent<TriggerRecive>();
            if (rec != null)
            {
                _recTrigger = null;
                _reciver = null;
            }

            var trans = other.GetComponent<TriggerTransmit>();
            if (trans != null)
                _transmitter = null;
        }

        IEnumerator OreRecive()
        {
            while (true)
            {
                if (_transmitter != null)
                {
                    if (!_stack.OreStackFull())
                    {
                        var ore = _transmitter.GetOre();
                        if (ore != null)
                            _stack.AddOre(ore, false);
                    }
                }
                yield return new WaitForSeconds(_speedRecive);
            }
        }

        IEnumerator OreTransmit()
        {
            while (true)
            {
                if (_recTrigger != null)
                {
                    if (!_recTrigger.Trash)
                    {
                        if (isReciverOre(_recTrigger))
                        {
                            if (!_reciver.OreStackFull())
                            {
                                var ore = _stack.GetOre();
                                if (ore != null)
                                    _reciver.AddOre(ore, false);
                            }
                        }
                    }
                    else
                    {
                        _stack.UsageOre(_recTrigger.transform);
                    }
                }
                yield return new WaitForSeconds(_speedTransmit);
            }
        }

        private bool isReciverOre(TriggerRecive reciver)
        {
            if (_stack != null)
            {
                var ore = _stack.GetLastOre();
                if (ore == null) return false;

                var matOre = ore.gameObject.GetComponent<MeshRenderer>().materials[0];
                //if (matOre.Equals(reciver.OreMaterial))
                if (matOre.name.Contains(reciver.OreMaterial.name))
                {
                    return true;
                }
            }

            return false;
        }

    }
}
