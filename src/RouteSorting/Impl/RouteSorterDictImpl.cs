using RouteSorting.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RouteSorting
{
    /// <summary>
    /// Метод упорядочивания маршрутных карточек с использованием словаря
    /// Сложность - O(n)
    /// </summary>
    /// <param name="SrcCardsList">Список неупорядоченных маршрутных карточек</param>
    public class RouteSorterDictImpl : IRouteSorter
    {
        public List<RouteCard> MakeRoute(List<RouteCard> SrcCardsList)
        {
            List<RouteCard> result = null;

            if (SrcCardsList != null)
            {
                // исходная карточка маршрута
                var currentCard = SrcCardsList
                                    .Where(c => !SrcCardsList.Any(cn => c.DepPlace == cn.DestPlace))
                                    .FirstOrDefault();

                if (currentCard != null)
                {
                    // преобразуем список в словарь, предварительно задав для него требуемую емкость
                    var dict = new Dictionary<string, string>(SrcCardsList.Count);

                    foreach (var card in SrcCardsList)
                    {
                        dict.Add(card.DepPlace, card.DestPlace);
                    }

                    // помещаем исходную карточку в результирующий список
                    result = new List<RouteCard> { currentCard };

                    // формируем итоговый список, перебирая данные словаря
                    for (var i = 0; i < SrcCardsList.Count - 1; i++)
                    {
                        currentCard = new RouteCard
                        {
                            DepPlace = currentCard.DestPlace,
                            DestPlace = dict[currentCard.DestPlace]
                        };

                        result.Add(currentCard);
                    }
                }
                else
                {
                    throw new ArgumentException("No primary departure place found in route cards list");
                }
            }

            return result;
        }
    }
}