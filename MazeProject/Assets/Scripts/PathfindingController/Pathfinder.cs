using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Base;
using Struct;
using UnityEngine;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

namespace PathfindingController
{
    public class Pathfinder : BaseController
    {
        private CellCoordinate _currentPosition;
        private CellCoordinate _targetPosition;
        private CellCoordinate _nextStepPosition;

        private Cell _current;
        private Cell _target;
        private Cell _nextStep;

        private Stack<CellCoordinate> _path = new Stack<CellCoordinate>();
        private Stack<Cell> _cellsStack = new Stack<Cell>();
        private List<Cell> _neighborCells = new List<Cell>();
        private List<List<Cell>> _mazeColumns = new List<List<Cell>>();

        public UnityAction<Stack<CellCoordinate>> SendPath;

        public Pathfinder()
        {
        }
        
        //test

        public override void UpdateExecute()
        {
            base.UpdateExecute();
            if (Input.GetMouseButtonDown(0))
            {
                foreach (var VARIABLE in _path)
                {
                    //Debug.Log($"Path step: {VARIABLE.xCoordinate}:{VARIABLE.yCoordinate}");
                }
            }

        }

        //test

        public void SetStartAndTarget(CellCoordinate start, CellCoordinate target)
        {
            _currentPosition = start;
            _targetPosition = target;
            _current = ConvertToCell(_currentPosition);
            _target = ConvertToCell(_targetPosition);
            ResetVisited();
            CalculatePath();
        }

        public void SetCellsArray(List<List<Cell>> mazeColumns)
        {
            _mazeColumns = mazeColumns;
        }
        
        public void CalculatePath()
        {
            _path.Clear();
            _cellsStack.Clear();
            _current = ConvertToCell(_currentPosition); 
            _current.Visited = true;


            do
            {
                GetAllNeighborCells(_current, 1);
                if (IsCellGotUnvisitedNeighbors(_current))
                {
                    _cellsStack.Push(_current);
                    _path.Push(ConvertToCoordinate(_current));
                    _nextStep = GetRandomNeighbor(_current);
                    _current = _nextStep;
                    _current.Visited = true;
                }
                else
                {
                    if (_cellsStack.Count > 0)
                    {
                        _current = _cellsStack.Pop();
                        _path.Pop();
                        _current.Visited = true;
                    }
                }
            } while (!(_current.XCoordinate == _target.XCoordinate && _current.YCoordinate == _target.YCoordinate));

            _path.Push(ConvertToCoordinate(_target));
            
            SendPath?.Invoke(_path);
        }

        private void GetAllNeighborCells(Cell cell, int step)
        {
            _neighborCells.Clear();
            if (cell.XCoordinate + step < _mazeColumns.Count)
            {
                var rightNeighbor = _mazeColumns[cell.XCoordinate + step][cell.YCoordinate];
                if (!rightNeighbor.Visited && rightNeighbor.Type == CellType.Cell) _neighborCells.Add(rightNeighbor);
            }

            if (cell.XCoordinate - step > 0)
            {
                var leftNeighbor = _mazeColumns[cell.XCoordinate - step][cell.YCoordinate];
                if (!leftNeighbor.Visited && leftNeighbor.Type == CellType.Cell) _neighborCells.Add(leftNeighbor);
            }

            if (cell.YCoordinate + step < _mazeColumns[cell.XCoordinate].Count)
            {
                var upNeighbor = _mazeColumns[cell.XCoordinate][cell.YCoordinate + step];
                if (!upNeighbor.Visited && upNeighbor.Type == CellType.Cell) _neighborCells.Add(upNeighbor);
            }

            if (cell.YCoordinate - step >= 0)
            {
                var downNeighbor = _mazeColumns[cell.XCoordinate][cell.YCoordinate - step];
                if (!downNeighbor.Visited && downNeighbor.Type == CellType.Cell) _neighborCells.Add(downNeighbor);
            }
        }

        private Cell GetRandomNeighbor(Cell cell)
        {
            var rand = Random.Range(0, _neighborCells.Count);
            return _neighborCells[rand];
        }

        private Cell ConvertToCell(CellCoordinate cellCoordinate) => _mazeColumns[cellCoordinate.xCoordinate][cellCoordinate.yCoordinate];

        private CellCoordinate ConvertToCoordinate(Cell cell) => new CellCoordinate(cell.XCoordinate, cell.YCoordinate);
        
        private bool IsCellGotUnvisitedNeighbors(Cell cell)
        {
            GetAllNeighborCells(cell, 1);
            return _neighborCells.Count > 0;
        }
        
        private void ResetVisited()
        {
            foreach (var columns in _mazeColumns)
            {
                foreach (var cell in columns)
                {
                    cell.Visited = cell.Type == CellType.Wall;
                }
            }
        }
    }
}
