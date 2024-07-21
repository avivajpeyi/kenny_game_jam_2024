using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public delegate void DestroyAction();
    public event DestroyAction OnDestroyed;
    
    private Player _player;
    [SerializeField] private GameObject _explosionPrefab;
    
    void Start()
    {

        _player = Player.Instance;
        
    }

    void OnDestroy()
    {
        if (OnDestroyed != null)
        {
            OnDestroyed();
        }
    }
    
    
    private void OnCollisionEnter(Collision collision) {
        if(_explosionPrefab) Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

        if (collision.transform == _player.transform)
        {
            Debug.Log("Player hurt by " + gameObject.name);
            _player.TakeDamage();
            
        }
        if (collision.transform.CompareTag("WreckingBall")) _player.AddScore();
            
        Destroy(gameObject);
    }
    
    
}
