using System.Collections.Generic;
using Base;
using Struct;
using UnityEngine;



internal class MazeGenerator : BaseController
{
    //public MazeSettings Settings { get; set; }

    private MazeSettings _settings;
    private MazePrefabs _prefabs;
    private Transform _rootMaze;

    private int _widthWithWalls;
    private int _heightWithWalls;

    private Cell _currentCell;
    
    private List<List<Cell>> _mazeColumns = new List<List<Cell>>();
    private List<GameObject> _mazeObjects = new List<GameObject>();
    private List<Cell> _unvisitedCells = new List<Cell>();
    private Stack<Cell> _cellsStack = new Stack<Cell>();
    private List<Cell> _neighborCells = new List<Cell>();


    public MazeGenerator(MazeSettings defaultSettings, MazePrefabs prefabs, Transform rootMaze)
    {
        _settings = defaultSettings;
        _prefabs = prefabs;
        _rootMaze = rootMaze;
    }

    public override void StartExecute()
    {
        base.StartExecute();
        SpawnMaze(_settings);
        Debug.Log("Default maze created");
    }

    public void SpawnMaze(MazeSettings settings)
    {
        if (settings.Height != 0 && settings.Width != 0)
        {
            _heightWithWalls = settings.Height + settings.Height + 1;
            _widthWithWalls = settings.Width + settings.Width + 1;
            ReturnGridToPool();
            SpawnGrid(_widthWithWalls, _heightWithWalls);
            GenerateMaze();
            Debug.Log($"Maze created: Width/Height : {settings.Height} / {settings.Width}");
        }
    }

    private void SpawnGrid(int width, int height)
    {
        _unvisitedCells.Clear();


        for (int x = 0; x < width; x++)
        {
            var mazeRow = new List<Cell>();

            for (int y = 0; y < height; y++)
            {
                if (x % 2 != 0 && y % 2 != 0)
                {
                    if (x < _widthWithWalls - 1 && y < _heightWithWalls - 1)
                    {
                        var cellPrefab = PoolManager.GetObjectFromPool(_prefabs.Cell);
                        cellPrefab.transform.parent = _rootMaze;
                        cellPrefab.transform.position = new Vector3(x, y, 0.0f);
                        var Cell = new Cell(CellType.Cell, cellPrefab, x, y, false);
                        _unvisitedCells.Add(Cell);
                        mazeRow.Add(Cell);
                        _mazeObjects.Add(Cell.CellPrefab);
                    }
                }
                else
                {

                    var cellPrefab = PoolManager.GetObjectFromPool(_prefabs.Wall);
                    var cellWall = new Cell(CellType.Wall, cellPrefab, x, y, true);
                    cellPrefab.transform.parent = _rootMaze;
                    cellPrefab.transform.position = new Vector3(x, y, 0.0f);
                    mazeRow.Add(cellWall);
                    _mazeObjects.Add(cellWall.CellPrefab);
                }
            }

            _mazeColumns.Add(mazeRow);
        }
    }

    private void ReturnGridToPool()
    {
        foreach (var objects in _mazeObjects)
        {
           PoolManager.ReturnToPool(objects);
        }
        _mazeObjects.Clear();
        _mazeColumns.Clear();
    }
    
    
// Debug.Log($"Neighbor cell coordinates: {neighborCell.XCoordinate}:{neighborCell.ZCoordinate}");
// Debug.Log($"Selected cell coordinates: {currentCell.XCoordinate}:{currentCell.ZCoordinate}");

 // cellToRemove = _unvisitedCells.Find(x => currentCell.CellPrefab);
 // _unvisitedCells.Remove(cellToRemove);

