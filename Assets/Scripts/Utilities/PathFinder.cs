using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Статический класс, потому что обращения к статически методам быстрее чем к методам экземпляра, а экземплярных методов нам тут не нужно.
/// </summary>
public static class PathFinder
{
    private struct NodeInfo
    {
        public bool IsAnObstacle; //Это препятствие
        public bool IsUtilized; //Эта нода забрана в результат
        public bool IsDone; //Эта нода обработана окончательно
        public bool IsCalculated; //Для этой ноды были просчитаны веса
        public float GCost; //Расстояние от начальной ноды
        public float HCost; //Расстояние до конечной ноды
        public float FCost; //Сумма этих расстояний
        public Vector2Int Coordinates; //Координаты ноды
    }

    public static List<Vector2Int> FindPath(IMap map, Vector2Int start, Vector2Int finish)
    {
        Debug.Log($"Finding path: from {start} to {finish}");
        /*
         * Тут я мусорю листами и массивами, а вообще надо их держать в кешах соответствующих экземпляров, ищущих пути
         * Но тут у нас экземпляров нет и нас дергают редко, поэтому не заморачиваемся.
         */
        var result = new List<Vector2Int>();
        var infos = new NodeInfo[map.Width * map.Height]; //Служебная структура, нужная только для поиска пути.
        for (int x = 0; x < map.Width; x++)
        {
            for (int y = 0; y < map.Height; y++)
            {
                infos[x * map.Height + y] = new NodeInfo();
                infos[x * map.Height + y].Coordinates = new Vector2Int(x, y);
                if (map[x, y].IsTraversable)
                    continue;
                infos[x * map.Height + y].IsAnObstacle = true;
            }
        }

        if (start == finish)
            return result;

        //Собственно алгоритм

        var currentNodeCoordinates = start;

        var calculatedButNotDoneNodes = new Dictionary<Vector2Int, NodeInfo>(); //Вспомогательный словарь, для уменьшения сложности алгоритма
        calculatedButNotDoneNodes.Add(currentNodeCoordinates, infos[currentNodeCoordinates.x * map.Width + currentNodeCoordinates.y]); //Добавляем начальную ноду в список обработанных
        infos[currentNodeCoordinates.x * map.Height + currentNodeCoordinates.y].IsCalculated = true;

        var (finishX, finishY) = finish;
        while (currentNodeCoordinates != finish)
        {
            calculateCurrentNodesCosts();

            var foundNextStep = false;
            var minFCost = float.MaxValue;
            var minHCost = float.MaxValue;
            var minCostCoords = default(Vector2Int);
            foreach (var (_, nextPossibleNode) in calculatedButNotDoneNodes) //Перебираем в поисках минимального FCost и HCost.
            {
                if (nextPossibleNode.IsAnObstacle)
                    continue;
                if (nextPossibleNode.FCost < minFCost)
                {
                    minFCost = nextPossibleNode.FCost;
                    minHCost = nextPossibleNode.HCost;
                    minCostCoords = nextPossibleNode.Coordinates;
                    foundNextStep = true;
                }
                else if (nextPossibleNode.FCost == minFCost && nextPossibleNode.HCost < minHCost)
                {
                    minHCost = nextPossibleNode.HCost;
                    minCostCoords = nextPossibleNode.Coordinates;
                    foundNextStep = true;
                }
            }
            if (!foundNextStep)
                return result;
            currentNodeCoordinates = minCostCoords;

            void calculateCurrentNodesCosts()
            {
                infos[currentNodeCoordinates.x * map.Width + currentNodeCoordinates.y].IsDone = true;
                calculatedButNotDoneNodes.Remove(currentNodeCoordinates);

                var (currentNodeX, currentNodeY) = currentNodeCoordinates;
                var currentGCost = infos[currentNodeX * map.Width + currentNodeY].GCost;

                doNeighbour(1, 0);
                doNeighbour(1, 1);
                doNeighbour(0, 1);
                doNeighbour(-1, 0);
                doNeighbour(-1, -1);
                doNeighbour(0, -1);
                doNeighbour(-1, 1);
                doNeighbour(1, -1);

                void doNeighbour(int offsetX, int offsetY)
                {
                    var x = offsetX + currentNodeX;
                    var y = offsetY + currentNodeY;
                    if (x < 0 || x >= map.Width || y < 0 || y >= map.Height)
                        return;
                    var index = x * map.Width + y;
                    //if (infos[index].IsDone) //Уже пройденные ноды не трогаем
                    //    return;
                    if (infos[index].IsAnObstacle)
                        return;

                    var newGCost = currentGCost + (Mathf.Abs(offsetX) + Mathf.Abs(offsetY) == 1 ? 1f : 1.414213562f);
                    var newHCost = Mathf.Sqrt(Mathf.Pow(x - finishX, 2) + Mathf.Pow(y - finishY, 2));
                    var newFCost = newGCost + newHCost;
                    if (!infos[index].IsCalculated)
                    {
                        infos[index].GCost = newGCost;
                        infos[index].HCost = newHCost;
                        infos[index].FCost = newFCost;
                    }
                    else
                    {
                        //В другие проходы эти значения могу быть ниже
                        if (newGCost < infos[index].GCost)
                        {
                            infos[index].GCost = newGCost;
                            infos[index].FCost = newFCost;
                        }
                    }

                    infos[index].IsCalculated = true;
                    var coords = infos[index].Coordinates;
                    if (!infos[index].IsDone && !calculatedButNotDoneNodes.ContainsKey(coords))
                        calculatedButNotDoneNodes.Add(coords, infos[index]);
                }
            }
        }

        //Нашли путь, осталось только пройти обратно, по пути минимального FCost'а
        while (currentNodeCoordinates != start)
        {
            infos[currentNodeCoordinates.x * map.Width + currentNodeCoordinates.y].IsUtilized = true;

            result.Add(currentNodeCoordinates);

            //Проходимся по соседям
            var minGCost = float.MaxValue;
            var minCostCoordinates = default(Vector2Int);
            for (int xOffset = -1; xOffset < 2; xOffset++)
            {
                for (int yOffset = -1; yOffset < 2; yOffset++)
                {
                    var x = currentNodeCoordinates.x + xOffset;
                    var y = currentNodeCoordinates.y + yOffset;
                    if (xOffset == 0 && yOffset == 0)
                        continue;
                    if (x < 0 || y < 0)
                        continue;
                    if (x >= map.Width || y >= map.Width)
                        continue;
                    var info = infos[x * map.Width + y];
                    if (info.IsAnObstacle)
                        continue;
                    if (info.IsUtilized)
                        continue;
                    if (!info.IsDone)
                        continue;
                    if (info.GCost < minGCost)
                    {
                        minGCost = info.GCost;
                        minCostCoordinates = info.Coordinates;
                    }
                }
            }
            currentNodeCoordinates = minCostCoordinates;
        }

        return result;
    }
}
