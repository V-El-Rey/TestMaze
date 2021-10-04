using System.Collections.Generic;
using System.Linq;
using Base;
using Data;
using PathLineDrawController;
using Struct;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : BaseController
{
    private PlayerView _playerView;
    private LineDrawerView _lineDrawerView;

    private CellCoordinate _currentPosition;
    private CellCoordinate _nextStepPosition;
    private CellCoordinate _targetPosition;

    private LineDrawerController _lineDrawer;

    public UnityAction<CellCoordinate, CellCoordinate> StartPathfinding;

    private Stack<CellCoordinate> _path = new Stack<CellCoordinate>();
    private float _stepDelay;
    private float _timeCounter;
    
    public PlayerController(PlayerView player,LineDrawerView lineDrawer, GameData data)
    {
        _playerView = player;
        _stepDelay = data.movementStepDelay;
        _timeCounter = _stepDelay;
        _lineDrawerView = lineDrawer;
    }

    public override void StartExecute()
    {
        base.StartExecute();
        _lineDrawer = new LineDrawerController(_lineDrawerView);
    }

    public override void UpdateExecute()
    {
        if (_path.Count <= 0)
        {
            if (_lineDrawerView.gameObject.activeInHierarchy)
            {
                 _lineDrawer.SetOff();
            }
            return;
        }

        _timeCounter--;
        
        if (_timeCounter <= 0.0f)
        {
            _nextStepPosition = _path.Pop();
            var nextMove = CalculateMovementDirection(_nextStepPosition);
            _currentPosition = _nextStepPosition;
            MovePlayer(nextMove);
            _timeCounter = _stepDelay;
        }
    }


    private void MovePlayer(PlayerMovementDirection direction)
    {
        switch (direction)
        {
            case(PlayerMovementDirection.Right):
                _playerView.playerTransform.position += Vector3.right;
                break;
            case(PlayerMovementDirection.Left):
                _playerView.playerTransform.position += Vector3.left;
                break;
            case(PlayerMovementDirection.Up):
                _playerView.playerTransform.position += Vector3.up;
                break;
            case(PlayerMovementDirection.Down):
                _playerView.playerTransform.position += Vector3.down;
                break;
            case PlayerMovementDirection.None:
                break;
        }
    }

    private PlayerMovementDirection CalculateMovementDirection(CellCoordinate nextStep)
    {
        if (nextStep.xCoordinate > _currentPosition.xCoordinate)
        {
            return PlayerMovementDirection.Right;
        }

        if (nextStep.yCoordinate > _currentPosition.yCoordinate)
        {
            return PlayerMovementDirection.Up;
        }

        if (nextStep.xCoordinate < _currentPosition.xCoordinate)
        {
            return PlayerMovementDirection.Left;
        }

        if (nextStep.yCoordinate < _currentPosition.yCoordinate)
        {
            return PlayerMovementDirection.Down;
        }

        return PlayerMovementDirection.None;
    }

    public void GetRandomCoordinatesAndSpawnPlayer(CellCoordinate coordinate)
    {
        _path.Clear();
        if (_lineDrawerView.gameObject.activeInHierarchy)
        {
            _lineDrawer.SetOff();
        }
        _playerView.playerTransform.position = new Vector3(coordinate.xCoordinate, coordinate.yCoordinate, -0.7f);
        _playerView.gameObject.SetActive(true);
        _currentPosition = coordinate;
    }
    
    public void SetTargetPosition(CellCoordinate target)
    {
        _targetPosition = target;
        StartPathfinding?.Invoke(_currentPosition, _targetPosition);
    }

    public void SetPath(Stack<CellCoordinate> path)
    {
        _path.Clear();
        while (path.Count != 0)
        {
            _path.Push(path.Pop());
        }
        _lineDrawer.DrawLine(_path);
    }
}