    private void GenerateMaze()
    {
        _cellsStack.Clear();
        _currentCell = _mazeColumns[1][1];
        _currentCell.Visited = true;
        _unvisitedCells.Remove(_unvisitedCells.Find(x => _currentCell.CellPrefab));


        while (_unvisitedCells.Count > 0)
        {
            GetAllNeighborCells(_currentCell);
    
            if (IsCellGotUnvisitedNeighbors(_currentCell))
            {
                _cellsStack.Push(_currentCell);
                var neighborCell = SelectRandomNeighbor(_currentCell);

                RemoveWall(_currentCell,neighborCell);
                
                _currentCell = neighborCell;
                _currentCell.Visited = true;
                _unvisitedCells.Remove(_unvisitedCells.Find(x => _currentCell.CellPrefab));
         

            }
            else
            {
                if (_cellsStack.Count > 0)
                {
                    _currentCell = _cellsStack.Pop();
                    _currentCell.Visited = true;
              
                }
                else
                {
                    break;
                }
            }
        }
    }

    private Cell SelectRandomNeighbor(Cell cell)
    {
        var randNumber = Random.Range(0, _neighborCells.Count); 
        return _neighborCells[randNumber];
    }


    private void RemoveWall(Cell current, Cell neighbor)
    {
        if (current.XCoordinate > neighbor.XCoordinate)
        {
            SwapWallWithCell(current.XCoordinate - 1, current.YCoordinate);
            return;
        }

        if (current.XCoordinate < neighbor.XCoordinate)
        {
            SwapWallWithCell(current.XCoordinate + 1, current.YCoordinate);
            return;
        }

        if (current.YCoordinate > neighbor.YCoordinate)
        {
            SwapWallWithCell(current.XCoordinate, current.YCoordinate - 1);
            return;
        }

        if (current.YCoordinate < neighbor.YCoordinate)
        {
            SwapWallWithCell(current.XCoordinate, current.YCoordinate + 1);
        }
    }


    private void SwapWallWithCell(int xCoordinate, int yCoordinate)
    {

        _mazeColumns[xCoordinate][yCoordinate].CellPrefab.SetActive(false);
        _mazeColumns[xCoordinate][yCoordinate].Type = CellType.Cell;
        var cellPrefab = PoolManager.GetObjectFromPool(_prefabs.Cell);
        cellPrefab.transform.position = new Vector3(xCoordinate, yCoordinate, 0.0f);
        cellPrefab.transform.parent = _rootMaze;
      

        _mazeColumns[xCoordinate][yCoordinate].CellPrefab = cellPrefab;
        _mazeColumns[xCoordinate][yCoordinate].XCoordinate = xCoordinate;
        _mazeColumns[xCoordinate][yCoordinate].YCoordinate = yCoordinate;
        _mazeColumns[xCoordinate][yCoordinate].Visited = true;
   
        
        _mazeObjects.Add(_mazeColumns[xCoordinate][yCoordinate].CellPrefab);
    }

    private bool IsCellGotUnvisitedNeighbors(Cell cell)
    {
        GetAllNeighborCells(cell);
        //Debug.LogError(_neighborCells.Count);
        return _neighborCells.Count > 0;
    }

    private void GetAllNeighborCells(Cell cell)
    {
        _neighborCells.Clear();
        if (cell.XCoordinate + 2 < _mazeColumns.Count)
        {
            var rightNeighbor = _mazeColumns[cell.XCoordinate + 2][cell.YCoordinate];
            if(!rightNeighbor.Visited && rightNeighbor.Type == CellType.Cell) _neighborCells.Add(rightNeighbor);
        }

        if (cell.XCoordinate - 2 > 0)
        {
            var leftNeighbor = _mazeColumns[cell.XCoordinate - 2][cell.YCoordinate];
            if(!leftNeighbor.Visited && leftNeighbor.Type == CellType.Cell) _neighborCells.Add(leftNeighbor);
        }

        if (cell.YCoordinate + 2 < _mazeColumns[cell.XCoordinate].Count)
        {
            var upNeighbor = _mazeColumns[cell.XCoordinate][cell.YCoordinate + 2];
            if(!upNeighbor.Visited && upNeighbor.Type == CellType.Cell) _neighborCells.Add(upNeighbor);
        }

        if (cell.YCoordinate - 2 >= 0)
        {
            var downNeighbor = _mazeColumns[cell.XCoordinate][cell.YCoordinate - 2];
            if(!downNeighbor.Visited && downNeighbor.Type == CellType.Cell) _neighborCells.Add(downNeighbor);
        }
    }
}

