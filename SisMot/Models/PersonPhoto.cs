using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SisMot.Models;

[Table("PersonPhoto")]
public partial class PersonPhoto
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Unicode(false)]
    public string PathPhotoPerson { get; set; } = null!;

    [ForeignKey("Id")]
    [InverseProperty("PersonPhoto")]
    public virtual Person? IdNavigation { get; set; } = null!;
}
