using System;
using System.Collections;
using UnityEngine;

namespace Script.Enemy.Movement
{
    public class IaSystem : MonoBehaviour
    {
        private EnemyMovement _enemyMovement;
        private EnemyShoot _enemyShoot;

        public int difficult;

        private float _timeProcess;

        private GameObject _playerGameObject;

        private Vector3 _playerPosition;

        private int _directions;

        private bool _iaCooldown;

        private bool _action;

        private float _distX;
        private float _distY;
        


        private void Start()
        {
            _enemyMovement = gameObject.GetComponent<EnemyMovement>();
            _enemyShoot = gameObject.transform.GetChild(0).GetComponent<EnemyShoot>();
        }

        private void OnEnable()
        {
            
            _playerGameObject = GameObject.Find("Player");
            
        }

        private void Update()
        {
            if (!_action)
            {
                _enemyMovement.AnimaEnemy(0, 0, false);
            }
            if (!_iaCooldown)
            {
                
                StartCoroutine(Cooldown());
            }
            
            Ia();
        }
        
        private void Ia()
        {
            
            if (_action)
            {
                if (_directions == 1)
                {
                    if(_playerGameObject.transform.position.x-0.3f < gameObject.transform.position.x)
                    {
                        _enemyMovement.AnimaEnemy(0, 0, false);
                        _enemyMovement.MoveEnemy(0,0,difficult,false);
                        _iaCooldown = false;
                        _directions = 0;
                        _action = false;
                    }
                    if (_playerGameObject.transform.position.x-0.3f > gameObject.transform.position.x)
                    {
                        _enemyMovement.AnimaEnemy(1, 0, false);
                        _enemyMovement.MoveEnemy(1,0,difficult,false);
                    }
                    
                }
                if (_directions == 2)
                {
                    if(_playerGameObject.transform.position.y < gameObject.transform.position.y)
                    {
                        _enemyMovement.AnimaEnemy(0, 0, false);
                        _enemyMovement.MoveEnemy(0,0,difficult,false);
                        _iaCooldown = false;
                        _directions = 0;
                        _action = false;
                    }
                    if (_playerGameObject.transform.position.y > gameObject.transform.position.y)
                    {
                        _enemyMovement.AnimaEnemy(0, 1, false);
                        _enemyMovement.MoveEnemy(0,1,difficult,false);
                    }
                    
                }
                if (_directions == 3)
                {
                    if(_playerGameObject.transform.position.x+0.3f > gameObject.transform.position.x)
                    {
                        _enemyMovement.AnimaEnemy(0, 0, false);
                        _enemyMovement.MoveEnemy(0,0,difficult,false);
                        _iaCooldown = false;
                        _directions = 0;
                        _action = false;
                    }
                    if (_playerGameObject.transform.position.x+0.3f < gameObject.transform.position.x)
                    {
                        _enemyMovement.AnimaEnemy(-1, 0, false);
                        _enemyMovement.MoveEnemy(-1,0,difficult,false);
                    }
                    
                }
                if (_directions == 4)
                {
                    if(_playerGameObject.transform.position.y > gameObject.transform.position.y)
                    {
                        _enemyMovement.AnimaEnemy(0, 0, false);
                        _enemyMovement.MoveEnemy(0,0,difficult,false);
                        _iaCooldown = false;
                        _directions = 0;
                        _action = false;
                    }
                    if (_playerGameObject.transform.position.y < gameObject.transform.position.y)
                    {
                        _enemyMovement.AnimaEnemy(0, -1, false);
                        _enemyMovement.MoveEnemy(0,-1,difficult,false);
                    }
                    
                }
                if (_directions == 5)
                {
                    _distY = Mathf.Abs(_playerGameObject.transform.position.y - gameObject.transform.position.y);
                    
                    if(_distY > 0.5f)
                    {
                        _enemyMovement.AnimaEnemy(0, 0, false);
                        _enemyShoot.ShootEnemy(0,0,false, difficult);
                        _iaCooldown = false;
                        _directions = 0;
                        _action = false;
                    }
                    if (_distY < 0.5f)
                    {
                        _enemyMovement.AnimaEnemy(1, 0, true);
                        _enemyShoot.ShootEnemy(1,0,true,difficult);
                    }
                }
                if (_directions == 6)
                {
                    _distX = Mathf.Abs(_playerGameObject.transform.position.x - gameObject.transform.position.x);
                    
                    if(_distX > 0.5f)
                    {
                        _enemyMovement.AnimaEnemy(0, 0, false);
                        _enemyShoot.ShootEnemy(0,0,false, difficult);
                        _iaCooldown = false;
                        _directions = 0;
                        _action = false;
                    }
                    if (_distX < 0.5f)
                    {
                        _enemyMovement.AnimaEnemy(0, 1, true);
                        _enemyShoot.ShootEnemy(0,1,true,difficult);
                    }
                }
                if (_directions == 7)
                {
                    _distY = Mathf.Abs(_playerGameObject.transform.position.y - gameObject.transform.position.y);
                    
                    if(_distY > 0.5f)
                    {
                        _enemyMovement.AnimaEnemy(0, 0, false);
                        _enemyShoot.ShootEnemy(0,0,false, difficult);
                        _iaCooldown = false;
                        _directions = 0;
                        _action = false;
                    }
                    if (_distY < 0.5f)
                    {
                        _enemyMovement.AnimaEnemy(-1, 0, true);
                        _enemyShoot.ShootEnemy(-1,0,true,difficult);
                    }
                }
                if (_directions == 8)
                {
                    _distX = Mathf.Abs(_playerGameObject.transform.position.x - gameObject.transform.position.x);
                    
                    if(_distX > 0.5f)
                    {
                        _enemyMovement.AnimaEnemy(0, 0, false);
                        _enemyShoot.ShootEnemy(0,0,false, difficult);
                        _iaCooldown = false;
                        _directions = 0;
                        _action = false;
                    }
                    if (_distX < 0.5f)
                    {
                        _enemyMovement.AnimaEnemy(0, -1, true);
                        _enemyShoot.ShootEnemy(0,-1,true,difficult);
                    }
                }
            }
        }


