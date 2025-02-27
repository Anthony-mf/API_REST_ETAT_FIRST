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

            CollectionAssert.AreEqual(expected, result, "Erreur lors de la récupération des users. Le résultat obtenu n'est pas le même qu'attendu.");
        }

        [TestMethod()]
        public void GetUtilisateurById_Correct()
        {
            Utilisateur expected = context.Utilisateurs.Where(c => c.UtilisateurId == 1).FirstOrDefault();
            Utilisateur result = controller.GetUtilisateurById(1).Result.Value;

            Assert.AreEqual(expected, result, "Erreur lors de la récupération d'un user par son id. Le résultat obtenu n'est pas le même qu'attendu.");
        }

        [TestMethod()]
        public void GetUtilisateurById_Uncorrect()
        {
            Utilisateur expected = context.Utilisateurs.Where(c => c.UtilisateurId == 2).FirstOrDefault();
            Utilisateur result = controller.GetUtilisateurById(1).Result.Value;

            Assert.AreNotEqual(expected, result, "La récupération d'un user par son id a été correct.");
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
             var result = controller.PostUtilisateur(userAtester).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout
             
             // Assert
             Utilisateur? userRecupere = context.Utilisateurs.Where(u => u.Mail.ToUpper() == userAtester.Mail.ToUpper()).FirstOrDefault(); // On récupère l'utilisateur créé directement dans la BD grace à son mail unique
             // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
             // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
             userAtester.UtilisateurId = userRecupere.UtilisateurId;
             Assert.AreEqual(userRecupere, userAtester, "Utilisateurs pas identiques");
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