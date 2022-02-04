using UnityEngine;

namespace Assets.Scripts
{
    internal class Coin : MonoBehaviour
    {
        public int Value => _value;

        [SerializeField] private int _value;

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
