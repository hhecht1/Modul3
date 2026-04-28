// using System;
// using System.Collections.Generic;
// using System.Linq;

// namespace LINQ_Methoden
// {
//     internal class Temp
//     {
//         static void Main(string[] args)
//         {
//             List<Employee> employeesTechs = new List<Employee>()
//             {
//                 new Employee(){ Id=1, Name= "Tom", Email="tom@email.com", TechSkills =
//                     new List<Techs>{
//                         new Techs() { Technology = "C#"},
//                         new Techs() { Technology = "PYTHON"},
//                         new Techs() { Technology = "VB"}
//                     } },
//                 new Employee(){ Id=2, Name= "Ann", Email="ann@email.com", TechSkills =
//                     new List<Techs>{
//                         new Techs() { Technology = "C++"},
//                         new Techs() { Technology = "SQL"},
//                         new Techs() { Technology = "RUST"}
//                     } },
//                 new Employee(){ Id=3, Name= "Max", Email="max@email.com", TechSkills =
//                     new List<Techs>{
//                         new Techs() { Technology = ".NET"},
//                         new Techs() { Technology = "LINQ"},
//                         new Techs() { Technology = "C"}
//                     } },
//                 new Employee(){ Id=4, Name= "Sara", Email="sara@email.com", TechSkills = new List<Techs>() }
//             };

//             // Method Syntax
//             var methodSyntaxTechMS =
//                 employeesTechs.SelectMany(emp => emp.TechSkills)
//                               .Select(t => t.Technology);

//             // Query Syntax
//             var querySyntaxTechAS =
//                 from emp in employeesTechs
//                 from tech in emp.TechSkills
//                 select tech.Technology;

//             foreach (var item in querySyntaxTechAS)
//             {
//                 Console.WriteLine("Skill: " + item);
//             }
//         }
//     }

//     public class Employee
//     {
//         public int Id { get; set; }
//         public string? Name { get; set; }
//         public string? Email { get; set; }
//         public List<string> Programming { get; set; } =new();
//         public List<Techs> TechSkills { get; set; } = new();
//     }

//     public class Techs
//     {
//         public string? Technology { get; set; } 
//     }
// }