﻿namespace Working_title.MapGenerator
{
    public class DeadEndRemover
    {
        private GridMap GridMap;

        public DeadEndRemover(GridMap gridMap)
        {
            GridMap = gridMap;
        }

        public void Start()
        {
            foreach (var BuildObject in GridMap.ObjectsAsList)
            {
                Cell CurrentCell = BuildObject as Cell;

                while (CurrentCell?.Directions.Count == 1 && !CurrentCell.IsRoomExit)
                {
                    GridMap[CurrentCell.Position] = new Wall(CurrentCell.Position,CurrentCell.Size);
                    Cell CellCommingFrom = CurrentCell.ParentCell;
                    if (CellCommingFrom.IsRoomExit)
                    {
                        break;
                    }
                    CellCommingFrom.RemovePasssageTo(CurrentCell);
                    CurrentCell = CellCommingFrom;
                }
            }
        }
    }
}