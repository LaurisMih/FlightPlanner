using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using FlightPlanner.Core.Interface;

namespace FlightPlanner.Core.Models
{
    public abstract class Entity : IEntity
    {
        
        public int Id { get; set; }

        
    }
}
