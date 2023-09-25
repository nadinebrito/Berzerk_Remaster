using System;
using System.Collections;
using System.Linq;
using Script.Behavior;
using Script.Enemy.Movement;
using Script.Player.Movement;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

namespace Script.Game
{
    public class GameController : MonoBehaviour
    {
        private int _allLife;
        
        private GameObject _mainCamera;
        private GameObject _playerGameObject;
        private GameObject _playerDummy;
        
        private GameObject _doors;
        private GameObject _central;
        private GameObject _goal;
        

        private PlayerMovement _playerMovement;
        
        private int _room;
        private int _actRoom;

        private bool _onMenu;
        private bool _start;
        
        public GameObject enemiePrefab;
        private int _totalEnemies;
        private GameObject newGO;

        
        public List<GameObject> ens = new List<GameObject>();

        private int _actRoomNumber;


        private GameObject _roomScore;
        private int _roomScoreInt;
        private GameObject _totalScore;
        private int _totalScoreInt;
        
        private void Awake()
        {
            _roomScore = GameObject.Find("RoomScore");
            _totalScore = GameObject.Find("TotalScore");

            _roomScoreInt = 0;
            _totalScoreInt = 0;
            
            _roomScore.GetComponent<TMPro.TextMeshProUGUI>().text = Convert.ToString(_roomScoreInt);
            _totalScore.GetComponent<TMPro.TextMeshProUGUI>().text = Convert.ToString(_totalScoreInt);
            
            _roomScore.SetActive(false);
            _totalScore.SetActive(false);
            
            _actRoomNumber = 1;
            _onMenu = true;
            _allLife = 3;
            _mainCamera = GameObject.Find("Main Camera");
            _playerGameObject = GameObject.Find("Player");
            _playerDummy = GameObject.Find("PlayerDummy");
            
            _goal = GameObject.Find("Goal");
            
            _doors = GameObject.Find("Doors");
            _central = GameObject.Find("Central");

            _playerMovement = _playerGameObject.GetComponent<PlayerMovement>();
            
            NewGame();
        }

        private void NewGame()
        {
            
            _actRoomNumber = 1;
            _mainCamera.GetComponent<Camera>().backgroundColor = Color.blue;
            _playerMovement.Revive();
            _allLife = 3;
            _onMenu = true;
            DesableEnemies();
            
            _playerGameObject.SetActive(false);
            _playerDummy.SetActive(true);
            _playerGameObject.transform.position = new Vector3(-7.5f, 0.5f,0f);
            _playerDummy.transform.position = new Vector3(-7.5f, 0.5f,0f);
            _room = Random.Range(0,4);
            DoorsCondition(0);
            GenerateRoom(_room);
            
            
        }

        private void Update()
        {
            
            if (_onMenu)
            {
                _start = Input.GetButtonDown("Shoot");
            }

            if (_onMenu && _start)
            {
                MenuToRoom();
            }
            
        }

        private void MenuToRoom()
        {
            if (_onMenu)
            {
                _roomScoreInt = 0;
                _totalScoreInt = 0;
            
                _roomScore.GetComponent<TMPro.TextMeshProUGUI>().text = Convert.ToString(_roomScoreInt);
                _totalScore.GetComponent<TMPro.TextMeshProUGUI>().text = Convert.ToString(_totalScoreInt);
                _onMenu = false;
                FirstRoom();
            }
        }
        

        private void FirstRoom()
        {
            _playerMovement.Revive();
            StartCoroutine(TimeToPlayer(new Vector3(-7.5f, 0.5f, 0f)));
            _room = Random.Range(0,4);
            DoorsCondition(0);
            GenerateRoom(_room);
            _mainCamera.GetComponent<Camera>().backgroundColor = Color.black;
            

        }

        private IEnumerator TimeToPlayer(Vector3 pos)
        {
            _playerDummy.transform.position = _playerGameObject.transform.position;
            _playerGameObject.SetActive(false);
            _playerDummy.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            DesableEnemies();
            yield return new WaitForSeconds(0.5f);
            _playerDummy.SetActive(false);
            _playerGameObject.transform.position = pos;
            _playerGameObject.SetActive(true);
            EnableEnemies();
        }

        public void DeathOnRoom()
        {
            
            if (_allLife > 0)
            {
                _allLife = _allLife - 1;
                _actRoomNumber = _actRoomNumber+1;
                
                StartCoroutine(ReviverPlayer());
                return;

            }
            if (_allLife == 0)
            {
                StartCoroutine(ResetGame());
            }
        }