        private IEnumerator Cooldown()
        {
            _iaCooldown = true;
            
            yield return new WaitForSeconds(8f/difficult);

            var position = _playerGameObject.transform.position;
            var position1 = gameObject.transform.position;
            _distX = Mathf.Abs(position.x - position1.x);
            _distY = Mathf.Abs(position.y - position1.y);
            
            if (difficult > 1)
            {
                if (_distX < 0.5f || _distY < 0.25f )
                {
                    if (_distY < 0.25f)
                    {
                        if (_playerGameObject.transform.position.x > gameObject.transform.position.x)
                        {
                            _directions = 5;
                            _action = true;
                        }
                        if (_playerGameObject.transform.position.x < gameObject.transform.position.x)
                        {
                            _directions = 7;
                            _action = true;
                        }
                    
                    }
                    if (_distX < 0.5f)
                    {
                        if (_playerGameObject.transform.position.y > gameObject.transform.position.y)
                        {
                            _directions = 6;
                            _action = true;
                        }
                        if (_playerGameObject.transform.position.y < gameObject.transform.position.y)
                        {
                            _directions = 8;
                            _action = true;
                        }
                    }
                }
                else
                {
                    if (_distX < _distY)
                    {
                        if (_playerGameObject.transform.position.x > gameObject.transform.position.x)
                        {
                            _directions = 1;
                            _action = true;
                        }

                        if (_playerGameObject.transform.position.x < gameObject.transform.position.x)
                        {
                            _directions = 3;
                            _action = true;
                        }
                    }

                    if (_distX > _distY)
                    {
                        if (_playerGameObject.transform.position.y > gameObject.transform.position.y)
                        {
                            _directions = 2;
                            _action = true;
                        }

                        if (_playerGameObject.transform.position.y < gameObject.transform.position.y)
                        {
                            _directions = 4;
                            _action = true;
                        }
                    }
                }
            }
            if(difficult == 1)
            {
                if (_distX > _distY)
                {
                    if (_playerGameObject.transform.position.x > gameObject.transform.position.x)
                    {
                        _directions = 1;
                        _action = true;
                    }

                    if (_playerGameObject.transform.position.x < gameObject.transform.position.x)
                    {
                        _directions = 3;
                        _action = true;
                    }
                }

                if (_distX < _distY)
                {
                    if (_playerGameObject.transform.position.y > gameObject.transform.position.y)
                    {
                        _directions = 2;
                        _action = true;
                    }

                    if (_playerGameObject.transform.position.y < gameObject.transform.position.y)
                    {
                        _directions = 4;
                        _action = true;
                    }
                }
            }
        }
    }
}
