using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Test1024
{
    public class OreStack : MonoBehaviour
    {
        [SerializeField] int _maxCount = 20;
        [SerializeField] float _blockHeight = 0.4f;
        [SerializeField] Transform _startPos;
        [SerializeField] Transform _endPos;
        [SerializeField] private float _duration = 0.3f;
        [SerializeField] private float _jumpPower = 3f;

        List<Ore> _stack = new List<Ore>();


        public bool AddOre(Ore ore, bool fromProd = true)
        {
            if (_stack.Count < _maxCount)
                _stack.Add(ore);
            else
            {
                Debug.Log("Ware full !!!");
                return false;
            }

            Transform startPos;
            if (fromProd)
                startPos = _startPos;
            else
                startPos = ore.transform;

            ore.transform.position = startPos.position;
            ore.transform.rotation = startPos.rotation;

            ore.gameObject.SetActive(true);

            //ore.transform.DOJump(calcEndPos(), _jumpPower, 1, _duration)
            //    .Join(ore.transform.DORotate(calcEndRot(), _duration, RotateMode.Fast));


            DOTween.Sequence()
           .Append(ore.transform.DOJump(calcEndPos(), _jumpPower, 1, _duration))
           .Join(ore.transform.DORotate(calcRot(_endPos), _duration, RotateMode.Fast))
           .OnComplete(() => DrawStack());

            return true;
        }

        public Ore GetLastOre()
        {
            if (_stack.Count > 0)
                return _stack.Last();
            return null;
        }

        public Ore GetOre()
        {
            if (_stack.Count > 0)
            {
                var obj = _stack.Last();
                _stack.RemoveAt(_stack.Count - 1);
                return obj;
            }
            return null;
        }

        public bool OreStackFull()
        {
            if (_stack.Count >= _maxCount) return true;
            else return false;
        }

        public Ore UsageOre(Transform endPos)
        {
            var ore = GetOre();
            if (ore == null) return null;

            DOTween.Sequence()
           .Append(ore.transform.DOJump(endPos.position, _jumpPower, 1, _duration))
           .Join(ore.transform.DORotate(calcRot(endPos), _duration, RotateMode.Fast))
           .OnComplete(() =>
           {
               ore.gameObject.SetActive(false);
               //DrawStack();
           });
            return ore;
        }
        public Ore UsageOre()
        {
            return UsageOre(_startPos);
        }



        public void DrawStack()
        {
            for (int i = 0; i < _stack.Count; i++)
            {
                var ore = _stack[i];
                ore.transform.position = calcEndPos(i);
                ore.transform.rotation = _endPos.rotation;
            }
        }

        private Vector3 calcEndPos(int num = -1)
        {
            var numPos = num;
            if (numPos < 0) numPos = _stack.Count - 1;
            var pos = new Vector3(_endPos.position.x, _endPos.position.y + (numPos * _blockHeight), _endPos.position.z);
            return pos;
        }

        private Vector3 calcRot(Transform transform)
        {
            ///return new Vector3(0,90,0);
            return transform.eulerAngles;
        }
    }
}