        public void EnemyDeathScore()
        {
            //int.TryParse(_roomScore.get)
            _roomScoreInt = _roomScoreInt + 50;
            _roomScore.GetComponent<TextMeshProUGUI>().text = Convert.ToString(_roomScoreInt);
            _roomScore.SetActive(true);
        }

        private IEnumerator ReviverPlayer()
        {
            yield return new WaitForSeconds(3f);
            
            FirstRoom();
            
        }
        private IEnumerator ResetGame()
        {
            yield return new WaitForSeconds(3f);
            
            NewGame();
            
        }

        private void EnableEnemies()
        {
            _totalEnemies = Random.Range(4, 10);
            enemiePrefab.GetComponent<IaSystem>().difficult = _actRoomNumber;
            
            for (int i = 0; i < _totalEnemies; i++)
            {
                ens.Add(Instantiate(enemiePrefab, new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(-3.4f, 3.4f),0),Quaternion.identity));
            }
            
        }
        private void DesableEnemies()
        {
            
            foreach (var t in ens)
            {
                Destroy(t);
            }

            var bullet = GameObject.FindGameObjectsWithTag("BulletEnemy");
            if (bullet == null) return;
            else
            {
                for (int i = 0; i < bullet.Length; i++)
                {
                    Destroy(bullet[i]);
                }
            }
        }
        
        
        private IEnumerator TimeToRoom(int door)
        {
            yield return new WaitForSeconds(0.5f);
            _room = Random.Range(0,4);
            GenerateRoom(_room);
            DoorsCondition(door);
        }
        
        public void NextRoom(string position)
        {
            _actRoomNumber = _actRoomNumber + 1;
            DesableEnemies();
            if (position == "Up")
            {
                StartCoroutine(TimeToRoom(1));
                StartCoroutine(TimeToPlayer(new Vector3(0f, -2.5f, 0)));
            }

            if (position == "Down")
            {
                StartCoroutine(TimeToRoom(2));
                StartCoroutine(TimeToPlayer(new Vector3(0f, 3.5f, 0)));
            }
            
            if (position == "Left")
            {
                StartCoroutine(TimeToRoom(3));
                StartCoroutine(TimeToPlayer(new Vector3(7.75f, 0.5f, 0)));
            }
            
            if (position == "Right")
            {
                StartCoroutine(TimeToRoom(4));
                StartCoroutine(TimeToPlayer(new Vector3(-7.75f, 0.5f, 0)));
            }
        }

        private void GenerateRoom(int code)
        {
      
            if (code == _actRoom )
            {
                if (_actRoom == 3)
                {
                    code = 0;
                }
                else
                {
                    code = code + 1;
                }
                
            }

            _actRoom = code;

            if (code == 0)
            {
                _central.transform.GetChild(0).gameObject.SetActive(false);
                _central.transform.GetChild(1).gameObject.SetActive(false);
                _central.transform.GetChild(2).gameObject.SetActive(false);
                _central.transform.GetChild(3).gameObject.SetActive(false);
                _central.transform.GetChild(4).gameObject.SetActive(false);
                _central.transform.GetChild(5).gameObject.SetActive(false);
                _central.transform.GetChild(6).gameObject.SetActive(false);
                _central.transform.GetChild(7).gameObject.SetActive(true);
                _central.transform.GetChild(8).gameObject.SetActive(true);
                _central.transform.GetChild(9).gameObject.SetActive(true);
                _central.transform.GetChild(10).gameObject.SetActive(true);

            }

            if (code == 1) 
            {
                _central.transform.GetChild(0).gameObject.SetActive(false);
                _central.transform.GetChild(1).gameObject.SetActive(false);
                _central.transform.GetChild(2).gameObject.SetActive(false);
                _central.transform.GetChild(3).gameObject.SetActive(true);
                _central.transform.GetChild(4).gameObject.SetActive(false);
                _central.transform.GetChild(5).gameObject.SetActive(false);
                _central.transform.GetChild(6).gameObject.SetActive(true);
                _central.transform.GetChild(7).gameObject.SetActive(true);
                _central.transform.GetChild(8).gameObject.SetActive(true);
                _central.transform.GetChild(9).gameObject.SetActive(false);
                _central.transform.GetChild(10).gameObject.SetActive(false);
            }

            if (code == 2)
            {
                _central.transform.GetChild(0).gameObject.SetActive(true);
                _central.transform.GetChild(1).gameObject.SetActive(true);
                _central.transform.GetChild(2).gameObject.SetActive(false);
                _central.transform.GetChild(3).gameObject.SetActive(false);
                _central.transform.GetChild(4).gameObject.SetActive(false);
                _central.transform.GetChild(5).gameObject.SetActive(false);
                _central.transform.GetChild(6).gameObject.SetActive(false);
                _central.transform.GetChild(7).gameObject.SetActive(false);
                _central.transform.GetChild(8).gameObject.SetActive(false);
                _central.transform.GetChild(9).gameObject.SetActive(false);
                _central.transform.GetChild(10).gameObject.SetActive(false);
            }
            if (code == 3)
            {
                _central.transform.GetChild(0).gameObject.SetActive(true);
                _central.transform.GetChild(1).gameObject.SetActive(true);
                _central.transform.GetChild(2).gameObject.SetActive(true);
                _central.transform.GetChild(3).gameObject.SetActive(false);
                _central.transform.GetChild(4).gameObject.SetActive(false);
                _central.transform.GetChild(5).gameObject.SetActive(false);
                _central.transform.GetChild(6).gameObject.SetActive(false);
                _central.transform.GetChild(7).gameObject.SetActive(false);
                _central.transform.GetChild(8).gameObject.SetActive(false);
                _central.transform.GetChild(9).gameObject.SetActive(false);
                _central.transform.GetChild(10).gameObject.SetActive(false);
            }
        }

