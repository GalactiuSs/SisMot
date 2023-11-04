using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SisMot.Models;

public partial class MotelService
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("motelID")]
    public int MotelId { get; set; }

    [Column("servicesID")]
    public int ServicesId { get; set; }

    [ForeignKey("MotelId")]
    [InverseProperty("MotelServices")]
    public virtual Motel? Motel { get; set; } = null!;

    [ForeignKey("ServicesId")]
    [InverseProperty("MotelServices")]
    public virtual Service? Services { get; set; } = null!;
}
