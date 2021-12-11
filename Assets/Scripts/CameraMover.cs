using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _offset;

    private void Update()
    {
        var cameraPosition = new Vector3(transform.position.x, transform.position.y, _target.transform.position.z - _offset);
        transform.position = cameraPosition;
    }
}
