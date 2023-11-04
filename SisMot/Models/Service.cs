using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SisMot.Models;

public partial class Service
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nameServices")]
    [StringLength(10)]
    [Unicode(false)]
    public string NameServices { get; set; } = null!;

    [Column("registerDate", TypeName = "datetime")]
    public DateTime RegisterDate { get; set; }

    [Column("lastUpdate", TypeName = "datetime")]
    public DateTime? LastUpdate { get; set; }

    [Column("status")]
    public byte Status { get; set; }

    [InverseProperty("Services")]
    public virtual ICollection<MotelService>? MotelServices { get; set; } = new List<MotelService>();
}
