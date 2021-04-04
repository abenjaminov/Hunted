using System;
using UnityEngine;

namespace Game
{
    public abstract class PowerUp<T>: Collectable where T : class 
    {
        private Sprite _sprite;
        [SerializeField] private bool IsPermanent;
        [SerializeField] private float _duration; // Hide this if IsPermanent is checked
        private float _timeUntillExpired;
        
        [SerializeField] private float _timeAvailable;
        private float _timeUntillNotAvailable;
        
        private bool _isPickedUp;

        private Collider2D _collider;
        private SpriteRenderer _renderer;
        
        protected T _acceptor;
        
        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _renderer = GetComponent<SpriteRenderer>();

            _timeUntillNotAvailable = _timeAvailable;
        }

        private void Update()
        {
            if (!_isPickedUp)
            {
                _timeUntillNotAvailable -= Time.deltaTime;

                if (_timeUntillNotAvailable <= 0)
                {
                    Destroy(gameObject);
                }
                
                return;
            }

            _timeUntillExpired -= Time.deltaTime;

            if (_timeUntillExpired <= 0)
            {
                this.Expired();
                Destroy(this.gameObject);
            }
        }

        protected abstract void Expired();
        protected abstract void Payload();

        private void PickedUp()
        {
            if (IsPermanent)
            {
                Destroy(gameObject);
            }
            else
            {
                _timeUntillExpired = _duration;
                _isPickedUp = true;
                _collider.enabled = false;
                _renderer.enabled = false;
            }
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (TryPickUp(other))
            {
                this.Payload();
            }
        }

        private bool TryPickUp(Collider2D other)
        {
            Component acceptor;

            if(other.TryGetComponent(typeof(T), out acceptor))
            {
                this._acceptor = acceptor as T;
                this.PickedUp();
            }

            return this._acceptor != null;
        }
    }
}