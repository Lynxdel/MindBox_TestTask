using RouteSorting.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RouteSorting
{
    public class RouteSorterListImpl : IRouteSorter
    {
        /// <summary>
        /// Метод упорядочивания маршрутных карточек "в лоб"
        /// Сложность - O(n^2)
        /// </summary>
        /// <param name="SrcCardsList">Список неупорядоченных маршрутных карточек</param>
        public List<RouteCard> MakeRoute(List<RouteCard> SrcCardsList)
        {
            List<RouteCard> result = null;

            if (SrcCardsList != null)
            {
                // исходная карточка маршрута
                var srcCard = SrcCardsList
                                .Where(c => !SrcCardsList.Any(cn => c.DepPlace == cn.DestPlace))
                                .FirstOrDefault();

                if (srcCard != null)
                {
                    int iterations = 1;

                    result = new List<RouteCard> { srcCard };

                    var nextCard = SrcCardsList
                                    .Where(c => c.DepPlace == srcCard.DestPlace)
                                    .FirstOrDefault();

                    // итерации до тех пор, пока будут находиться карточки для продолжения маршрутов
                    while (nextCard != null)
                    {
                        iterations++;

                        if (iterations > SrcCardsList.Count)
                        {
                            throw new Exception("Loop found in route cards list");
                        }

                        result.Add(nextCard);

                        nextCard = SrcCardsList
                                    .Where(c => c.DepPlace == nextCard.DestPlace)
                                    .FirstOrDefault();
                    }

                    if (result.Count != SrcCardsList.Count)
                    {
                        throw new Exception("Gap found in route cards list");
                    }
                }
                else
                {
                    throw new Exception("No primary departure place found in route cards list");
                }
            }

            return result;
        }
    }
}