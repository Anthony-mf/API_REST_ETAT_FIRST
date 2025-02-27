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
using System.Xml;
using API_REST_ETAT_FIRST.Models.Repository;
using API_REST_ETAT_FIRST.Models.DataManager;
using Moq;

namespace API_REST_ETAT_FIRST.Controllers.Tests
{
    [TestClass()]
    public class UtilisateursControllerTests
    {
        private UtilisateursController controller;
        private FilmRatingDBContext context;
        private IDataRepository<Utilisateur> dataRepository;

        [TestInitialize]
        public void Initialisation()
        {
            var builder = new DbContextOptionsBuilder<FilmRatingDBContext>().UseNpgsql("Server=localhost;port=5432;Database=R4.01; uid=postgres;password=3246;"); // Chaine de connexion à mettre dans les ( ) 
            context = new FilmRatingDBContext(builder.Options);

            dataRepository = new UtilisateurManager(context);
            controller = new UtilisateursController(dataRepository);
        }

        [TestMethod()]
        public void GetUtilisateursTest()
        {
            List<Utilisateur> expected = new List<Utilisateur>();
            List<Utilisateur> result = new List<Utilisateur>();

            expected = context.Utilisateurs.ToList(); 

            result = controller.GetUtilisateurs().Result.Value.ToList();

            CollectionAssert.AreEqual(expected, result, "Erreur lors de la récupération des users. Le résultat obtenu n'est pas le même qu'attendu.");
        }

        //[TestMethod()]
        //public void GetUtilisateurById_Correct()
        //{
        //    Utilisateur expected = context.Utilisateurs.Where(c => c.UtilisateurId == 1).FirstOrDefault();
        //    Utilisateur result = controller.GetUtilisateurById(1).Result.Value;

        //    Assert.AreEqual(expected, result, "Erreur lors de la récupération d'un user par son id. Le résultat obtenu n'est pas le même qu'attendu.");
        //}

        //[TestMethod()]
        //public void GetUtilisateurById_Uncorrect()
        //{
        //    Utilisateur expected = context.Utilisateurs.Where(c => c.UtilisateurId == 2).FirstOrDefault();
        //    Utilisateur result = controller.GetUtilisateurById(1).Result.Value;

        //    Assert.AreNotEqual(expected, result, "La récupération d'un user par son id a été correct.");
        //}

        [TestMethod]
        public void GetUtilisateurById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Utilisateur user = new Utilisateur
            {
                UtilisateurId = 1,
                Nom = "Calida",
                Prenom = "Lilley",
                Mobile = "0653930778",
                Mail = "clilleymd@last.fm",
                Pwd = "Toto12345678!",
                Rue = "Impasse des bergeronnettes",
                CodePostal = "74200",
                Ville = "Allinges",
                Pays = "France",
                Latitude = 46.344795F,
                Longitude = 6.4885845F
            };
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(user);
            var userController = new UtilisateursController(mockRepository.Object);
            // Act
            var actionResult = userController.GetUtilisateurById(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(user, actionResult.Value as Utilisateur);
        }

        [TestMethod()]
        public void GetUtilisateurByEmail_Correct()
        {
            Utilisateur expected = context.Utilisateurs.Where(c => c.Mail == "clilleymd@last.fm").FirstOrDefault();
            Utilisateur result = controller.GetUtilisateurByEmail("clilleymd@last.fm").Result.Value;

            Assert.AreEqual(expected, result, "Erreur lors de la récupération d'un user par son mail. Le résultat obtenu n'est pas le même qu'attendu.");
        }

        [TestMethod()]
        public void GetUtilisateurByEmail_Uncorrect()
        {
            Utilisateur expected = context.Utilisateurs.Where(c => c.Mail == "clilleymd@last.f").FirstOrDefault();
            Utilisateur result = controller.GetUtilisateurByEmail("clilleymd@last.fm").Result.Value;

            Assert.AreNotEqual(expected, result, "La récupération d'un user par son mail a été correct.");
        }

        [TestMethod]
        public void PostUtilisateur_Correct()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            var userController = new UtilisateursController(mockRepository.Object);

            Utilisateur user = new Utilisateur
            {
                Nom = "POISSON",
                Prenom = "Pascal",
                Mobile = "1",
                Mail = "poisson@gmail.com",
                Pwd = "Toto12345678!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };

            // Act
            var actionResult = userController.PostUtilisateur(user).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Utilisateur>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Utilisateur), "Pas un Utilisateur");
            user.UtilisateurId = ((Utilisateur)result.Value).UtilisateurId;
            Assert.AreEqual(user, (Utilisateur)result.Value, "Utilisateurs pas identiques");
        }

        [TestMethod]
        public void PutUtilisateur_Correct()
        {
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);

            Utilisateur userAtester = new Utilisateur()
            {
                Nom = "MACHIN",
                Prenom = "Luc",
                Mobile = "0606070809",
                Mail = "machin" + chiffre + "@gmail.com",
                Pwd = "Toto1234!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };

            // Act
            controller.PostUtilisateur(userAtester); // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            var result = controller.PutUtilisateur(userAtester.UtilisateurId, userAtester).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre la modification

            // Assert
            Utilisateur? userRecupere = context.Utilisateurs.Where(u => u.Mail.ToUpper() == userAtester.Mail.ToUpper()).FirstOrDefault();

            userAtester.UtilisateurId = userRecupere.UtilisateurId;
            Assert.AreEqual(userRecupere, userAtester, "Utilisateurs pas identiques");
        }

        [TestMethod]
        public void DeleteUtilisateur_Correct()
        {
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);

            Utilisateur userAtester = new Utilisateur()
            {
                Nom = "MACHIN",
                Prenom = "Luc",
                Mobile = "0606070809",
                Mail = "machin" + chiffre + "@gmail.com",
                Pwd = "Toto1234!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };

            // Act

            context.Utilisateurs.Add(userAtester);
            context.SaveChanges();

            var result = controller.DeleteUtilisateur(userAtester.UtilisateurId).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre la suppression

            Utilisateur? userRecupere = context.Utilisateurs.Where(u => u.Mail.ToUpper() == userAtester.Mail.ToUpper()).FirstOrDefault();

            Assert.IsNull(userRecupere, "L'utilisateur n'a pas été supprimé");
        }
    }
}