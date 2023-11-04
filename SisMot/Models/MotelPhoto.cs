using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SisMot.Models;

[Table("MotelPhoto")]
public partial class MotelPhoto
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Unicode(false)]
    public string PathPhotoMotel { get; set; } = null!;

    [Column("motelID")]
    public int MotelId { get; set; }

    [ForeignKey("MotelId")]
    [InverseProperty("MotelPhotos")]
    public virtual Motel? Motel { get; set; } = null!;
}
