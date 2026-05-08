using System.Collections.Generic;
using System.Linq;
using Data.Runtime;
using TMPro;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{

    #region Unity API

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void FixedUpdate()
    {
        _fpsCount.text = _fps.ToString("F0");
    }

    private void Update()
    {
        _fps = 1 / Time.deltaTime;
    }
    
    
    #endregion

    #region Main Methods

    public void SendRaycast()
    {
        _gameObjetHits.Clear();
        for (int a = 0; a < _gridWidth; a++)
        {
            for (int b = 0; b < _gridHeigth; b++)
            {
                Vector3 rayDirection = new Vector3(a * _cellSize, b * _cellSize, 0);
                Ray r = _cam.ScreenPointToRay(rayDirection);
                RaycastHit hit;

                if (Physics.Raycast(r.origin, r.direction, out hit, 20f, _layerMask))
                {
                    _gameObjetHits.Add(hit.collider.gameObject);
                }
            }
        }
        GetObjectWithMaxHit(_gameObjetHits);
    }

    #endregion

    #region Utils

    private void GetObjectWithMaxHit(List<GameObject> list)
    {
        //var newList = list.OrderBy(x => x.name);
        if (list.Count == 0) return;
        
        var countList  = 
            list
                .GroupBy(f => f)
                .Select(g => new { Valeur = g.Key, Nombre = g.Count() })
                .OrderByDescending(x => x.Nombre);

        _maxHitData = new MaxHitForGameObjectData(countList.First().Valeur, countList.First().Nombre);
    }

    #endregion
        
    #region Privates

        private Camera _cam;
        private int _gridWidth = 48;
        private int _gridHeigth = 27;
        private float _cellSize = 40f;
        [SerializeField] private LayerMask _layerMask;
        private List<GameObject> _gameObjetHits = new List<GameObject>();
        
        [SerializeField] private TMP_Text _fpsCount;
        private float _fps;

        private MaxHitForGameObjectData _maxHitData;

        #endregion

}