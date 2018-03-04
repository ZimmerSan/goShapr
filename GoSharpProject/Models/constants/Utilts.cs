using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using GoSharpProject.Models.entities;

namespace GoSharpProject.Models.constants
{
    public static class Utilts
    {

        private static ProjectTask first = new ProjectTask
        {
            Name = "Test",
            Description = "Make some work",
            Status = TaskStatus.Initial,
            Price = 300

        };

        private static ProjectTask makeInterface = new ProjectTask
        {
            Name = "Make Interfase",
            Description = "Make",
            Status = TaskStatus.Initial,
            Price = 100
        };


        private static ProjectTask makeNewGraphics = new ProjectTask
        {
            Name = "New Graphics",
            Description = "Make new graphics",
            Status = TaskStatus.Initial,
            Price = 500
        };

        private static ProjectTask makeBackEnd = new ProjectTask
        {
            Name = "BackEnd",
            Description = "Make back end ... ",
            Status = TaskStatus.Initial,
            Price = 1500
        };



        private static ProjectTask createDataBase = new ProjectTask
        {
            Name = "DataBase",
            Description = "It consist something special",
            Status = TaskStatus.Initial,
            Price = 400
        };

        private static ProjectTask findResourses = new ProjectTask
        {
            Name = "Resourse",
            Description = "Find some something",
            Status = TaskStatus.Initial,
            Price = 700
        };


        private static ProjectTask kakaatoHRENY = new ProjectTask
        {
            Name = "TSILKOM NORMALNO",
            Description = "Є моменти зроблені є не зроблені",
            Status = TaskStatus.Initial,
            Price = 1200
        };


        private static ProjectTask propoyWORK = new ProjectTask
        {
            Name = "Some amazing",
            Description = "The one to rule them all",
            Status = TaskStatus.Initial,
            Price = 1000
        };


        private static ProjectTask error = new ProjectTask
        {
            Name = "Somethin wrong",
            Description ="YOU WROTE SHITCODE",
            Status = TaskStatus.Completed,
            Price = 1
        };

        public static ICollection<ProjectTask>  GenericTasks(TemplateSiteTypes type)
        {

            ICollection<ProjectTask> tasks = new Collection<ProjectTask>();
            switch (type)
            {
                case TemplateSiteTypes.Blog:
                    tasks.Add(makeInterface);
                    tasks.Add(makeBackEnd);
                    break;
                case TemplateSiteTypes.VisitCard:
                    tasks.Add(makeInterface);
                    tasks.Add(makeNewGraphics);
                    break;
                case TemplateSiteTypes.Shop:
                    tasks.Add(makeBackEnd);
                    tasks.Add(makeNewGraphics);
                    tasks.Add(createDataBase);
                    break;
                default:
                    tasks.Add(error);
                    break;
            }
            return tasks;
        }
    }
}