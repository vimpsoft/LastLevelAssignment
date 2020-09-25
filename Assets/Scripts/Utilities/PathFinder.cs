using System.Collections.Generic;

/// <summary>
/// Статический класс, потому что обращения к его методам будут быстрее чем к методам экземпляра
/// </summary>
public static class PathFinder
{
    //Массив одномерный, чтобы не плодить много мелких объектов
    private static NodeInfo[] _infos;

    /// <summary>
    /// Принимаем стек а не возвращаем, чтобы не мусорить - каждый объект будет переиспользовать свой стэк
    /// </summary>
    /// <param name="result">Стек для заполнения</param>
    /// <param name="map">Карта</param>
    /// <param name="from">Начальная нода</param>
    /// <param name="to">Конечная нода</param>
    public static void FindPath(Stack<Node> result, Map map, Node from, Node to)
    {

    }
}
