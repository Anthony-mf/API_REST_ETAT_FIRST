using Microsoft.VisualStudio.TestTools.UnitTesting;
using API_REST_ETAT_FIRST.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_REST_ETAT_FIRST.Models.EntityFramework;

namespace API_REST_ETAT_FIRST.Controllers.Tests
{
    [TestClass()]
    public class UtilisateursControllerTests
    {
        UtilisateursController controller;
        FilmRatingDBContext context;

        [TestInitialize]
        public void Initialisation()
        {
            var builder = new DbContextOptionsBuilder<FilmRatingDBContext>().UseNpgsql("Server=localhost;port=5432;Database=R4.01; uid=postgres;password=3246;"); // Chaine de connexion à mettre dans les ( ) 
            context = new FilmRatingDBContext(builder.Options);

            controller = new UtilisateursController(context);
        }

        [TestMethod()]
        public void GetUtilisateursTest()
        {
            List<Utilisateur> expected = new List<Utilisateur>();
            List<Utilisateur> result = new List<Utilisateur>();

            expected = context.Utilisateurs.ToList(); 

            result = controller.GetUtilisateurs().Result.Value.ToList();

            CollectionAssert.AreEqual(expected, result, "Erreur lors de la récupération des séries. Le résultat obtenu n'est pas le même qu'attendu.");
        }

        [TestMethod()]
        public void GetUtilisateurById_Correct()
        {
            Utilisateur expected = context.Utilisateurs.FirstOrDefault();
            Utilisateur result = controller.GetUtilisateurById(1).Result.Value;

            Assert.AreEqual(expected, result, "Erreur lors de la récupération d'une série par son id. Le résultat obtenu n'est pas le même qu'attendu.");
        }
    }
}