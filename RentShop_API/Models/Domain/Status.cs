using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using RentShop_API.Models.Domain.Clients;

namespace RentShop_API.Models.Domain;

public class Status
{

    public int Id { get; set; }
    public string Name { get; set; }

    public List<Client> Clients { get; set; } = new();
}