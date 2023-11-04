using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SisMot.Models;

[Table("User")]
public partial class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("userName")]
    [StringLength(45)]
    [Unicode(false)]
    public string UserName { get; set; } = null!;

    [Column("password")]
    [StringLength(50)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [Column("role")]
    [StringLength(30)]
    [Unicode(false)]
    public string Role { get; set; } = null!;

    [ForeignKey("Id")]
    [InverseProperty("User")]
    public virtual Person? IdNavigation { get; set; } = null!;
}
