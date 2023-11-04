using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SisMot.Models;

[Table("Person")]
public partial class Person
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("firstName")]
    [StringLength(50)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [Column("lastName")]
    [StringLength(50)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    [Column("secondLastName")]
    [StringLength(50)]
    [Unicode(false)]
    public string? SecondLastName { get; set; }

    [Column("phoneNumber")]
    [StringLength(10)]
    [Unicode(false)]
    public string PhoneNumber { get; set; } = null!;

    [Column("ci")]
    [StringLength(15)]
    [Unicode(false)]
    public string Ci { get; set; } = null!;

    [Column("email")]
    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Column("registerDate", TypeName = "datetime")]
    public DateTime RegisterDate { get; set; }

    [Column("lastUpdate", TypeName = "datetime")]
    public DateTime? LastUpdate { get; set; }

    [Column("status")]
    public byte Status { get; set; }

    [InverseProperty("Person")]
    public virtual ICollection<Motel>? Motels { get; set; } = new List<Motel>();

    [InverseProperty("IdNavigation")]
    public virtual PersonPhoto? PersonPhoto { get; set; }

    [InverseProperty("Person")]
    public virtual ICollection<PersonRequest>? PersonRequests { get; set; } = new List<PersonRequest>();

    [InverseProperty("IdNavigation")]
    public virtual User? User { get; set; }
}
