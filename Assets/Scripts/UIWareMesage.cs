using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Test1024
{
    public class UIWareMesage : MonoBehaviour
    {
        [SerializeField] int _wareNum = 1;

        int _state = 1;
        TMP_Text _mesage;

        private void Start()
        {
            _mesage = GetComponent<TMP_Text>();

            EventService.Instance.WareFullEvent.AddListener(WareFull);
            EventService.Instance.LowOreEvent.AddListener(LowOre);
            EventService.Instance.WareOkEvent.AddListener(WareOk);
        }

        void WareFull(int num)
        {
            if ((_wareNum != num) || (_state == 1)) return;
            _state = 1;
            _mesage.text = "Склад заполнен";
            //_mesage.color = Color.yellow;
            StartCoroutine(ScaleText());
            _mesage.DOColor(Color.yellow, 0.5f);
        }
        void LowOre(int num)
        {
            if ((_wareNum != num) || (_state == 2)) return;
            _state = 2;
            _mesage.text = "Нехватает руды";
            StartCoroutine(ScaleText());
            _mesage.DOColor(Color.red, 0.5f);
        }
        void WareOk(int num)
        {
            if ((_wareNum != num) || (_state == 3)) return;
            _state = 3;
            _mesage.text = "Идет производство";
            StartCoroutine(ScaleText());
            _mesage.DOColor(Color.green, 0.5f);
        }


        IEnumerator ScaleText()
        {
            var fontSize = _mesage.fontSize;
            for (int i = 1; i < 12; i++)
            {
                yield return new WaitForFixedUpdate();
                _mesage.fontSize = fontSize + i;
            }
            _mesage.fontSize = fontSize;
        }

    }
}
