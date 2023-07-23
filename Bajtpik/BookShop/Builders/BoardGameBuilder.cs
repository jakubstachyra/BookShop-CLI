using Bajtpik.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bajtpik.Data.Builders
{
    public class BoardGameBuilder : IBuilder
    {
        protected string title;
        protected int? minPlayers;
        protected int? maxPlayers;
        protected int difficulty;
        protected List<IAuthor> authors;

        public  IEntity Build()
        {
            return new BoardGame(title, minPlayers, maxPlayers, difficulty, authors);
        }

        public bool SetField(string fieldName, string value)
        {
            switch (fieldName.ToLower())
            {
                case "title":
                    title = value;
                    return true;

                case "minplayers":
                    if (int.TryParse(value, out int minPlayersValue))
                    {
                        minPlayers = minPlayersValue;
                        return true;
                    }
                    break;

                case "maxplayers":
                    if (int.TryParse(value, out int maxPlayersValue))
                    {
                        maxPlayers = maxPlayersValue;
                        return true;
                    }
                    break;

                case "difficulty":
                    if(int.TryParse(value, out int difficultyValue))
                    {
                        difficulty = difficultyValue;
                    }
                    return true;
            }

            return false;
        }

        public List<string> GetFieldNames()
        {
            return new List<string> { "title", "minPlayers", "maxPlayers", "difficulty", "authors" };
        }
    }
    public class BoardGameListOfTupleBuilder : BoardGameBuilder
    {
        public new IEntity Build()
        {
            return new BoardgameListOfTuple(title, minPlayers, maxPlayers, difficulty, authors);
        }
    }
}
