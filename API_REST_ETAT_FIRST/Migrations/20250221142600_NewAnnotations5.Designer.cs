﻿// <auto-generated />
using System;
using API_REST_ETAT_FIRST.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API_REST_ETAT_FIRST.Migrations
{
    [DbContext(typeof(FilmRatingDBContext))]
    [Migration("20250221142600_NewAnnotations5")]
    partial class NewAnnotations5
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("td4")
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("API_REST_ETAT_FIRST.Models.EntityFramework.Film", b =>
                {
                    b.Property<int>("FilmId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("flm_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("FilmId"));

                    b.Property<DateTime?>("DateSortie")
                        .HasColumnType("date")
                        .HasColumnName("flm_datesortie");

                    b.Property<decimal?>("Duree")
                        .HasColumnType("numeric(3, 0)")
                        .HasColumnName("flm_duree");

                    b.Property<string>("Genre")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("flm_genre");

                    b.Property<string>("Resume")
                        .HasColumnType("text")
                        .HasColumnName("flm_resume");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("flm_titre");

                    b.HasKey("FilmId")
                        .HasName("pk_film");

                    b.HasIndex(new[] { "Titre" }, "ix_t_e_film_flm_titre");

                    b.ToTable("t_e_film_flm", "td4");
                });

            modelBuilder.Entity("API_REST_ETAT_FIRST.Models.EntityFramework.Notation", b =>
                {
                    b.Property<int>("UtilisateurId")
                        .HasColumnType("integer")
                        .HasColumnName("utl_id");

                    b.Property<int>("FilmId")
                        .HasColumnType("integer")
                        .HasColumnName("flm_id");

                    b.Property<int?>("Note")
                        .HasColumnType("integer")
                        .HasColumnName("not_note");

                    b.HasKey("UtilisateurId", "FilmId")
                        .HasName("pk_not");

                    b.HasIndex("FilmId");

                    b.ToTable("t_j_notation_not", "td4");
                });

            modelBuilder.Entity("API_REST_ETAT_FIRST.Models.EntityFramework.Utilisateur", b =>
                {
                    b.Property<int>("UtilisateurId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("utl_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UtilisateurId"));

                    b.Property<string>("CodePostal")
                        .HasColumnType("char(5)")
                        .HasColumnName("utl_cp");

                    b.Property<DateTime>("DateCreation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasColumnName("utl_datecreation")
                        .HasDefaultValueSql("now()");

                    b.Property<float?>("Latitude")
                        .HasColumnType("real")
                        .HasColumnName("utl_latitude");

                    b.Property<float?>("Longitude")
                        .HasColumnType("real")
                        .HasColumnName("utl_longitude");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("utl_mail");

                    b.Property<string>("Mobile")
                        .HasColumnType("char(10)")
                        .HasColumnName("utl_mobile");

                    b.Property<string>("Nom")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("utl_nom");

                    b.Property<string>("Pays")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasDefaultValue("France")
                        .HasColumnName("utl_pays");

                    b.Property<string>("Prenom")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("utl_prenom");

                    b.Property<string>("Pwd")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("utl_pwd");

                    b.Property<string>("Rue")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("utl_rue");

                    b.Property<string>("Ville")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("utl_ville");

                    b.HasKey("UtilisateurId")
                        .HasName("pk_utilisateur");

                    b.HasIndex(new[] { "Mail" }, "uq_utl_mail")
                        .IsUnique();

                    b.ToTable("t_e_utilisateur_utl", "td4");
                });

            modelBuilder.Entity("API_REST_ETAT_FIRST.Models.EntityFramework.Notation", b =>
                {
                    b.HasOne("API_REST_ETAT_FIRST.Models.EntityFramework.Film", "FilmNote")
                        .WithMany("NotesFilm")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_notation_film");

                    b.HasOne("API_REST_ETAT_FIRST.Models.EntityFramework.Utilisateur", "UtilisateurNotant")
                        .WithMany("NotesUtilisateur")
                        .HasForeignKey("UtilisateurId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_notation_utilisateur");

                    b.Navigation("FilmNote");

                    b.Navigation("UtilisateurNotant");
                });

            modelBuilder.Entity("API_REST_ETAT_FIRST.Models.EntityFramework.Film", b =>
                {
                    b.Navigation("NotesFilm");
                });

            modelBuilder.Entity("API_REST_ETAT_FIRST.Models.EntityFramework.Utilisateur", b =>
                {
                    b.Navigation("NotesUtilisateur");
                });
#pragma warning restore 612, 618
        }
    }
}
