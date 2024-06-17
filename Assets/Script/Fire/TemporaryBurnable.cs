using UnityEngine;

public class TemporaryBurnable : MonoBehaviour
{
    [SerializeField]
    private GameObject _fire;

    [SerializeField] 
    private Rain _rain;

    [SerializeField] 
    private MeshCollider _collider;

    [SerializeField]
    [Tooltip("Is object burning")]
    private bool _isBurning;

    [SerializeField]
    [Tooltip("How long fire exist befor rain")]
    private float _timeBeforRain;

    private void Start()
    {
        _collider = GetComponent<MeshCollider>();
        _isBurning = false;
        
        GameObject rainObj = GameObject.FindWithTag("Rain");
        if (rainObj != null)
        {
            _rain = rainObj.GetComponent<Rain>();
        }
        _timeBeforRain = 1.5f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isBurning)
        {
            Debug.Log("Here");
            GameObject bonfire = collision.gameObject.transform.GetChild(0).gameObject;
            if(bonfire != null)
            {
                Debug.Log("HereHere");
                ContactPoint contact = collision.GetContact(0);
                Burnable otherBurnable = bonfire.GetComponent<Burnable>();
                if (otherBurnable != null && otherBurnable._isBurning)
                {
                    Debug.Log("HereHereHere");
                    StartBurning(contact.point);
                }
                Lighter lighter = collision.gameObject.GetComponent<Lighter>();
                if (lighter != null && lighter.fireSpawned)
                {
                    StartBurning(contact.point);
                }
                else
                if (collision.gameObject.tag == "FireStarter")
                {
                    StartBurning(contact.point);
                }
            }
        }
    }

    private void StartBurning(Vector3 vec)
    {
        _isBurning = true;

        // Instantiate the fire at the contact point
        GameObject _newFire = Instantiate(_fire, vec, Quaternion.identity, transform);

        //_newFire.transform.position = contact.point;
        FireSize bonfire = _newFire.GetComponent<FireSize>();

        if (bonfire != null)
        {
            bonfire.StartAll();
        }

        Invoke("StartRain", _timeBeforRain);
    }
    private void StartRain()
    {
        _rain.StartRain();
    }
}
