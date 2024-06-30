using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DliibApi.Data;
public class DliibUser : IdentityUser
{
    [MaxLength(50)]
    public string NickName { get; set; } = null!;
}
