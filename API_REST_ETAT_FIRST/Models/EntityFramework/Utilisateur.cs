using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;

namespace API_REST_ETAT_FIRST.Models.EntityFramework
{
    [Table("t_e_utilisateur_utl")]
    [Index(nameof(Mail), Name = "uq_utl_mail", IsUnique = true)]

    public class Utilisateur
    {
        [Key]
        [Column("utl_id")]
        public int UtilisateurId { get; set; }

        [Column("utl_nom")]
        [StringLength(50)]
        public string? Nom { get; set; }

        [Column("utl_prenom")]
        [StringLength(50)]
        public string? Prenom { get; set; }

        [Column("utl_mobile", TypeName = "char(10)")]
        [RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "Le téléphone n'est pas dans le bon format.")]
        public string? Mobile { get; set; }

        [Required]
        [Column("utl_mail")]
        [EmailAddress(ErrorMessage = "La longueur d’un email doit être comprise entre 6 et 100 caractères.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La longueur d’un email doit être comprise entre 6 et 100 caractères.")]
        public string Mail { get; set; } = null!;

        [Column("utl_pwd")]
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{12,20}$", ErrorMessage = "Le mot de passe doit contenir entre 12 et 20 caractères avec au moins 1 lettre majuscule, 1 lettre minuscule, 1 chiffre et 1 caractère spécial.") ]
        // ^(?=.*[a-z]) : au moins une lettre minuscule
        // (?=.*[A-Z]) : au moins une lettre majuscule
        // (?=.*\d) : au moins un chiffre
        // (?=.*[\W_]) : au moins un caractère spécial (non alphanumérique) avec _
        // .{12,20} : entre 12 et 20 caractères

        public string Pwd { get; set; }

        [Column("utl_rue")]
        [StringLength(200)]
        public string? Rue { get; set; }

        [Column("utl_cp", TypeName = "char(5)")]
        [RegularExpression(@"\d{5}", ErrorMessage = "Le code postal n'est pas valide.")]
        public string? CodePostal { get; set; }

        [Column("utl_ville")]
        [StringLength(50)]
        public string? Ville { get; set; }

        [Column("utl_pays")]
        [StringLength(50)]
        public string? Pays { get; set; }

        [Column("utl_latitude", TypeName = "real")]
        public float? Latitude { get; set; }

        [Column("utl_longitude", TypeName = "real")]
        public float? Longitude { get; set; }

        [Column("utl_datecreation", TypeName = "date")]
        [Required]
        public DateTime? DateCreation { get; set;} = DateTime.Now;

        [InverseProperty(nameof(Notation.UtilisateurNotant))]
        public virtual ICollection<Notation>? NotesUtilisateur { get; set; }
    }
}
