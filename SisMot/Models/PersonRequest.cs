using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SisMot.Models;

[Table("PersonRequest")]
public partial class PersonRequest
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("applicationDate", TypeName = "date")]
    public DateTime ApplicationDate { get; set; }

    /// <summary>
    /// APROBADO
    /// PENDIENTE
    /// RECHAZADO
    /// </summary>
    [Column("statusRequest")]
    public byte StatusRequest { get; set; }

    [Column("personID")]
    public int PersonId { get; set; }

    [Column("motelID")]
    public int MotelId { get; set; }

    [ForeignKey("MotelId")]
    [InverseProperty("PersonRequests")]
    public virtual Motel? Motel { get; set; } = null!;

    [ForeignKey("PersonId")]
    [InverseProperty("PersonRequests")]
    public virtual Person? Person { get; set; } = null!;
}
