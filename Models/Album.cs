﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EverestAlbumLibrary.Models
{
    public class Album
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Tittle { get; set; }
        [Required]
        public string AlbumDetails { get; set; }
        [Required]
        public string CopyCode { get; set; }
        [Required]
        public DateTime ReleasedDate { get; set; }
        [Required]
        public string length { get; set; }
        [NotMapped]
        public HttpPostedFile CoverImage { get; set; }
        public string CoverImagePath { get; set; }
        public string StudioName { get; set; }
        public virtual IEnumerable<Album> Albums { get; set; }
    }
    }