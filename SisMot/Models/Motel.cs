using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SisMot.Models;

[Table("Motel")]
public partial class Motel
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(70)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("description")]
    [StringLength(150)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [Column("price")]
    [StringLength(10)]
    [Unicode(false)]
    public string Price { get; set; } = null!;

    [Column("phoneNumber")]
    [StringLength(10)]
    [Unicode(false)]
    public string PhoneNumber { get; set; } = null!;

    [Column("latitude")]
    public double Latitude { get; set; }

    [Column("longitude")]
    public double Longitude { get; set; }

    [Column("registerDate", TypeName = "datetime")]
    public DateTime RegisterDate { get; set; }

    [Column("lastUpdate", TypeName = "datetime")]
    public DateTime? LastUpdate { get; set; }

    [Column("status")]
    public byte Status { get; set; }

    [Column("personID")]
    public int PersonId { get; set; }

    [InverseProperty("Motel")]
    public virtual ICollection<MotelPhoto>? MotelPhotos { get; set; } = new List<MotelPhoto>();

    [InverseProperty("Motel")]
    public virtual ICollection<MotelService>? MotelServices { get; set; } = new List<MotelService>();

    [ForeignKey("PersonId")]
    [InverseProperty("Motels")]
    public virtual Person? Person { get; set; } = null!;

    [InverseProperty("Motel")]
    public virtual ICollection<PersonRequest>? PersonRequests { get; set; } = new List<PersonRequest>();
}
