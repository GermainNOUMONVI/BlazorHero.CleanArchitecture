using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Features.Commands.Queries.GetAllCommands
{
    public class GetAllCommandsResponse
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
