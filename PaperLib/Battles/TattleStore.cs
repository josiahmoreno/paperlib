using Enemies;
using System;
using System.Collections.Generic;

namespace Battle
{
    internal class TattleStore : ITattleStore
    {
        private Dictionary<Type, GameText> dictionary = new Dictionary<Type, GameText>()
        {
            { typeof(Enemies.Magikoopa), new GameText("a","b","c")},
             { typeof(Enemies.GoombaKing), new GameText("a","b","c","d")},
             { typeof(Fuzzie), new GameText("a","b","c","d")}
        };


        public GameText FetchGameText(Enemy enemy)
        {
            return dictionary[enemy.GetType()];
        }
    }
}