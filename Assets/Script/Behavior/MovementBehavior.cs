using UnityEngine;

namespace Script.Behavior
{
    public class MovementBehavior : MonoBehaviour
    {
        protected Rigidbody2D Rb2d;
        private Vector2 _newVector2;
        
        private Animator _anim;
        protected GameObject GunDirection;

        protected GameObject GunSystem;
        

        private void Start()
        {
            Rb2d = gameObject.GetComponent<Rigidbody2D>();
            _anim = gameObject.GetComponent<Animator>();
            GunDirection = gameObject.transform.GetChild(0).gameObject;
            GunSystem = gameObject.transform.GetChild(0).gameObject;
        }

        protected void Move(float moveX, float moveY, int speed, bool isShooting, bool isDead)
        {
            
            if (!isDead)
            {
                if (!isShooting)
                {
                    _newVector2 = new Vector2(moveX,moveY).normalized;
                    Rb2d.velocity = _newVector2 * speed;
                }
                else
                {
                    _newVector2 = Vector2.zero;
                    Rb2d.velocity = _newVector2 * speed;
                }
            }
        }

        protected void GunPosition(float moveX, float moveY, bool isShooting)
        {
            if (isShooting)
            {
                switch (moveY)
                {
                    case 0 when moveX == 0:
                    case 0 when moveX > 0:
                    case 0 when moveX < 0:
                        GunDirection.transform.localPosition = new Vector3(0.2f,0.22f,0f);
                        break;
                    case > 0 when moveX == 0:
                        GunDirection.transform.localPosition = new Vector3(0.22f,0.45f,0f);
                        break;
                    case < 0 when moveX == 0:
                        GunDirection.transform.localPosition = new Vector3(0.18f,-0.06f,0f);
                        break;
                }
                GunDirection.transform.localPosition = moveY switch
                {
                    > 0 when moveX is > 0 or < 0 => new Vector3(0.34f, 0.44f, 0f),
                    < 0 when moveX is > 0 or < 0 => new Vector3(0.24f, -0.06f, 0f),
                    _ => GunDirection.transform.localPosition
                };
            }
            else
            {
                GunDirection.transform.localPosition = new Vector3(0.3f,0.22f,0f);
            }
            
        }
        
        protected void Anima(float moveX, float moveY,bool isShooting, bool isPlayer, bool isDead)
        {
            if (isDead)
            {
                _anim.Play("Dead");
            }else
            {
                if (moveX != 0 || moveY != 0) 
                {
                    gameObject.transform.rotation = moveX switch
                    {
                        < 0 => Quaternion.Euler(0, 180f, 0),
                        > 0 => Quaternion.Euler(0, 0f, 0),
                        _ => transform.rotation
                    };
                    if (isShooting)
                    {
                        if (!isPlayer) return;
                        switch (moveY)
                        {
                            case > 0:
                                _anim.Play("ShootTop");
                                break;
                            case < 0:
                                _anim.Play("ShootBot");
                                break;
                            case 0:
                                _anim.Play("Shoot");
                                break;
                        }
                    }
                    else
                    {
                        if (!isPlayer)
                        {
                            switch (moveY)
                            {
                                case > 0:
                                    _anim.Play("WalkUp");
                                    break;
                                case < 0:
                                    _anim.Play("WalkDown");
                                    break;
                                case 0:
                                    _anim.Play("Walk");
                                    break;
                            }
                        }
                        else
                        {
                            _anim.Play("Walk");
                        }
                    }
                }else
                {
                    if (isShooting)
                    {
                        if (!isPlayer) return;
                        switch (moveY)
                        {
                            case > 0:
                                _anim.Play("ShootTop");
                                break;
                            case < 0:
                                _anim.Play("ShootBot");
                                break;
                            case 0:
                                _anim.Play("Shoot");
                                break;
                        }
                    }
                    else
                    { 
                        _anim.Play("Idle");
                    }
                }
            }
        }
    }
}
