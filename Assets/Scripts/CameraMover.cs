using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _zOffset;

    private void Update()
    {
        var cameraPosition = new Vector3(_target.position.x, transform.position.y, _target.transform.position.z - _zOffset);
        transform.position = cameraPosition;
    }
}
