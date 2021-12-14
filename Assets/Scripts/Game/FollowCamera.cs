using UnityEngine;

namespace Game
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;

        [SerializeField] private Transform _target;

        private void Update()
        {
            if (_target != null)
            {
                transform.position = _target.position + _offset;
            }
        }
    }
}
