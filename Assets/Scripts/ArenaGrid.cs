using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaGrid : ArenaBase, IFreezed
{
    public int horda;
    
    CountdownTimer _timer, _Freezetime;
    [SerializeField] float _timeSpawn;
    public delegate void DelegateUpdate();
    public DelegateUpdate delegateUpdate;


    private void Start()
    {
        
        delegateUpdate = NormalUpdate;
        GameManager.instance.pj.theWorld += StoppedTime;
        _timer = new CountdownTimer(_timeSpawn);
        _timer.OnTimerStop = IniciarHorda;
        _Freezetime = new CountdownTimer(3);
        _Freezetime.OnTimerStop = BackToNormal;
    }
    
    private void Update()
    {
        delegateUpdate.Invoke();
    }
    
    public override void IniciarHorda()
    {
        _arenaEmpezada = true;
        for(int i = 0; i < spawnPoints.Length; i++)
        {
            enemigos[0].SpawnEnemy(spawnPoints[i].transform);
        }
    }

    public void StoppedTime()
    {
        _timer.Pause();
        delegateUpdate = Freezed;
        _Freezetime.Start();
        
    }

    public void NormalUpdate()
    {
        if (enemigosEnLaArena.Count == 0 && _arenaEmpezada)
        {
            _arenaEmpezada = false;
            horda++;
            _timer.Start();
        }
        _timer.Tick(Time.deltaTime);
    }

    public void Freezed()
    {
        _Freezetime.Tick(Time.deltaTime);
    }

    public void BackToNormal()
    {
        delegateUpdate = NormalUpdate;
        _timer.Start();
    }
}
