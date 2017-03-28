using System;

namespace HtmlToPdfSample.Templates.PersonsFavouriteCities
{
    public class PersonsFavouriteCitiesModel
    {
        public PersonsFavouriteCitiesModel(string name, DateTime dateOfBirth, params string[] favouriteCities)
        {
            DateOfBirth = dateOfBirth;
            Name = name;
            FavouriteCities = favouriteCities;
        }

        public string Name { get; }
        public DateTime DateOfBirth { get; }
        public string[] FavouriteCities { get; }
    }
}