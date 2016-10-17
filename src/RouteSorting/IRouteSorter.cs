using RouteSorting.Entities;
using System.Collections.Generic;

namespace RouteSorting
{
    public interface IRouteSorter
    {
        List<RouteCard> MakeRoute(List<RouteCard> SrcCardsList);
    }
}