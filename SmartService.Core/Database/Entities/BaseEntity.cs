namespace SmartService.Core.Database.Entities;

using System.ComponentModel.DataAnnotations;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
}