using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using Family.Models;
using AutoMapper;

namespace Family.Controllers
{
    public class Manager
    {
        // Data store
        private ApplicationDbContext ds = new ApplicationDbContext();

        public Manager()
        {
            // Default constructor, if needed
        }

        // ########################################

        // Get-all, get-all-filtered, get-one, add-new, edit-existing, delete-one
        // With associated objects if necessary

        public IEnumerable<PersonBase> AllPersons()
        {
            var fetchedObjects = ds.Persons.OrderBy(p => p.FamilyName).ThenBy(p => p.GivenNames);

            return Mapper.Map<IEnumerable<PersonBase>>(fetchedObjects);
        }

        public PersonBase PersonById(int id)
        {
            var fetchedObject = ds.Persons.Find(id);

            return (fetchedObject == null) ? null : Mapper.Map<PersonBase>(fetchedObject);
        }

        public PersonBaseWithRelations PersonByIdWithRelations(int id)
        {
            var fetchedObject = ds.Persons
                .Include("Father")
                .Include("Mother")
                .Include("Children")
                .SingleOrDefault(p => p.Id == id);

            return (fetchedObject == null) ? null : Mapper.Map<PersonBaseWithRelations>(fetchedObject);
        }

        public IEnumerable<PersonList> PossibleFathers(DateTime personBirthDate)
        {
            var parentBirthDate = personBirthDate.AddYears(-15);

            var fetchedObjects = ds.Persons
                .Where(p => p.BirthDate <= parentBirthDate & p.Gender == "Male")
                .OrderBy(p => p.FamilyName).ThenBy(p => p.GivenNames);

            return Mapper.Map<IEnumerable<PersonList>>(fetchedObjects);
        }

        public IEnumerable<PersonList> PossibleMothers(DateTime personBirthDate)
        {
            var parentBirthDate = personBirthDate.AddYears(-15);

            var fetchedObjects = ds.Persons
                .Where(p => p.BirthDate <= parentBirthDate & p.Gender == "Female")
                .OrderBy(p => p.FamilyName).ThenBy(p => p.GivenNames);

            return Mapper.Map<IEnumerable<PersonList>>(fetchedObjects);
        }

        public IEnumerable<PersonList> PossibleChildren(DateTime personBirthDate)
        {
            var childBirthDate = personBirthDate.AddYears(15);

            var fetchedObjects = ds.Persons
                .Where(p => p.BirthDate >= childBirthDate)
                .OrderBy(p => p.FamilyName).ThenBy(p => p.GivenNames);

            return Mapper.Map<IEnumerable<PersonList>>(fetchedObjects);
        }

        public PersonBaseWithRelations PersonEditRelations(PersonBaseEditFamily newItem)
        {
            // Validate the incoming data
            var fetchedObject = ds.Persons
                .Include("Father")
                .Include("Mother")
                .Include("Children")
                .SingleOrDefault(p => p.Id == newItem.Id);

            var father = ds.Persons.Find(newItem.FatherId.Value);
            var mother = ds.Persons.Find(newItem.MotherId.Value);

            if (fetchedObject == null)
            {
                return null;
            }
            else
            {
                // Update the object with the incoming values
                ds.Entry(fetchedObject).CurrentValues.SetValues(newItem);

                // Update the associated objects

                if (father != null)
                {
                    fetchedObject.Father = father;
                    fetchedObject.FatherId = father.Id;
                }

                if (mother != null)
                {
                    fetchedObject.Mother = mother;
                    fetchedObject.MotherId = mother.Id;
                }

                // Handle the collection

                if (newItem.ChildrenIds.Count() > 0)
                {
                    // Clear the existing children collection
                    fetchedObject.Children.Clear();

                    // Go through the incoming collection
                    foreach (var childId in newItem.ChildrenIds)
                    {
                        var child = ds.Persons.Find(childId);
                        if (child != null)
                        {
                            fetchedObject.Children.Add(child);
                        }
                    }
                }

                ds.SaveChanges();

                return Mapper.Map<PersonBaseWithRelations>(fetchedObject);
            }

        }

    }

}
