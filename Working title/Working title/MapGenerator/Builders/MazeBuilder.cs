﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using Microsoft.Xna.Framework;
using The_RPG_thread_game.Utillity;

namespace Working_title.MapGenerator
{
    public class MazeBuilder : Builder
    {
        private List<Cell> Maze = new List<Cell>();
        private HashSet<Cell> SearchMazeList = new HashSet<Cell>();
        private GridMap GridMap;
        private Random Random;

        public MazeBuilder(GridMap gridMap)
        {
            GridMap = gridMap;
            Random = new Random();
        }

        public void Build(BuilderCallback builderCallback)
        {
            AddCell(GetRandomCell());

            while(SearchMazeList.Count > 0)
            {
                Cell CurrentCell = GetCell();

                Cell RandomNeighborCell = GetRandomNeighborCell(CurrentCell);
                RandomNeighborCell.ParentCell = CurrentCell;

                float DistanceToRandomNeighbor = Vector2.Distance(RandomNeighborCell.Position, CurrentCell.Position);

                if (Math.Abs(DistanceToRandomNeighbor) <= 1 && Math.Abs(DistanceToRandomNeighbor) > 0.01f)
                {
                    CurrentCell.AddDirection(RandomNeighborCell.Position - CurrentCell.Position);
                }

                if (!RandomNeighborCell.IsEmpty)
                {
                    AddCell(RandomNeighborCell);
                }
                else
                {
                    RemoveCell(CurrentCell);
                }
            }

            builderCallback(Maze.Cast<BuildObject>().ToList());
        }

        private void AddCell(Cell cell)
        {
            SearchMazeList.Add(cell);
            Maze.Add(cell);
        }

        private void RemoveCell(Cell cell)
        {
            SearchMazeList.Remove(cell);
        }

        private Cell GetRandomCell()
        {
            Vector2 RandomPosition = Size.GetRandomSize(new Size(0, 0),GridMap.Size,Random).ToVector2();

            return new Cell(RandomPosition, new Size(1, 1));
        }

        private Cell GetCell()
        {
            return SearchMazeList.ElementAt(SearchMazeList.Count-1);
        }

        private Cell GetRandomNeighborCell(Cell cell)
        {
            List<Vector2> DirectionsTaken = new List<Vector2>();
            Vector2 CurrentDirection = Vector2.Zero;

            while (Math.Abs(CurrentDirection.X - 9999) > 0.1f && Math.Abs(CurrentDirection.Y - 9999) > 0.1f)
            {
                CurrentDirection = GetRandomDirectionNotTaken(DirectionsTaken);
                Vector2 NewCellPosition = cell.Position + CurrentDirection;
                if (GridMap.IsWithinBounds(NewCellPosition) && GridMap[NewCellPosition] is Empty)
                {
                    Cell NewCell = new Cell(NewCellPosition, cell.Position - NewCellPosition, new Size(1,1));
                    GridMap[NewCellPosition] = NewCell;
                    return NewCell;
                }

                DirectionsTaken.Add(CurrentDirection);
            }

            return new Cell();
        }

        private Vector2 GetRandomDirectionNotTaken(List<Vector2> directionsTaken)
        {
            List<Vector2> DirectionsNotTaken = GetDirectionsNotTaken(GridMap.Directions, directionsTaken);
            if (DirectionsNotTaken.Count > 0)
            {
                int RandomNumber = Random.Next(0, DirectionsNotTaken.Count);
                return DirectionsNotTaken[RandomNumber];
            }
            return new Vector2(9999,9999);
        }

        private List<Vector2> GetDirectionsNotTaken(List<Vector2> directions, List<Vector2> directionsTaken)
        {
            return directions.FindAll(direction => !directionsTaken.Contains(direction));
        }

      




    }
}