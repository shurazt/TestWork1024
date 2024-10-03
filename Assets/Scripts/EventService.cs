using UnityEngine;
using UnityEngine.Events;

namespace Test1024
{
    public class EventService : MonoBehaviour
    {
        public static EventService Instance;

        public UnityEvent<int> WareFullEvent = new UnityEvent<int>();
        public UnityEvent<int> LowOreEvent = new UnityEvent<int>();
        public UnityEvent<int> WareOkEvent = new UnityEvent<int>();
        private void Awake()
        {
            if (Instance != null)
                GameObject.Destroy(Instance);
            else
                Instance = this;

            DontDestroyOnLoad(this);
        }

        public void WareFull(int val)
        {
            WareFullEvent.Invoke(val);
        }
        public void LowOre(int val)
        {
            LowOreEvent.Invoke(val);
        }
        public void WareOk(int val)
        {
            WareOkEvent.Invoke(val);
        }
    }
}