        private void DoorsCondition(int code)
        {
            if (code == 0) //start
            {
                _doors.transform.GetChild(0).gameObject.SetActive(false);
                _goal.transform.GetChild(0).gameObject.SetActive(true);
                
                _doors.transform.GetChild(1).gameObject.SetActive(false);
                _goal.transform.GetChild(1).gameObject.SetActive(true);
                
                _doors.transform.GetChild(2).gameObject.SetActive(true);
                _goal.transform.GetChild(2).gameObject.SetActive(false);
                
                _doors.transform.GetChild(3).gameObject.SetActive(true);
                _goal.transform.GetChild(3).gameObject.SetActive(false);
            }

            if (code == 1) // entrou up
            {
                _doors.transform.GetChild(0).gameObject.SetActive(false);
                _goal.transform.GetChild(0).gameObject.SetActive(true);
                
                _doors.transform.GetChild(1).gameObject.SetActive(true);
                _goal.transform.GetChild(1).gameObject.SetActive(false);
                
                _doors.transform.GetChild(2).gameObject.SetActive(false);
                _goal.transform.GetChild(2).gameObject.SetActive(true);
                
                _doors.transform.GetChild(3).gameObject.SetActive(false);
                _goal.transform.GetChild(3).gameObject.SetActive(true);
            }
            
            if (code == 2) // entrou Down
            {
                _doors.transform.GetChild(0).gameObject.SetActive(true);
                _goal.transform.GetChild(0).gameObject.SetActive(false);
                
                _doors.transform.GetChild(1).gameObject.SetActive(false);
                _goal.transform.GetChild(1).gameObject.SetActive(true);
                
                _doors.transform.GetChild(2).gameObject.SetActive(false);
                _goal.transform.GetChild(2).gameObject.SetActive(true);
                
                _doors.transform.GetChild(3).gameObject.SetActive(false);
                _goal.transform.GetChild(3).gameObject.SetActive(true);
            }
            
            if (code == 3) // entrou Left
            {
                _doors.transform.GetChild(0).gameObject.SetActive(false);
                _goal.transform.GetChild(0).gameObject.SetActive(true);
                
                _doors.transform.GetChild(1).gameObject.SetActive(false);
                _goal.transform.GetChild(1).gameObject.SetActive(true);
                
                _doors.transform.GetChild(2).gameObject.SetActive(true);
                _goal.transform.GetChild(2).gameObject.SetActive(false);
                
                _doors.transform.GetChild(3).gameObject.SetActive(false);
                _goal.transform.GetChild(3).gameObject.SetActive(true);
            }
            
            if (code == 4) // entrou Right
            {
                _doors.transform.GetChild(0).gameObject.SetActive(false);
                _goal.transform.GetChild(0).gameObject.SetActive(true);
                
                _doors.transform.GetChild(1).gameObject.SetActive(false);
                _goal.transform.GetChild(1).gameObject.SetActive(true);
                
                _doors.transform.GetChild(2).gameObject.SetActive(false);
                _goal.transform.GetChild(2).gameObject.SetActive(true);
                
                _doors.transform.GetChild(3).gameObject.SetActive(true);
                _goal.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
    }
}
 
